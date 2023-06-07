using LuLib.Transform;
using LuLib.Vector;
using Pathfinding;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AIPath))]
public class FindPlayer : MonoBehaviour
{
    // inspector settings
    [SerializeField] private float maxDistance;
    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;

    // constants
    private static int wallLayer;

    // references
    private AIPath path;
    [Inject] private PlayerController player;
    private IOnSeePlayer[] onSeePlayers;

    // vars
    [HideInInspector] public bool CanSeePlayer;

    private void Awake()
    {
        // get references
        path = GetComponent<AIPath>();
        onSeePlayers = GetComponents<IOnSeePlayer>();

        // init constants
        wallLayer = LayerMask.GetMask("Wall");
    }

    private void FixedUpdate()
    {
        Transform t = transform;
        Vector3 position = t.position;

        // check if it can see player
        Vector3 delta = player.transform.position - position;

        if (!Physics.Raycast(position, delta.normalized, delta.magnitude, wallLayer))
        {
            CanSeePlayer = true;

            // check if its too far away to stop pathfinding
            if (delta.magnitude > maxDistance)
            {
                path.canMove = true;
                path.destination = player.transform.position;
            }
            else
            {
                // check if whole head can see player
                // right
                Vector3 rightPoint = position + t.right * rightOffset;
                Vector3 deltaRight = player.transform.position - rightPoint;
                bool rightIntersect = Physics.Raycast(rightPoint, deltaRight.normalized, delta.magnitude, wallLayer);

                // left
                Vector3 leftPoint = position - t.right * leftOffset;
                Vector3 deltaLeft = player.transform.position - leftPoint;
                bool leftIntersect = Physics.Raycast(leftPoint, deltaLeft.normalized, delta.magnitude, wallLayer);

                if (!leftIntersect && !rightIntersect) path.canMove = false; // stop pathfinding if head can see player
                else
                {
                    path.canMove = true;
                    path.destination = player.transform.position; // continue pathfinding if head cant see player
                }
            }

            // look at player
            Vector2 delta2D = new(delta.x, delta.z);
            float rotation = delta2D.GetRotation();
            t.SetAngleY(-rotation);

            foreach (IOnSeePlayer onSeePlayer in onSeePlayers)
            {
                onSeePlayer.OnSeePlayer();
            }
        }
        // path find to last seen position of player if line of sight lost
        else if (CanSeePlayer)
        {
            path.canMove = true;
            path.destination = player.transform.position;

            CanSeePlayer = false;
        }
    }
}