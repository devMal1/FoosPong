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
    private enum SCORE { right, left, both };

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        walls = walls_dir.GetComponentsInChildren<BoxCollider2D>();
        start = true;
        leftScore = 0;
        rightScore = 0;
        UpdateScore(SCORE.both);
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
            //TODO: add to bounce off wall
            
        }

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
                return true;
            }
        }
        return false;
    }

    void reset()
    {
        rb2d.Sleep();
        rb2d.position = new Vector2(0, 0);
    }

}
