using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        float movementVertical = Input.GetAxis("Vertical");
        float movementPush = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(movementPush, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

}
