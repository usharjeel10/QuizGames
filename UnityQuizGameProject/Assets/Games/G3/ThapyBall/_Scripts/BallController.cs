using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float upForce;
    Rigidbody2D rb;
    bool started;
    bool gameOver;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        started = false;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!started) { // if game hasn't started yet
            if (Input.GetMouseButtonDown(0))
            {
                started = true;
                rb.isKinematic = false; // will make it start falling
                ThayBallGameManager.instance.GameStart();
            }
        }
        else // if game has been started
        {
            if (Input.GetMouseButtonDown(0) && !gameOver)
            {
                rb.velocity = Vector2.zero; // making velocity 0, was preventing ball from jumping
                rb.AddForce(new Vector2(0.0f, upForce));
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col) // if ball collides with anything
    {
        gameOver = true;
        ThayBallGameManager.instance.GameOver();
        GetComponent<Animator>().Play("ball");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Pipe pipe = col.gameObject.GetComponent<Pipe>();

        if (pipe && !gameOver)
        {
            gameOver = true;
            ThayBallGameManager.instance.GameOver();
            GetComponent<Animator>().Play("ball");
        }

        if (col.gameObject.tag == "ScoreChecker" && !gameOver)
            ThayBallScoreManager.instance.IncrementScore();
    }
}
