using UnityEngine;
using UnityEngine.SceneManagement;

public class Charger : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float smallBoostSpeed;
    [SerializeField] private float smallBoostChargeRate;
    public float maxSmallBoost;
    [SerializeField] private float largeBoostSpeed;
    [SerializeField] private float boostAcceleration;
    [SerializeField] private float acceleration;
    [SerializeField] private float stopSpeed;
    [SerializeField] private float boostStopSpeed;
    [SerializeField] private float turnAngle;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float hitSpeedLoss;

    private Rigidbody2D rb;
    private float verticalInput;
    private bool holdingBoost;
    private bool holdingGo;

    private Quaternion targetRotation;

    private float speed;
    private float bigBoostTimer;
    public float smallBoostCharge;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        holdingBoost = Input.GetKey(KeyCode.LeftShift);
        holdingGo = Input.GetKey(KeyCode.Space);
        verticalInput = Input.GetAxisRaw("Vertical");

        if (speed > 0)
        {
            if (verticalInput >= 0)
            {
                targetRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, -90 + turnAngle), verticalInput);
            }
            else
            {
                targetRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, -90 - turnAngle), -verticalInput);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        if (!holdingBoost && speed >= maxSpeed / 2)
        {
            smallBoostCharge = Mathf.Min(maxSmallBoost, smallBoostCharge + smallBoostChargeRate * Time.deltaTime);
        }


        if (bigBoostTimer > 0)
        {
            speed = Mathf.Min(largeBoostSpeed, speed + boostAcceleration * Time.deltaTime);
            bigBoostTimer -= Time.deltaTime;
        }
        else if (holdingBoost && smallBoostCharge > 0)
        {
            speed = Mathf.Min(smallBoostSpeed, speed + boostAcceleration * Time.deltaTime);
            smallBoostCharge -= Time.deltaTime;
        }
        else if (speed > maxSpeed)
        {
            speed = Mathf.Max(maxSpeed, speed - boostStopSpeed * Time.deltaTime);
        }
        else if (speed < maxSpeed && holdingGo)
        {
            speed = Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime);
        }
        else if (speed > 0 && !holdingGo)
        {
            speed = Mathf.Max(0, speed - stopSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * transform.up;
        Debug.Log(speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed -= hitSpeedLoss * Vector2.Dot(transform.up, -collision.contacts[0].normal);
    }

    public void Boost(float time)
    {
        bigBoostTimer = time;
    }
}
