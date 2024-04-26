using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer lr;
    private GameObject player;
    private GameObject handF;
    private SpriteRenderer playerSprite;

    private Vector3 offset0;
    private Vector3 offset1;
    private Vector3 offset1Flip;

    private void Start()
    {
        player = GameObject.Find("Player");
        handF = GameObject.Find("handF");
        playerSprite = player.GetComponent<SpriteRenderer>();

        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        offset0 = new Vector3(0f, -0.2f, 0f);
        offset1 = new Vector3(0.1f, -0.2f, 0f);
        offset1Flip = new Vector3(-0.1f, -0.2f, 0f);

    }

    private void Update()
    {
        //If the player sprite is not flipped
        if (!playerSprite.flipX)
        {
       
            lr.SetPosition(0, player.transform.position + offset0);
            lr.SetPosition(1, handF.transform.position + offset1);
        }
        //If the player sprite is flipped
        else
        {
            lr.SetPosition(0, player.transform.position + offset0);
            lr.SetPosition(1, handF.transform.position + offset1Flip);
        }
        //lr.SetPosition(0, player.transform.position + offset0);
        //lr.SetPosition(1, handF.transform.position + offset1);

    }



}
