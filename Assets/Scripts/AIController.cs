using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject ball;
    public GameObject walls;
    public float speed;

    private Rigidbody2D rb2d;
    private Rigidbody2D ball_rb2d;
    //private BoxCollider2D walls_box;
    private int movementVertical;
    private float? startDelay;
    private bool delaying;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ball_rb2d = ball.GetComponent<Rigidbody2D>();
        //walls_box = walls.GetComponent<BoxCollider2D>();
        movementVertical = 1;
        startDelay = null;
        delaying = false;
    }

    void FixedUpdate()
    {
        if (collidedWithWall()) {
            print("inside wall collide");
            movementVertical *= -1;
        } else if (rb2d.IsTouching(ball_rb2d.GetComponent<CircleCollider2D>()) && !delaying) {
            Vector2 force = new Vector2(rb2d.mass * speed, 0);
            ball_rb2d.AddForce(force * Time.deltaTime);
            movementVertical *= -1;
            delay(2);
        } else { movementVertical = movementVertical + 0; }

        Vector2 movement = new Vector2(0, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    bool collidedWithWall()
    {
        //NOT WORKING... gonna have to try something else
        return rb2d.IsTouchingLayers(walls.layer) ? true : false;
    }

    void delay(float seconds)
    {
        if (!startDelay.HasValue) { startDelay = Time.time; }

        if (Time.time - startDelay.Value >= seconds)
        {
            startDelay = null;
            delaying =  false;
        } else { delaying = true; }
    }

}
