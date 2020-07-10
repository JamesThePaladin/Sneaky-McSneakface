using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    private Rigidbody2D rb; //for attached object's rb
    private CircleCollider2D sound; //our circle collider sound wave
    public float volume; //to hold player volume

    void Start()
    {
        //get the rb of object
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (rb.velocity.x > 0 || rb.velocity.y > 0) 
        {
            volume++;
        }
    }
}
