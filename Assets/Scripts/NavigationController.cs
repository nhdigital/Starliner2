using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseFirstButton;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameOverFirstButton;
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject howToPlayFirstButton;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject settingsFirstSlider;
    [SerializeField] GameObject functionality;
    [SerializeField] GameObject title;
    [SerializeField] GameObject version;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject leaderboardFirstButton;
    [SerializeField] GameObject creditsFirstButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            PauseUnpause();        
        } 

        if (gameManager.gameOverCalled)
        {
            GameOverUI();
        }
    }

    public void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy && gameManager.isGameActive)
        {
            pauseMenu.SetActive(true);
            gameManager.isGameActive = false;
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            
        }
        else
        {
            pauseMenu.SetActive(false);
            gameManager.isGameActive = true;
            Time.timeScale = 1f;
            
        }
    }

    public void GameOverUI()
    {
        StartCoroutine(tmmButtonActive()); 
    }

    public void HowToPlayUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(howToPlayFirstButton);
       // functionality.SetActive(false);
       // title.SetActive(false);
       // version.SetActive(false);
      //  howToPlay.SetActive(true);
    }

    public void SettingsUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstSlider);
     //   functionality.SetActive(false);
     //   title.SetActive(false);
     //   version.SetActive(false);
        settings.SetActive(true);
    }


    public void LeaderboardUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leaderboardFirstButton);
    }

    public void CreditsUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }


    public void Reset()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    IEnumerator tmmButtonActive()
    {
        yield return new WaitForSeconds(5);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
    }


}
