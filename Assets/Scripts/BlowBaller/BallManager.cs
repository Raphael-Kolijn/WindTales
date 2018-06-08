using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
    private Vector3 startPosition;

    IEnumerator ResetWait()
    {
        yield return new WaitForSeconds(GameMaster.instance.getResetSec());
        ResetBall();
    }

    public Vector3 getStartPosition()
    {
        return startPosition;
    }

    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        startPosition = transform.position;
    }

    public void ShootBall(int power)
    {
        Physics.gravity = GameMaster.instance.getBallGravity();
        Vector3 tempThrowSpeed;
        tempThrowSpeed = new Vector3(GameMaster.instance.getBallThrowSpeed().x, GameMaster.instance.getBallThrowSpeed().y + power, GameMaster.instance.getBallThrowSpeed().z); // power alleen voor missen
        GetComponent<Rigidbody>().AddForce(tempThrowSpeed, ForceMode.Impulse);
        GetComponent<AudioSource>().Play(); //play shoot sound
        StartCoroutine(ResetWait());
    }

    public void ResetBall()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameMaster.instance.resetBasket();
        transform.position = startPosition;
    }
}