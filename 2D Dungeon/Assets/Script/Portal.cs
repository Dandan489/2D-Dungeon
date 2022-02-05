using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] scenenames;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            GameManager.instance.SaveState();
            string scenename = scenenames[Random.Range(1, scenenames.Length)];
            SceneManager.LoadScene(scenename);
        }
    }
}
