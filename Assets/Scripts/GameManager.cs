using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float totalTime;
    public TextMeshProUGUI timerText, pointText;
    public int currentPoin;
    public string[] aksaraText;

    public int currentAksara;
    public int pointCount;
    public TextMeshProUGUI aksaraSource;

    private float currentTime;
    public int currentGame;

    private bool isPlay;

    [Header("Nyawa")]
    public int startHealth;
    [HideInInspector] public int currentHealth;
    public TextMeshProUGUI healthText;

    [Header("Panel")]
    public GameObject pausePanel;
    public GameObject gameoverPanel;

    [Header("Audio")]
    public AudioSource BGM;
    public AudioSource Benar;
    public AudioSource Salah;
    public AudioSource GameOver;

    void Start()
    {
        currentTime = PlayerPrefs.GetFloat("Time", totalTime);
        currentPoin = PlayerPrefs.GetInt("Point", 0);
        currentHealth = PlayerPrefs.GetInt("Health", startHealth);
        currentAksara = Random.Range(1, 21);
        aksaraSource.text = aksaraText[currentAksara];
        pointCount = 5;

        gameoverPanel.SetActive(false);
        Time.timeScale = 1;

        if(PlayerPrefs.GetInt("Game") >= 3)
        {
            PlayerPrefs.SetInt("Game", 1);
            currentHealth += 1;
        }
        currentGame = PlayerPrefs.GetInt("Game", 1);

        isPlay = false;
    }

    void Update()
    {
        TimerUpdate();

        pointText.text = "Poin : " + currentPoin.ToString();
        healthText.text = "Nyawa : " + currentHealth.ToString();

        if(pointCount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt("Point", currentPoin);
            PlayerPrefs.SetFloat("Time", currentTime);
            PlayerPrefs.SetInt("Health", currentHealth);
            currentGame += 1;
            PlayerPrefs.SetInt("Game", currentGame);
        }

        if(currentHealth <= 0)
        {
            gameoverPanel.SetActive(true);
            Time.timeScale = 0;

            if(!isPlay)
            {
                GameOver.Play();
                isPlay = true;
            }
        }
    }

    private void TimerUpdate()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = 0;
        }

        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
    }
}
