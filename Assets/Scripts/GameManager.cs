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

    public GameObject player; //variable for player
    private NoiseMaker noise; //variable for player volume
    private Transform playerTf; //variable for player's transform


    public int score; //public player score for testing
    public int lives; //lives for player
    public Text scoreText; //reference to score text
    public Text livesText; //reference to lives text


    public enum GameStates 
    {
        MainMenu,
        Game
    }

    private void Awake()
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
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
            noise = player.GetComponent<NoiseMaker>(); //get player's noise maker
        }

        score = 0; //initialize score
        scoreText.text = "" + score;//update score text in UI
        livesText.text = "Lives: " + lives;//update lives in UI
        Controller[] allPlayers = FindObjectsOfType<Controller>(); //list of all controllers
        Controller[] humanPlayers = FindObjectsOfType<PlayerController>(); //list of all player controllers
        Controller[] aiPlayers = FindObjectsOfType<AIController>(); //list of all ai controllers
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) //if player slot is empty
        {
            player = GameObject.FindWithTag("Player"); //fill it with player
            noise = player.GetComponent<NoiseMaker>(); //get player's noise maker
        }
    }

    //takes in points from other objects and adds it to the player's score
    void ScorePoints(int addPoints) 
    {
        score += addPoints; //add points to player score
        scoreText.text = "" + score;//update score text in UI
    }
}

