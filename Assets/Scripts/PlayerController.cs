using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jolt;

    private Rigidbody2D rb2d;
    //private bool pushing;
    //public float maxX;
    //private float restingX;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        //pushing = false;
        //maxX = 10f;
        //restingX = transform.position.x;
	}

    void FixedUpdate()
    {
        float movementVertical = Input.GetAxis("Vertical");
        float movementPush = Input.GetAxis("Horizontal");

        //if (movementVertical == 0 ) { rb2d.get}

        Vector2 movement = new Vector2(movementPush, movementVertical);

        rb2d.AddForce(movement * speed * Time.deltaTime);
    }

    /*void Update()
    {
        if (transform.position.x > maxX)
        {
            pushing = false;
            transform.position.Set(maxX, transform.position.y, transform.position.z);
        } else if (transform.position.x < restingX)
        { 
            transform.position.Set(restingX, transform.position.y, transform.position.z);
        }
    }

    float CheckPush()
    {
        float movementPush = Input.GetAxis("Jump");
        if (movementPush <= 0 && !pushing)
        {
            pushing = false;
            return 0f;
        } else if (movementPush > 0 && !pushing)
        {
            pushing = false;
            return jolt * -1;
        } else if (movementPush <= 0 && pushing)
        {
            pushing = true;
            return jolt;
        } else
        {
            pushing = false;
            return jolt * -1;
        }
    }*/

}
