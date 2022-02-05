using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{ 
    public float boundx;
    public float boundy;
    public Transform look;

    private void Start()
    {
        look = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltax = look.position.x - transform.position.x;
        if(deltax > boundx || deltax < -boundx)
        {
            if(transform.position.x < look.position.x)
            {
                delta.x = deltax - boundx;
            }
            else
            {
                delta.x = deltax + boundx;
            }
        }

        float deltay = look.position.y - transform.position.y;
        if (deltay > boundy || deltay < -boundy)
        {
            if (transform.position.y < look.position.y)
            {
                delta.y = deltay - boundy;
            }
            else
            {
                delta.y = deltay + boundy;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
