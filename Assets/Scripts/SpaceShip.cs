using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField]
    GameObject gameHud;


    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    const float ApplyBreaks = 0.5f;
    // to handled adding the thrust on the spaceShip
    Rigidbody2D spaceShip;

    float breakAmount = 0;


    // a value to change that is responsible for rotating the ship in a different direction when using 
    // different dirctions 
    int signChangerForRotation = 1;

    // Vector with direction to make the spaceShip move to the right
    Vector2 thrustDirection = new Vector2(1, 0);
    
    // making sure that the thrust isn't done continusouly when pressing the key (Space bar)
    bool isThurstApplied = false;
    
    // making sure that the stoping isn't done continusouly when pressing the key (down key)
    bool isStopApplied = false;
    
    // true when there is a thrust force applied on the ship to move forward
    // false when the ship is stopped
    bool canBreak = false;

    // making sure to rotate in a stable way 
    bool isRotateRightButtonPressed = false;
    bool isRotateLeftButtonPressed = false;

    public Vector2 ThrustDirection { get { return thrustDirection; } }

    // Start is called before the first frame update
    void Start()
    {
        spaceShip = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateShip();
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            if (!isThurstApplied)
            {
                isThurstApplied = true;
                spaceShip.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);
                canBreak = true;
            }
            else
            {
                isThurstApplied = false;
                
            }
        }
        if (Input.GetAxis("Stop") > 0)
        {
            if (!isStopApplied)
            {
                isStopApplied = true;
                breakAmount += ApplyBreaks;
                float magnitude = Mathf.Sqrt(thrustDirection.x * thrustDirection.x
                                             + thrustDirection.y * thrustDirection.y);
                if (canBreak)
                    spaceShip.AddForce(thrustDirection * -breakAmount, ForceMode2D.Force);
                if (breakAmount >= magnitude * ThrustForce)
                {
                    spaceShip.Sleep();
                    breakAmount = 0;
                    canBreak = false;
                }
            }
            else
            {
                isStopApplied = false;
            }
        }
    }



    private void rotateShip()
    {
        float rotationAmount = signChangerForRotation * RotateDegreesPerSecond * Time.deltaTime;
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput < 0)
        {
            if (!isRotateRightButtonPressed)
            {
                isRotateRightButtonPressed = true;
                isRotateLeftButtonPressed = false;
                rotationAmount *= -1;
                signChangerForRotation = -1 * Mathf.Abs(signChangerForRotation);
            }
        }
        if (rotationInput > 0)
        {
            if (!isRotateLeftButtonPressed)
            {
                isRotateLeftButtonPressed = true;
                isRotateRightButtonPressed = false;
                signChangerForRotation = Mathf.Abs(signChangerForRotation);
                rotationAmount = Mathf.Abs(rotationAmount);
            }
        }
        if (rotationInput == 0)
        {
            isRotateLeftButtonPressed = false;
            isRotateRightButtonPressed = false;
            rotationAmount = 0;
        }

        // change the thrust direction according to the direction of the ship's rotation
        float rotateAroundZAxis = Mathf.Deg2Rad * transform.eulerAngles.z;
        thrustDirection.x = Mathf.Cos(rotateAroundZAxis);
        thrustDirection.y = Mathf.Sin(rotateAroundZAxis);


        transform.Rotate(Vector3.forward, rotationAmount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameHud.GetComponent<HUD>().stopGameTimer();
        AudioManager.Play(AudioClipName.PlayerDeath);
        Destroy(gameObject);
    }


}
