using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointMovement : MonoBehaviour
{
    private GameObject handF;

    private void Start()
    {
        handF = GameObject.Find("handF");
    }


    private void Update()
    {
        //AttackPoint gameobject is at the same position as handF.
        transform.position = handF.transform.position;
    }

}
