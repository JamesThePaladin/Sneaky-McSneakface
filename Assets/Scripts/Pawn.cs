using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject thisPawn; //to hold this pawn object
    private Transform tf; //hold pawn tf
    public float speed; //movement speed
    public float rotateSpeed; //for pawn rotation

    public float hp; //to hold the pawn's helf value
    public float maxHp; //to hold the max health a pawn has

    [Header("AI Stuff")]
    public Transform targetTf; //AI look towards lock

    public float closeEnough; //close enough distance to waypoint

    void Start()
    {
        //get the transform of object you are attached to
        tf = GetComponent<Transform>();
        //set hp to max
        hp = maxHp;
    }

    //***************************************************
    //REMINDER
    //Check out movement to use more-fun tilesets
    //Kenny is awesome for the free stuff but I am not in love with the feel of this kind of top down
    //and the project is hard enough to look at anyway with the time crunch
    //try to get your chicken sprites to work if you finish everything
    //*****************************************************
    public void Move(float direction)
    {
        //create a new vector 3 equal to pawn position
        Vector3 pawnPos = transform.position;
        //add our desired movement direction to it
        pawnPos += transform.right * direction * speed * Time.deltaTime;
        //set z to zero because it was being weird
        pawnPos.z = 0;
        //set our transform equal to pawn position
        transform.position = pawnPos;
    }

    public void Rotate(float direction)
    {
        //pawn rotation
        transform.Rotate(transform.forward * direction * rotateSpeed * Time.deltaTime * -1);
    }

    public void TakeDamage(float damage) 
    {
        //damage is subtracted from hp
        hp -= damage; 
    }

    public void LookTowards() 
    {
        ////get the direction to face
        Vector3 targetLock = targetTf.position - transform.position;
        //rotate to look at target
        transform.right = targetLock;
    }

    public void Death() 
    {
        //if hp is equal to or less than zero
        if (hp <= 0)
        {
            //die
            Destroy(gameObject);
        }
    }
}
    



   

