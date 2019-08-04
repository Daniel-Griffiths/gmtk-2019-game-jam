using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Kino;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public TMP_Text scoreText;
    public GameObject enemySpawnPoint;
    public GameObject gameOverScreen;
    public GameObject mainCamera;

    public bool gameHasEnded = false;
    private float timeSlow = 0.5f;
    private int score = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        InvokeRepeating("SpawnEnemy", 2f,2f);
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (audioSource.pitch > .2f && gameHasEnded == true) {
            audioSource.pitch -= 0.02f;
        }
    }

    void SpawnEnemy() {
        Instantiate(enemy, enemySpawnPoint.transform.position, Quaternion.identity);
    }

    public void GameOver(){
        if (gameHasEnded == false) {
            gameHasEnded = true;
            Time.timeScale = timeSlow;
            gameOverScreen.SetActive(true);
            mainCamera.GetComponent<AnalogGlitch>().enabled = true;
            mainCamera.GetComponent<DigitalGlitch>().enabled = true;
        }
    }

    public void IncreaseScore(){
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
