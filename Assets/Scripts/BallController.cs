using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float initSpeed;
    public int initXDirection;

    private Rigidbody2D rb2d;
    private bool start;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        start = true;
	}
	

    void FixedUpdate()
    {
        if (start) {
            rb2d.AddForce(new Vector2(initXDirection, 0) * initSpeed);
            start = false;
        }
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
