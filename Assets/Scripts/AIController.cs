using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public float heightOfBackground;
    public float heightOfPaddle;
    public GameObject ball; //need to use for collision detection
    public float speed;

    private Rigidbody2D rb2d;
    private int movementVertical;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementVertical = 1;
    }

    void FixedUpdate()
    {
        if (collidedWithWall()) { movementVertical *= -1; }

        //ball.GetComponent<Rigidbody2D>().AddForce();

        Vector2 movement = new Vector2(0, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    bool collidedWithWall()
    {
        float topOfPaddle = rb2d.position.y + (heightOfPaddle / 2);
        float bottomOfPaddle = rb2d.position.y - (heightOfPaddle / 2);

        float topOfBackground = heightOfBackground / 2;
        float bottomOfBackground = topOfBackground * -1;

        //make ternary statement
        if (topOfPaddle >= topOfBackground || bottomOfPaddle <= bottomOfBackground) { return true; }
        else { return false; }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.CompareTag("Wall")) { movementVertical *= -1; }
    }*/

}
