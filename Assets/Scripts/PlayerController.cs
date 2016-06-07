using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speedX;
    public float speedY;
    public string horizontal_axis;
    public string vertical_axis;
    public GameObject leftConstraint;
    public GameObject rightConstraint;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
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

}
