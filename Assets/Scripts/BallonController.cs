using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonController : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer aksara;
    void Start()
    {
        aksara.sprite = sprites[GameManager.Instance.currentAksara];
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        GameManager.Instance.currentPoin += 10;
        GameManager.Instance.pointCount -= 1;
    }
}