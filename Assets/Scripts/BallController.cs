using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

    public float initSpeed;
    public int initXDirection;
    public Text leftScoreUI;
    public Text rightScoreUI;
    public GameObject walls_dir;

    private Rigidbody2D rb2d;
    private BoxCollider2D[] walls;
    private bool start;
    private int leftScore;
    private int rightScore;
    private enum SCORE { right, left, both }
    private Vector2 prevVel;
    private Vector2 prevDir;
    private string whichWall;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        walls = walls_dir.GetComponentsInChildren<BoxCollider2D>();
        start = true;
        leftScore = 0;
        rightScore = 0;
        UpdateScore(SCORE.both);
        whichWall = null;
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
        } else if (bounceOffWall())
        {
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

            rb2d.Sleep();
            rb2d.AddForce(new Vector2(30 * dirX, 30 * dirY));

        }

        prevVel = rb2d.velocity;
        prevDir = new Vector2(getDir(prevVel.x), getDir(prevVel.y));

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        bool isGoal = true;

        if (otherObject.CompareTag("RightGoal"))
        {
            leftScore++;
            UpdateScore(SCORE.left);
        } else if (otherObject.CompareTag("LeftGoal"))
        {
            rightScore++;
            UpdateScore(SCORE.right);
        } else { isGoal = false; }

        if (isGoal) { reset(); }
    }

    void UpdateScore(SCORE score)
    {
        if (score == SCORE.right)
        {
            rightScoreUI.text = rightScore.ToString();
        }
        else if (score == SCORE.left)
        {
            leftScoreUI.text = leftScore.ToString();
        } else
        {
            rightScoreUI.text = rightScore.ToString();
            leftScoreUI.text = leftScore.ToString();
        }
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

    int getDir(float vel)
    {
        return (int)(vel / Mathf.Abs(vel == 0 ? 0 : vel));
    }

    void reset()
    {
        rb2d.Sleep();
        rb2d.position = new Vector2(0, 0);
    }

}
