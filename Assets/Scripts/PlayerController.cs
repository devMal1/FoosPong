﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speedX;
    public float speedY;
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
        else if (rb2d.IsTouching(rightConstraint.GetComponent<BoxCollider2D>())) { movementPush = movementPush > 0 ? resetMovementPush() : movementPush; }

        if (movementVertical == 0 && movementPush == 0) { rb2d.Sleep(); }
        else
        {
            rb2d.WakeUp();

            rb2d.AddForce(new Vector2(movementPush * speedX, movementVertical * speedY) * Time.deltaTime);
        }

    }

    float resetMovementPush()
    {
        rb2d.Sleep();
        rb2d.position = rb2d.position + new Vector2(0, 0);
        return 0;
    }

}
