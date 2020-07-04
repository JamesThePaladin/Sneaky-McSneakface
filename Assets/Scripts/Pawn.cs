using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject thisPawn; //to hold this pawn object
    public Transform tf; //hold pawn tf
    public float speed; //movement speed
    public float rotationSpeed; //speed for pawn rotation

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
        //calculating what rotation will be within vector
        Quaternion rotateDirection = Quaternion.LookRotation(direction, Vector3.forward);
        //set x and y rotation to 0.
        rotateDirection.x = 0;
        rotateDirection.y = 0;
        //rotate sprite to proper direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed * Time.deltaTime);

        //transform.LookAt(target, Vector3.froward);
    }
}
