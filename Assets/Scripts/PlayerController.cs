using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    Vector2 movement; //to hold input values

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.instance.humanPlayers.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    //when destroyed
    void OnDestroy()
    {
        //GameManager.instance.humanPlayers.Remove(this.gameObject);
    }
}
