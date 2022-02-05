using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();    
        DontDestroyOnLoad(gameObject);
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int id)
    {
        sr.sprite = GameManager.instance.playerSprite[id];
    }

    public void OnLevelUp()
    {
        maxHitPoint ++;
        hitPoint = maxHitPoint;
        GameManager.instance.OnHitpointChange();
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    
    public void HealUp(int heal)
    {
        if (hitPoint == maxHitPoint)
            return;
        else if (hitPoint + heal < maxHitPoint)
        {
            GameManager.instance.ShowText("+" + heal + " HP", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            hitPoint += heal;
            GameManager.instance.OnHitpointChange();

        }
        else
        {
            GameManager.instance.ShowText("+" + (maxHitPoint - hitPoint) + " HP", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            hitPoint = maxHitPoint;
            GameManager.instance.OnHitpointChange();
        }
    }
}
