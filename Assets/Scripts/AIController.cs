using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

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

        Vector2 movement = new Vector2(0, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.CompareTag("Wall")) { movementVertical *= -1; }
    }

}
