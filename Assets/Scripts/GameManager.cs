using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;

    private float spawnRate = 1.5f;
    private int score;
    public float remainingLives;
 

    // Start is called before the first frame update
    void Start()
    {
        remainingLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    // Handle update score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    // Handle game over
    public void GameOver()
    {
        if (remainingLives < 1)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;    
        }
    }

    public void LiveLose(int liveToDelete)
    {
        liveText.text = "Live: " + live;
        remainingLives -= liveToDelete;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        LiveLose(0);
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
}
