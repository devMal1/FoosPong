using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject p_paddle;
    public GameObject ai_paddle;
    public GameObject ball;
    public Text leftScoreUI;
    public Text rightScoreUI;
    public int points2Win;

    private GameObject[] gameObjects = new GameObject[3];
    private Vector2[] gameObjects_initPos = new Vector2[3];
    private string points2Win_string;
    private enum SCORE { left, right };

    // Use this for initialization
    void Start () {
        populate();
        points2Win_string = points2Win.ToString();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (checkForWin(SCORE.left) || checkForWin(SCORE.right))
        {
            reset();
            //add some UX animation jazz
        }
	}

    void populate()
    {
        //something wrong with 
        gameObjects[0] = p_paddle;
        gameObjects[1] = ai_paddle;
        gameObjects[2] = ball;

        for (int i = 0; i < gameObjects.Length; i ++)
        {
            gameObjects_initPos[i] = gameObjects[i].GetComponent<Rigidbody2D>().position;
        }
    }

    void reset()
    {
        for (int i = 0; i < gameObjects.Length; i ++)
        {
            gameObjects[i].GetComponent<Rigidbody2D>().Sleep();
            gameObjects[i].GetComponent<Rigidbody2D>().MovePosition(gameObjects_initPos[i]);
        }

        leftScoreUI.text = "0";
        rightScoreUI.text = "0";
    }

    bool checkForWin(SCORE score)
    {
        if (score == SCORE.left)
        {
            return leftScoreUI.text.Equals(points2Win_string) ? true : false;
        } else if (score == SCORE.right)
        {
            return rightScoreUI.text.Equals(points2Win_string) ? true : false;
        } else { return false; }
    }
}
