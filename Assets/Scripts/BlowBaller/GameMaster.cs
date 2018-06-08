using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [Header("Breath Settings")]
    [SerializeField]
    private int minimumValueStart;
    [SerializeField]
    private int difficulty;
    private int minimumValue;
    private int highestValueReached;
    [Header("Reset Settings")]
    [SerializeField]
    private float resetSec;
    [Header("Basket settings")]
    [SerializeField]
    private GameObject basket;
    [SerializeField]
    private float basketSpeed;
    private Vector3 basketStartPosition;
    [Header("Ball settings")]
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Vector3 ballThrowSpeed;
    [SerializeField]
    private Vector3 ballGravity;
    [Header("Meter settings")]
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private int ruisValue;
    [SerializeField]
    private GameObject minimumValueArrow;
    [SerializeField]
    private float arrowSpeed;
    [Header("Controller settings")]
    [SerializeField]
    private DeviceManager.DeviceType deviceType;
    private int currentScore;
    

    public static GameMaster instance;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        DeviceManager.Instance.SetDeviceType(deviceType);
        minimumValue = minimumValueStart;
        highestValueReached = 0;
        minimumValueArrow.GetComponent<minimumValueArrow>().changePosition(slider, minimumValue);
        basketStartPosition = basket.transform.position;
    }
    void FixedUpdate()
    {
        updateSlider();
    }

    public float getBasketSpeed()
    {
        if (basketSpeed < 0) // negatieve waardes: basket andere kant op, verdwijnd uit beeld
        {
            return 0;
        }
        return basketSpeed;
    }

    public GameObject getBall()
    {
        return ball;
    }

    public Vector3 getBallThrowSpeed()
    {
        return ballThrowSpeed;
    }

    public Vector3 getBallGravity()
    {
        return ballGravity;
    }

    public Vector3 getBasketStartPosition()
    {
        return basketStartPosition;
    }

    public void scorePlusPlus()
    {
        currentScore++;
    }
    private bool VectorCompare(Vector3 a, Vector3 b)
    {
        if (Mathf.Round(a.x) == Mathf.Round(b.x)
            && Mathf.Round(a.y) == Mathf.Round(b.y)
            && Mathf.Round(a.z) == Mathf.Round(b.z))
        {
            return true;
        }
        return false;
    }

    public void setMinimumValue()
    {
        if (currentScore > 0)
        {
            minimumValue = minimumValueStart + (difficulty * currentScore);
            minimumValueArrow.GetComponent<minimumValueArrow>().changePosition(slider, minimumValue);
            
        }
    }

    public void resetBasket()
    {
        slider.value = 0;
        highestValueReached = 0;
        basket.GetComponent<Basket>().Reset();
    }

    public void setBasketSpeed()
    {
        if (currentScore <= 0)
        {
            basketSpeed = 0;
        }
        else
        {
            basketSpeed = 30 + (10 * currentScore);
        }

    }

    public float getResetSec()
    {
        return resetSec;
    }

    public void updateSlider()
    {
        if (VectorCompare(ball.transform.position, ball.GetComponent<BallManager>().getStartPosition()) == true)
        {
            slider.value = (float)System.Math.Round(DeviceManager.Instance.FlowLMin, 1) - ((float)System.Math.Round(DeviceManager.Instance.FlowLMin, 1) * 2);
            Debug.Log("slider: " + slider.value);
            if (slider.value > ruisValue)
            {
                if (slider.value >= highestValueReached)
                {
                    highestValueReached = (int)slider.value;
                    if (highestValueReached >= minimumValue)
                    {
                        Debug.Log("raak");
                        ball.GetComponent<BallManager>().ShootBall(0);
                    }
                }
                else if (slider.value < highestValueReached)
                {

                    Debug.Log("mis");
                    ball.GetComponent<BallManager>().ShootBall(-20);
                }
            }

        }
    }



}