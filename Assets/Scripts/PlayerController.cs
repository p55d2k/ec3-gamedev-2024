using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    bool canjump = false;
    Vector2 gravity;

    public float terminalVelocity = -20f;
    public float jumpHeight = 20;
    public float fallStrength = 1;
    public float accel = 1f;
    public float moveSpeed = 10f;

    float speed = 0;
    GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.gameOver)
        {
            return;
        }

        // define the current velocity of the player
        Vector2 currentVelocity = new Vector2(0.0f, 0.0f);

        // if the player is not moving up or down, they can jump
        if (rb.velocity[1] > -0.001 && rb.velocity[1] < 0.001)
        {
            canjump = true;
            gravity = new Vector2(0, 0);
        }
        else // if the player is moving up or down, they cannot jump
        {
            canjump = false;
        }

        // if the player is moving too fast, set their velocity to the terminal velocity
        if (gravity[1] < terminalVelocity)
        {
            gravity = new Vector2(0, terminalVelocity);
        }

        // if the player is not jumping, apply gravity
        if (!canjump)
        {
            gravity += new Vector2(0.0f, -fallStrength);
        }

        //  if the player is pressing the jump button, and they can jump, set their velocity to the jump height
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (canjump)
            {
                gravity = new Vector2(0, jumpHeight);
            }
        }

        // if the player is pressing the left or right arrow keys, or the A or D keys, move the player
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            speed -= accel;
            if (speed < -moveSpeed)
            {
                speed = -moveSpeed;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            speed += accel;
            if (speed > moveSpeed)
            {
                speed = moveSpeed;
            }
        }

        // if the player is not pressing the left or right arrow keys, or the A or D keys, slow the player down
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            // if the player is moving slower than the acceleration, stop the player
            if (Mathf.Abs(speed) < accel)
            {
                speed = 0;
            }
            // if the player is moving faster than the acceleration, slow the player down
            if (speed > 0)
            {
                speed -= accel / 2;
            }
            // if the player is moving faster than the acceleration, slow the player down
            else if (speed < 0)
            {
                speed += accel / 2;
            }
        }

        // apply the current velocity to the player
        currentVelocity += new Vector2(speed, 0.0f);
        rb.velocity = currentVelocity + gravity;
    }
}