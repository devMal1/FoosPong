﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

    public float minSpeed;
    public float initSpeed;
    //public int initXDirection;
    public Text leftScoreUI;
    public Text rightScoreUI;
    public GameObject walls_dir;
    public GameObject paddle_constraints;

    private Rigidbody2D rb2d;
    private BoxCollider2D[] walls;
    private bool start;
    private Vector2 prevVel;
    private Vector2 prevDir;
    private string whichWall;
    private float maxX_paddleConstraint;
    private float minX_paddleConstraint;
    private float maxX_paddleConstraint2;
    private float minX_paddleConstraint2;
    private int initXDirection;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        walls = walls_dir.GetComponentsInChildren<BoxCollider2D>();
        start = true;
        leftScoreUI.text = "0";
        rightScoreUI.text = "0";
        whichWall = null;
        Transform[] pc = paddle_constraints.GetComponentsInChildren<Transform>();
        maxX_paddleConstraint = Mathf.Max(pc[1].position.x, pc[2].position.x);
        minX_paddleConstraint = Mathf.Min(pc[1].position.x, pc[2].position.x);
        maxX_paddleConstraint2 = Mathf.Max(pc[3].position.x, pc[4].position.x);
        minX_paddleConstraint2 = Mathf.Min(pc[3].position.x, pc[4].position.x);
    }
	

    void FixedUpdate()
    {
        if (start) {
            rb2d.AddForce(new Vector2(randomDir(), randomDir()) * initSpeed);
            start = false;
        } else if (rb2d.IsSleeping())
        {
            rb2d.WakeUp();
            rb2d.AddForce(new Vector2(randomDir(), randomDir()) * initSpeed);
        } else {

            if (bounceOffWall()) {
                int dirX = (int)prevDir.x;
                int dirY = (int)prevDir.y;
                switch (whichWall)
                {
                    case "TopWall":
                        dirY = -1;
                        break;
                    case "BotWall":
                        dirY = 1;
                        break;
                    case "RightWall":
                        dirX = -1;
                        break;
                    case "LeftWall":
                        dirX = 1;
                        break;
                    default:
                        dirY = 0;
                        dirX = 0;
                        break;
                }

                float speedX;
                speedX = inPlayersReach() ? 0 : initSpeed;
                rb2d.AddForce(new Vector2(speedX * dirX, 30 * dirY));
            } else {
                if (!inPlayersReach()) {
                    float velX = rb2d.velocity.x;
                    if (Mathf.Abs(velX) < minSpeed)
                    {
                        float velXMag = rb2d.velocity.normalized.x;
                        velX += (norm(velXMag)*0.05f);

                        rb2d.AddForce(new Vector2(velX, 0));

                    }
                }
            }
        }

        prevDir = rb2d.velocity.normalized;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        bool isGoal = true;
        int points;

        if (otherObject.CompareTag("RightGoal"))
        {
            points = int.TryParse(leftScoreUI.text, out points) ? points : -1;
            points++;
            leftScoreUI.text = points.ToString();
        } else if (otherObject.CompareTag("LeftGoal"))
        {
            points = int.TryParse(rightScoreUI.text, out points) ? points : -1;
            points++;
            rightScoreUI.text = points.ToString();
        } else { isGoal = false; }

        if (isGoal) { reset(); }
    }

    bool bounceOffWall()
    {
        foreach (BoxCollider2D wall in walls)
        {
            if (rb2d.IsTouching(wall)) {
                whichWall = wall.tag.ToString();
                return true;
            }
        }
        whichWall = null;
        return false;
    }

    bool inPlayersReach()
    {
        return (rb2d.position.x > minX_paddleConstraint &&  rb2d.position.x < maxX_paddleConstraint) ||
                (rb2d.position.x > minX_paddleConstraint2 && rb2d.position.x < maxX_paddleConstraint2) ? true : false;
    }

    void reset()
    {
        rb2d.Sleep();
        rb2d.velocity.Set(0f, 0f);
        rb2d.position = new Vector2(0, 0);
    }

    int norm(float value)
    {
        if (value < 0) { return -1; }
        else if (value > 0) { return 1; }
        else { return 0; }
    }

    int randomDir()
    {
        return Mathf.CeilToInt(Random.Range(0, 2)) == 0 ? -1 : 1;
    }

}
