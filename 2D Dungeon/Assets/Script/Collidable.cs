using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private ContactFilter2D filter;
    private BoxCollider2D boxcollider;
    private Collider2D[] hits = new Collider2D[10];

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        boxcollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        //Debug.Log(coll.name);
    }

}
