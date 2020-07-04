using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //variable that holds this instance of the GameManager

    float timer; //timer for spawning
    public float waitTime; //wait time for spawning 

    private GameObject player; //variable for player
    private NoiseMaker noise; //variable for player volume
    private Transform playerTf; //variable for player's transform
    public float volumeLoss; //volume lost for distance

    public float fieldOfView; //for Ai field of view
    public float alertLevel; //for enemy volume alert threshold

    public int score; //public player score for testing
    public int lives; //lives for player
    public Text scoreText; //reference to score text
    public Text livesText; //reference to lives text

    public Controller[] allPlayers = FindObjectsOfType<Controller>(); //list of all controllers
    public Controller[] humanPlayers = FindObjectsOfType<PlayerController>(); //list of all player controllers
    public Controller[] aiPlayers = FindObjectsOfType<AIController>(); //list of all ai controllers

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else
        {
            Destroy(this.gameObject); // delete the new game manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }

        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
            noise = player.GetComponent<NoiseMaker>(); //get player's noise maker
        }

        score = 0; //initialize score
        scoreText.text = "" + score;//update score text in UI
        livesText.text = "Lives: " + lives;//update lives in UI
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //takes in points from other objects and adds it to the player's score
    void ScorePoints(int addPoints) 
    {
        score += addPoints; //add points to player score
        scoreText.text = "" + score;//update score text in UI
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

