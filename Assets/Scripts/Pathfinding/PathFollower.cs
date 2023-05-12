using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NPCS.Schedule
{
    public class PathFollower : MonoBehaviour
    {
            private List<AStarPathfinding.Node> path;
            private int currentNodeIndex;
            private float moveSpeed = .5f;
            private float distanceThreshold = 0.1f;
            [SerializeField] private Transform destination;
            private Pathfinder pathfinder;
            private Grid grid;
            private IEnumerator Start()
            {
                
                pathfinder = FindObjectOfType<Pathfinder>();
                yield return null;

                grid = pathfinder.Grid;
                Vector2 newTransform = grid.GetGridPosition(transform.position);
                Vector2 newDest = grid.GetGridPosition(destination.position);
                SetPath(pathfinder.FindPath(newTransform,newDest));


            }




            public void SetPath(List<AStarPathfinding.Node> p)
            {
                //Debug.Log(p.StringFromStringBuilder());
                path = p;
                currentNodeIndex = 0;
            }

            private void Update()
            {
                if (path == null)
                {
                    Debug.Log("Path was null");
                    return;
                }

                if (currentNodeIndex >= path.Count)
                {
                   Debug.Log($"Reached end of path : {currentNodeIndex} ");
             
                    // Reached the end of the path
                    return;
                }

                // Move towards the current node
                Vector3 targetPosition = path[currentNodeIndex].Position;

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Check if the current node has been reached
                if (Vector2.Distance(transform.position, targetPosition) <= distanceThreshold)
                {
                    // Move to the next node
                    currentNodeIndex++;
                }
            }
        

    }
}
