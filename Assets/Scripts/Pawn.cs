using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject thisPawn; //to hold this pawn object
    private Transform tf; //hold pawn tf
    public Transform attackPoint; //the transform of attack point empty
    public float speed; //movement speed
    public float rotateSpeed; //for pawn rotation
    public float attackRange; //for attack range
    public float hp; //to hold the pawn's helf value
    public float maxHp; //to hold the max health a pawn has
    public float damage;
    public float noiseDistance;

    [Header("AI Stuff")]
    public Transform targetTf; //AI look towards lock
    public float chaseRange; //for chase range
    public float closeEnough; //close enough distance to waypoint
    public float viewRadius; 
    public float fieldOfView; //for Ai field of view

    void Start()
    {
        //get the transform of object you are attached to
        tf = GetComponent<Transform>();
        //set hp to max
        hp = maxHp;
    }

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
    public void Attack() 
    {
        //Detect all enemies in range of attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        //damage them
        foreach (Collider2D entities in hits) 
        {
            Debug.Log("HIT");
        }
    }

    //to show attack range visually
    private void OnDrawGizmosSelected()
    {
        //check if there is an attack point
        if (attackPoint == null)
            return;
        //draw a wire sphere with unity's gizmo at the attackPoint's position, with a radius of attack range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    public Vector3 AngleToTarget(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
    



   

