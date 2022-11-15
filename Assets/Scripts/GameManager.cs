using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public int difficulty = 1;
    [SerializeField] int score;
    [SerializeField] int previousDifficulty = 1;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject timeLabel;
    [SerializeField] GameObject shipsLabel;
    [SerializeField] GameObject ammoLabel;
    [SerializeField] TextMeshProUGUI levelLabel;
    [SerializeField] TextMeshProUGUI countdownDisplay;
    [SerializeField] GameObject labels;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource worldspaceAudioSource;
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject levelUpText;
    [SerializeField] GameObject gameOverExplosion;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] AudioSource gameOver;
    [SerializeField] Button toMainMenuButton;
    [SerializeField] Image toMainMenuImage;
    [SerializeField] TextMeshProUGUI toMainMenuText;
    [SerializeField] PlayfabManager playfabManager;
    [SerializeField] Animator motdAnim;
    private int countdownTime; 
    private int secondsPerLevel = 30;
    [SerializeField] Fire fire;
    public bool gameOverCalled;



    public void StartGame()
    {
        

        isGameActive = true;
        fire.enabled = true;
        score = 0;
        motdAnim.SetTrigger("StartGame");
        spawnManager.StartSpawning();
        StartCountdown();
        Buttons.SetActive(false);
        anim.SetBool("StartButtonClicked", true);
        labels.SetActive(true);
        worldspaceAudioSource.Stop();
        audioSource.Play();
    }


    public void GameOver()
    {
        isGameActive = false;
        gameOverExplosion.SetActive(true);
        gameOver.Play();
        audioSource.Stop();
        spawnManager.DestroyAllPrefabs();
        gameOverUI.SetActive(true);
        playerGameObject.SetActive(false);
        labels.SetActive(false);
        playfabManager.SendLeaderboard(score);
        StartCoroutine(buttonWorking());
        //int finalScore = score-1;
        finalScoreText.text = "Final Score: " + score.ToString();
        
        gameOverCalled = true;
    }


    public void StartCountdown()
    {
        countdownTime = 0;
        StartCoroutine(CountdownToEnd());
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        sfxManager.PlayExplosionSFX();
    }


   public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }


    public string FormatTime(int seconds)
    {
        int min = seconds / 60;
        int sec = seconds - (min * 60);
        string smin = min.ToString().PadLeft(2, '0');
        string ssec = sec.ToString().PadLeft(2, '0');
        return $"{smin}:{ssec}";
    }


    IEnumerator CountdownToEnd()
    {
        while (countdownTime >= 0 && isGameActive)
        {
            difficulty = 1 + (countdownTime / secondsPerLevel);
            levelLabel.text = "Level: " + difficulty;

            if (difficulty > previousDifficulty)
            {
                sfxManager.PlayLevelUpSFX();
                StartCoroutine(levelUpDisplay());
              
                previousDifficulty = difficulty;
            }

            countdownDisplay.text = FormatTime(countdownTime);
            yield return new WaitForSeconds(1f);
            countdownTime++;
            Debug.Log(difficulty);
        }
    }


    IEnumerator levelUpDisplay()
    {
        levelUpText.SetActive(true);
        yield return new WaitForSeconds(4);
        levelUpText.SetActive(false);
    }

    IEnumerator buttonWorking()
    {
        yield return new WaitForSeconds(5);
        toMainMenuImage.enabled = true;
        toMainMenuText.enabled = true;
        toMainMenuButton.enabled = true;
    }


    public void ShowLeaderboard()
    {
        playfabManager.GetLeaderboard();
    }


   
}





