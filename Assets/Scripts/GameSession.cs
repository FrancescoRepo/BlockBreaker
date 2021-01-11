using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 83;
    [SerializeField] private int lifesForLevel = 2;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private Image[] lifesImages;

    private int currentLife = 2;
    private int currentScore = 0;

    private void Awake()
    {
        int countGameStatus = FindObjectsOfType<GameSession>().Length;
        if (countGameStatus > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void RemoveLife()
    {
        currentLife--;

        if (currentLife == 0)
        {
            Destroy(lifesImages[currentLife]);
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            Destroy(lifesImages[currentLife]);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            FindObjectOfType<Ball>().LockTheBall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
