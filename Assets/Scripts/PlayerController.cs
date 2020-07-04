using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.humanPlayers.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
