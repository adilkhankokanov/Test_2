using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public GameController gc;
    float delta;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        delta = 300f / Screen.width;
    }
    Vector2 lastMousePos;
    bool mousePressed = false;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            lastMousePos = Input.mousePosition;
            
        }
        if(!Input.anyKey && mousePressed)
        {
            rb.AddForce((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePos) * delta);
        }
        mousePressed = Input.anyKey;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
            gc.Lose();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("goal"))
        {
            gc.Win();
        }
    }
}
