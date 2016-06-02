using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject ball;
    public GameObject walls_dir;
    public float speed;

    private Rigidbody2D rb2d;
    private Rigidbody2D ball_rb2d;
    private BoxCollider2D[] walls;
    private int movementVertical;
    private float? startDelay;
    private bool delaying;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ball_rb2d = ball.GetComponent<Rigidbody2D>();
        walls = walls_dir.GetComponentsInChildren<BoxCollider2D>();
        movementVertical = 1;
        startDelay = null;
        delaying = false;
    }

    void FixedUpdate()
    {
        if (collidedWithWall()) {
            movementVertical *= -1;
        } else if (rb2d.IsTouching(ball_rb2d.GetComponent<CircleCollider2D>()) && !delaying) {
            /*Vector2 force = new Vector2(12 * -1, 0);
            ball_rb2d.AddForce(force);*/
            movementVertical *= -1;
            delay(2);
            ball_rb2d.velocity = ball_rb2d.velocity * -1;
        } else { movementVertical = movementVertical + 0; }

        Vector2 movement = new Vector2(0, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    bool collidedWithWall()
    {
        foreach (BoxCollider2D wall in walls)
        {
            if (rb2d.IsTouching(wall)) { return true; }
        }

        return false;
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
