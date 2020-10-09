﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;


    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;
    public float horzontalTValue;

    private Rigidbody2D m_rigidBody;
    private Vector3 touchesEnd;

    // Start is called before the first frame update
    void Start()
    {
        touchesEnd = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }
   
    private void _FireBullet()
    {
        if(Time.frameCount % 20 == 0)
        {
           bulletManager.GetBullet(transform.position);
        }
       
    }


    private void _Move()
    {
        float direction = 0.0f;

        // simple touch imput

        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {
                // direction is positive 
                direction = 1.0f;
            }
            if (worldTouch.x < transform.position.x)
            {
                // direction is negative
                direction = -1.0f;
            }

            touchesEnd = worldTouch;
        }

        if(Input.GetAxis("Horizontal") >= 0.1f)
        {
            // direction is positive 
            direction = 1.0f;
        }
        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

    
      
        if (touchesEnd.x != 0.0f)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchesEnd.x, horzontalTValue), transform.position.y);
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;

        }
    }

    private void _CheckBounds()
    {
        //check the right bounds and check left bounds.
       if(transform.position.x >= horizontalBoundary)
       {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
       }

        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }
    }
}
