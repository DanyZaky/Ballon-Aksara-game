using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1; //inisiasi pada scene main menu agar waktu berjalan normal
    }

    //method untuk klik PLay Game
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
    }
}
