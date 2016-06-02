using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

    public float minSpeed;
    public float initSpeed;
    public int initXDirection;
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
    }
	

    void FixedUpdate()
    {
        if (start) {
            rb2d.AddForce(new Vector2(initXDirection, 0) * initSpeed);
            start = false;
        } else if (rb2d.IsSleeping())
        {
            rb2d.WakeUp();
            rb2d.AddForce(new Vector2(initXDirection, 0) * initSpeed);
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
                speedX = rb2d.position.x > minX_paddleConstraint && rb2d.position.x < maxX_paddleConstraint ? 0 : 30;
                //rb2d.Sleep();
                rb2d.AddForce(new Vector2(speedX * dirX, 30 * dirY));
                /*float velX = rb2d.velocity.x;
                float velY = rb2d.velocity.y;
                rb2d.velocity.Set(prevVel.x * dirX, prevVel.y * dirY);
                print("velX : " + rb2d.velocity.x);
                print("velY : " + rb2d.velocity.y);*/
            }

            float velX = rb2d.velocity.x;
            int velXMag = (int)rb2d.velocity.normalized.x;
            if (Mathf.Abs(velX) < minSpeed)
            {
                if (velXMag < 0) { velX--; }
                else if (velXMag > 0) { velX++; }
            }
            rb2d.velocity = new Vector2(velX, rb2d.velocity.y);
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

    void reset()
    {
        rb2d.Sleep();
        rb2d.velocity.Set(0F, 0F);
        rb2d.position = new Vector2(0, 0);
    }

}
