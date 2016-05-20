using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float heightOfBackground;
    public float heightOfPaddle;
    public GameObject ball; //need to use for collision detection
    public float speed;

    private Rigidbody2D rb2d;
    private Rigidbody2D ball_rb2d;
    private int movementVertical;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ball_rb2d = GetComponent<Rigidbody2D>();
        movementVertical = 1;
    }

    void FixedUpdate()
    {
        if (collidedWithWall()) { movementVertical *= -1; }
        else if (collidedWithBall()) {
            Vector2 force = new Vector2(rb2d.mass * speed, 0);
            ball_rb2d.AddForce(force * Time.deltaTime);
            movementVertical *= -1;
        } else { movementVertical = movementVertical + 0; }

        Vector2 movement = new Vector2(0, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    //TODO: change this to use IsTouching();
    bool collidedWithWall()
    {
        float topOfPaddle = rb2d.position.y + (heightOfPaddle / 2);
        float bottomOfPaddle = rb2d.position.y - (heightOfPaddle / 2);

        float topOfBackground = heightOfBackground / 2;
        float bottomOfBackground = topOfBackground * -1;

        return (topOfPaddle >= topOfBackground || bottomOfPaddle <= bottomOfBackground) ? true : false;
    }

    //TODO: change this to use  IsTouching();
    bool collidedWithBall()
    {
        float tol = 1f;

        return ((ball_rb2d.position.x + tol) == rb2d.position.x || (ball_rb2d.position.x - tol) == rb2d.position.x) ? true : false;
    }

}
