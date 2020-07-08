using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    private Rigidbody2D rb; //for attached object's rb
    public float volume; //to hold player volume
    public float volumeLoss; //volume lost for distance

    void Start()
    {
        //get the rb of object
        rb = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
      //TODO add noise maker functionss
    }
}
