using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonUncorrectController : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer aksara;
    void Start()
    {
        int randomNumber = GenerateRandomNumber(); //melakukan generate aksara
        aksara.sprite = sprites[randomNumber]; //attach sprite/gambar aksara di tiap index
    }

    //method untuk generate random aksara pada nilai salah
    int GenerateRandomNumber()
    {
        int result = Random.Range(1, 20);
        while (result == GameManager.Instance.currentAksara)
        {
            result = Random.Range(1, 20);
        }
        return result;
    }


    //method untuk klik aksara yang salah
    private void OnMouseDown()
    {
        if(!GameManager.Instance.isGameOver)
        {
            Destroy(gameObject);
            GameManager.Instance.currentHealth -= 1;
            GameManager.Instance.Salah.Play();
        }
    }
}
