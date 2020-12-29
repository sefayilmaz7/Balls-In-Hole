using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    private GameObject coin;
    public GameObject firstBall;
    public GameObject secondBall;


    // This class manages our game
    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("Coin");
    }

    void Update()
    {
        // rotating coins
        if (coin != null)
            coin.transform.Rotate(0, 0, 0.5f, Space.Self);

        // controlling if balls are active or not

        if (firstBall != null || secondBall != null)
        {
            if (!firstBall.activeSelf && !secondBall.activeSelf)
            {
                FinishGame();
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("FinishGame");
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
