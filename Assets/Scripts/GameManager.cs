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

    public bool isGameOver;

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
            if (currentHealth > 5) // kondisi untuk setel max health ke 5, jadi tidak lebih dari 5
            {
                currentHealth += 1;
            }
        }
        currentGame = PlayerPrefs.GetInt("Game", 1);

        isPlay = false;
        isGameOver = false;
    }

    void Update()
    {
        TimerUpdate();

        pointText.text = "Poin : " + currentPoin.ToString();
        healthText.text = "Nyawa : " + currentHealth.ToString();

        if(pointCount <= 0) //kondisi jika menang di tiap level maka akan ke level selanjutnya
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt("Point", currentPoin);
            PlayerPrefs.SetFloat("Time", currentTime);
            PlayerPrefs.SetInt("Health", currentHealth);
            currentGame += 1;
            PlayerPrefs.SetInt("Game", currentGame);
        }

        if(currentHealth <= 0) // kondisi jika nyawa habis
        {
            gameoverPanel.SetActive(true);
            Time.timeScale = 0;

            if(!isPlay)
            {
                GameOver.Play();
                isPlay = true;
            }

            isGameOver = true;
        }
    }

    //method untuk pengurangan waktu
    private void TimerUpdate()
    {
        currentTime -= Time.deltaTime; // fungsi waktu berkurang

        if (currentTime < 0)
        {
            currentTime = 0;

            gameoverPanel.SetActive(true);
            Time.timeScale = 0;

            if (!isPlay)
            {
                GameOver.Play();
                isPlay = true;
            }

            isGameOver = true;
        }

        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }


    //method untuk pause game
    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    //method untuk resume game
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    //method untuk restart game
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
    }

    //method jika klik back to menu/ kembali ke mainmenu scene
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
