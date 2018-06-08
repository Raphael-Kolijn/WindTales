using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Collections;
using UnityEngine.EventSystems;

public class SimpleCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Tank,
        Direct,
        Tap
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private Camera m_camera;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;
    [SerializeField] [Range(1, 10)] private float m_movementSpeed;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private bool _isAtStand = false;

    private Vector3 m_target;

    private void OnCollisionEnter(Collision collision)
    {
        StopMoving();

        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }

                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }

            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }

        if (m_collisions.Count == 0)
        {
            m_isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            case ControlMode.Tap:
                TapUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk)
            {
                v *= m_backwardsWalkScale;
            }
            else
            {
                v *= m_backwardRunScale;
            }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = m_camera.transform.forward * m_currentV + m_camera.transform.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void TapUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    m_target = hit.point;

                    if (hit.transform.GetComponent<TappableObject>() != null)
                    {
                        m_target = hit.transform.position;

                        Collider[] colliders = Physics.OverlapSphere(transform.position, 2);

                        foreach (var collider1 in colliders)
                        {
                            if (collider1.name.Equals(hit.transform.name))
                            {
                                m_target = new Vector3(0, 0, 0);
                            }
                        }
                    }

                    if (m_target.y != 0)
                    {
                        StartCoroutine(Turn());
                    }
                }
            }
        }

        if (m_target.y != 0)
        {
            m_animator.SetFloat("MoveSpeed", m_movementSpeed);
            Vector3 direction = (m_target - transform.position).normalized;
            m_rigidBody.MovePosition(transform.position + direction * m_movementSpeed * Time.deltaTime);
        }
    }

    IEnumerator Turn()
    {
        var localTarget = transform.InverseTransformPoint(m_target);

        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        while (angle < -5 || angle > 5)
        {
            localTarget = transform.InverseTransformPoint(m_target);

            angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            var eulerAngleVelocity = new Vector3(0, angle * 5, 0);
            var deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            m_rigidBody.MoveRotation(m_rigidBody.rotation * deltaRotation);

            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        _isAtStand = true;

        StopMoving();

        other.GetComponent<TappableObject>().OpenUi();
    }

    private void OnTriggerExit(Collider other)
    {
        _isAtStand = false;
    }

    private void StopMoving()
    {
        m_target = new Vector3(0, 0, 0);
        StopAllCoroutines();

        m_rigidBody.velocity = new Vector3(0, 0, 0);
        m_rigidBody.angularVelocity = new Vector3(0, 0, 0);

        m_animator.SetFloat("MoveSpeed", 0);
    }
}