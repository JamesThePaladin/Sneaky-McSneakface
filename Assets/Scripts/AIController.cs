using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    
    private NoiseMaker noise; //variable for player volume
    private Transform playerTf; //variable for player's transform

    public float volumeLoss; //volume lost for distance
    public float fieldOfView; //for Ai field of view
    public float alertLevel; //for enemy volume alert threshold
    //enum for AI states
    public enum State
    {
        Idle,
        Patrol,
        LookTowards,
        Chase,
        Attack,
        Death,
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                DoIdle();
                break;
            case State.Patrol:
                //patrol waypoints
                break;
            case State.LookTowards:
                //DoSeek();
                break;
            case State.Chase:
                //chase player
                break;
            case State.Attack:
                //attack player
                break;
            case State.Death:
                //die
                break;
        }

    }
    public void ChangeState(State newState)
    {
        // Change our state
        state = newState;
        Debug.Log("");
    }
    public void DoIdle()
    {
        // Do Nothing!
    }

    //bool for AI hearing
    public bool CanHear(GameObject player)
    {
        //if player has no Noise Maker
        if (noise == null)
        {
            return false;
        }
        //otherwise
        else
        {
            //adjust for distance loss
            float newVolume = noise.volume - volumeLoss;
            //if new volume is greater than the alert threshold
            if (newVolume > alertLevel)
            {
                //return true
                return true;
            }
            //if not
            else
            {
                //return false
                return false;
            }
        }
    }

    //bool for FOV
    public bool CanSee(GameObject player)
    {
        //get the player's transform
        playerTf = player.GetComponent<Transform>();
        // Find the vector from the agent to the target
        Vector3 agentToPlayerVector = playerTf.position - GetComponent<Transform>().position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToPlayer = Vector3.Angle(agentToPlayerVector, transform.forward);
        // if that angle is less than our field of view
        if (angleToPlayer < fieldOfView)
        {
            // Raycast
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToPlayerVector);

            //if the first object hit is the player
            if (hitInfo.collider.gameObject == player)
            {
                //return true
                return true;
            }
        }
        //this will only run if nothing is hit or if something is hit that isnt the player
        return true;
    }
}
