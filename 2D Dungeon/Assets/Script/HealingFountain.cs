﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount =2;

    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player" && Time.time-lastHeal > healCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.HealUp(healingAmount);
        }
    }
}
