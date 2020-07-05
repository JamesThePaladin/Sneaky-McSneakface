using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : AIController
{
    [SerializeField]
    public Pawn thisEnemy; //for this enemy object
    public GameObject target; //for player object
    private Transform targetTf; //for player transform
    public float stopDistance; //variable for enemy stopping distance

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
        if (Vector2.Distance(transform.position, targetTf.position) > stopDistance)
        {
            //move towards player at a rate of speed * the amount of time since the last frame draw
            transform.position = Vector2.MoveTowards(transform.position, targetTf.position, pawn.speed * Time.deltaTime);
        }
    }
}
