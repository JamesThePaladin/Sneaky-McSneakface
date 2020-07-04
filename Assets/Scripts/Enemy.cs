using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AIController
{
    [SerializeField]
    public Pawn thisEnemy; //for this enemy object
    private GameObject target; //for player object
    private Transform targetTf; //for player transform

    // Start is called before the first frame update
    void Start()
    {
        //if player is empty
        if (target == null) 
        {
            //fill with current player
            target = GameObject.FindWithTag("Player");
        }
        //get target transform
        targetTf = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player is empty, because they got destroyed
        if (target == null)
        {
            //fill with current player
            target = GameObject.FindWithTag("Player");
            //get target transform
            targetTf = target.GetComponent<Transform>();
        }
    }
}
