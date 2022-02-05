using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite Empty;
    public int coins = 5;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = Empty;
            GameManager.instance.coins += coins;
            GameManager.instance.ShowText("+" + coins + " coins", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }

    }
}
