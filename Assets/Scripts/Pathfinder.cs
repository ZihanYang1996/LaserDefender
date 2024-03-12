using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex++].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)  // if the index is less than the number of waypoints
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);  // move towards the target position
            if (Vector2.Distance(transform.position, targetPosition) < Mathf.Epsilon)  // if the current position is the target position
            {
                waypointIndex++;  // increment the index
            }
        }
        else
        {
            Destroy(gameObject); // destroy the game object
        }
    }
}
