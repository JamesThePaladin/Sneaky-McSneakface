using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject thisPawn; //to hold this pawn object
    private Transform tf; //hold pawn tf
    public float speed; //movement speed
    public float rotationSpeed; //speed for pawn rotation

    void Start()
    {
        tf = GetComponent<Transform>();
    }

    public virtual void Attack() 
    {
        Debug.Log("This is the parent Attack.");
    }

    public void Move(float direction)
    {
        //pawn movement
        transform.Translate(transform.right * direction * speed * Time.deltaTime);
    }

    public void RotateToward(Vector3 direction) 
    {
       
    }
}
