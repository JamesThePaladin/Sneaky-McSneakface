using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Pawn pawn;
    public enum state
    {
        patrol,
        attack,
        search,
        chase
    }

    public state currentState;

    [Header("patrolling")]
    public List<Transform> waypoints;
    public int patrolIndex;
    public float closeEnough;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
            switch (currentState)
            {
                case state.patrol:
                    Patrol();
                    break;
                case state.attack:
                    Attack();
                    break;
                case state.search:
                    Search();
                    break;
                case state.chase:
                    Chase();
                    break;
            }
    }

    void Patrol()
    {
        Seek((waypoints[patrolIndex].position = transform.position).normalized);
        //check distance from AI to waypoint
        if (Vector3.Distance(transform.position, waypoints[patrolIndex].position) < closeEnough)
        {
            if (patrolIndex >= waypoints.Count)
            {

            }
            else
            {
                //if close enough to waypoint, continue to next
                patrolIndex++;
            }
        }
    }

    void Attack() 
    {
        //attack
    }

    void Chase() 
    {

    }
    void Search() 
    {
        
    }
}

