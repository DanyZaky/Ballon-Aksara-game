using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonUncorrectController : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer aksara;
    void Start()
    {
        int randomNumber = GenerateRandomNumber();
        aksara.sprite = sprites[randomNumber];
    }

    int GenerateRandomNumber()
    {
        int result = Random.Range(1, 20);
        while (result == GameManager.Instance.currentAksara)
        {
            result = Random.Range(1, 20);
        }
        return result;
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
