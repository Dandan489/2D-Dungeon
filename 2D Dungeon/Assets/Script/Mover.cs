using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected Vector3 Move;
    protected BoxCollider2D boxcollider;
    protected RaycastHit2D hit;
    protected float xSpeed = 1.0f;
    protected float ySpeed = 0.75f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        Move = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if (Move.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (Move.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        Move += pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(0, Move.y), Mathf.Abs(Move.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        if (hit.collider == null)
        {
            transform.Translate(0, Move.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(Move.x, 0), Mathf.Abs(Move.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        if (hit.collider == null)
        {
            transform.Translate(Move.x * Time.deltaTime, 0, 0);
        }
    }
}
