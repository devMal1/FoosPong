using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speedX;
    public float speedY;
    public string horizontal_axis;
    public string vertical_axis;
    public GameObject leftConstraint;
    public GameObject rightConstraint;
    public GameObject ball;

    private Rigidbody2D rb2d;
    private Rigidbody2D ball_rb2d;
    private bool delaying;
    private float? startDelay;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        ball_rb2d = GetComponent<Rigidbody2D>();
        delaying = false;
        startDelay = null;
	}

    void FixedUpdate()
    {
        float movementVertical = Input.GetAxis(vertical_axis);
        float movementPush = Input.GetAxis(horizontal_axis);

        if (rb2d.IsTouching(leftConstraint.GetComponent<BoxCollider2D>())) { movementPush = rb2d.velocity.normalized.x < 0 || movementPush < 0 ? resetMovementPush() : movementPush; }
        else if (rb2d.IsTouching(rightConstraint.GetComponent<BoxCollider2D>())) { movementPush = rb2d.velocity.normalized.x > 0 || movementPush > 0 ? resetMovementPush() : movementPush; }

        if (movementVertical == 0 && movementPush == 0) {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.Sleep();
            if (rb2d.IsTouching(ball.GetComponent<CircleCollider2D>()) && !delaying) {
                ball_rb2d.AddForce(new Vector2(100/*ball_prevDir.x*-1*/, 0)); //TODO: Figure out how to utilize the ball's direction!!
                delay(1);
            }
        } else {
            rb2d.WakeUp();
            rb2d.AddForce(new Vector2(movementPush * speedX, movementVertical * speedY) * Time.deltaTime);
        }

    }

    float resetMovementPush()
    {
        rb2d.position = rb2d.position + new Vector2(0, 0);
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        return 0;
    }

    void delay(float seconds)
    {
        if (!startDelay.HasValue) { startDelay = Time.time; }

        if (Time.time - startDelay.Value >= seconds)
        {
            startDelay = null;
            delaying = false;
        }
        else { delaying = true; }
    }

}
