using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    void Update()
    {
        if (pawn == null) 
        {
            pawn = GameManager.instance.player.GetComponent<Pawn>();
        }

        //get vertical and horizontal axi
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        //send vertical to pawn move function
        pawn.Move(vertical);
        //send horizontal to pawn rotate function
        pawn.Rotate(horizontal);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            pawn.Attack();
        }
    }
}
