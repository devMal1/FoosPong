using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject leftConstraint;
    public GameObject rightConstraint;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        float movementVertical = Input.GetAxis("Vertical");
        float movementPush = Input.GetAxis("Horizontal");

        if (rb2d.IsTouching(leftConstraint.GetComponent<BoxCollider2D>())) { movementPush = movementPush < 0 ? resetMovementPush() : movementPush; }
        if (rb2d.IsTouching(rightConstraint.GetComponent<BoxCollider2D>())) { movementPush = movementPush > 0 ? resetMovementPush() : movementPush; }

        if (movementVertical == 0 && movementPush == 0) { rb2d.Sleep(); }
        else
        {
            rb2d.WakeUp();

            Vector2 movement = new Vector2(movementPush, movementVertical);

            rb2d.AddForce(movement * speed * Time.deltaTime);
        }

    }

    float resetMovementPush()
    {
        rb2d.Sleep();
        return 0;
    }

}
