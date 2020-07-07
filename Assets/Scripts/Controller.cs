using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Pawn pawn;


    protected virtual void Start()
    {
        pawn = GetComponent<Pawn>(); 
    }
}
