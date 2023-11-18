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

    void Start()
    {
        currentTime = totalTime;
        currentPoin = 0;
        currentAksara = Random.Range(1, 21);
        aksaraSource.text = aksaraText[currentAksara];
        pointCount = 5;
    }

    void Update()
    {
        TimerUpdate();

        pointText.text = "Poin : " + currentPoin.ToString();

        if(pointCount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
