using System;
using System.Collections;
using System.Collections.Generic;
using LuLib.Transform;
using LuLib.Vector;
using Pathfinding;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AIPath))]
public class FindPlayer : MonoBehaviour
{
    // constants
    private static int wallLayer;

    // references
    private AIPath path;
    [Inject] private PlayerController player;

    // vars
    private bool canSeePlayer = false;

    private void Awake()
    {
        // get references
        path = GetComponent<AIPath>();

        // init constants
        wallLayer = LayerMask.GetMask("Wall");
    }

    private void FixedUpdate()
    {

        // check if it can see player
        Vector3 delta = player.transform.position - transform.position;
        if (!Physics.Raycast(transform.position, delta.normalized, delta.magnitude, wallLayer))
        {

            canSeePlayer = true;

            // stop pathfinding (found player)
            path.canMove = false;

            // look at player
            Vector2 delta2D = new(delta.x, delta.z);
            float rotation = delta2D.GetRotation();
            transform.SetAngleY(-rotation);

            // TODO: init shooting here or smth
        }
        // if line of sight stops
        else if (canSeePlayer)
        {
            // move to last seen player position
            path.canMove = true;
            path.destination = player.transform.position;

            canSeePlayer = false;
        }
    }
}
