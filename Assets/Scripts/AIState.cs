//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIState : MonoBehaviour
//{
//    public string AIState;
//    public AIState aiState = "idle";
//    public float aiSenseRadius; // If the player is closer than this, we will chase
//    public float cutoff; // if our health is below this, we will stop and heal
//    public float restingHealRate; // in hp/framedraw  

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (aiState == "idle")
//        {
//            // Do Action
//            DoIdle();
//            // Check for transitions
//            if (Vector3.Distance(transform.position, tf.position) < aiSenseRadius)
//            {
//                aiState = "seek";
//            }
//        }
//        else if (aiState == "seek")
//        {
//            // Do Action
//            DoSeek();
//            // Check for transitions
//            if (Vector3.Distance(transform.position, tf.position) > aiSenseRadius)
//            {
//                aiState = "idle";
//            }
//            if (health < cutoff)
//            {
//                aiState = "rest";
//            }
//        }
//        else if (aiState == "rest")
//        {
//            // Do Action
//            DoRest();
//            // Check for transitions
//            if (health > cutoff)
//            {
//                aiState = "idle";
//            }
//        }
//    }
//    public void ChangeState(string newState)
//    {
//        // Change our state
//        aiState = newState;
//    }
//    public void DoIdle()
//    {
//        // Do Nothing!
//    }

//    public void DoSeek()
//    {
//        Vector3 vectorToTarget = target.position - tf.position;
//        tf.position += vectorToTarget.normalized * speed;
//    }

//    public void DoRest()
//    {
//        // Increase our health. Remember that our increase in this case is "per frame"!
//        health += restingHealRate;

//        // But never go over our max health
//        health = Mathf.Min(health, maxHealth);
//    }
//}
