using System.Collections;
using System.Collections.Generic;
using NPCS.Schedule;
using UnityEngine;
using Grid = NPCS.Schedule.Grid;

namespace Pathfinding
{
    public class PathFollower : MonoBehaviour
    {
            private List<AStarPathfinding.Node> path;
            private int currentNodeIndex;
            private float moveSpeed = .5f; // NPC move speed
            private float distanceThreshold = 0.1f; 
      
            private Pathfinder pathfinder;
            private Grid grid;
            
            [SerializeField] private Transform destination;
            private IEnumerator Start()
            {
                
                pathfinder = FindObjectOfType<Pathfinder>();
                yield return null;

                grid = pathfinder.Grid;
                Vector2 startGridPos = grid.GetGridPosition(transform.position);
                Vector2 endGridPos = grid.GetGridPosition(destination.position);
                SetPath(pathfinder.FindPath(startGridPos,endGridPos));


            }
            
            public void SetPath(List<AStarPathfinding.Node> p)
            {
  
                path = p;
                currentNodeIndex = 0;
            }

            private void Update()
            {
                if (path == null)
                {
                    return;
                }

                if (currentNodeIndex >= path.Count)
                {
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
