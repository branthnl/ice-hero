﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody2D myRigidbody2D;
    private Vector3 startPosition;
    private bool isMoving;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Give some gravity when needed
            float vsp = myRigidbody2D.velocity.y;
            if (Mathf.Abs(vsp) < speed)
            {
                myRigidbody2D.velocity += new Vector2(0, ((speed * Mathf.Sign(vsp == 0? 1 : vsp)) - vsp) * 0.1f);
            }
        }
        else
        {
            transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * 10.0f) * 0.05f, 0);
        }
    }
    public void Start()
    {
        Respawn();
    }
    public void Respawn()
    {
        isMoving = false;
        myRigidbody2D.velocity = Vector2.zero;
        transform.position = startPosition;
        Invoke("FirstMove", 1.0f);
    }
    public void FirstMove()
    {
        isMoving = true;
        myRigidbody2D.velocity = new Vector2(Random.Range(-3.0f, 3.0f), 3.0f) * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            float xDiff = transform.position.x - other.transform.position.x;
            myRigidbody2D.velocity += new Vector2(xDiff, 0);
        }
    }
}