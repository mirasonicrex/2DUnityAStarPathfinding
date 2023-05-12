using System.Collections.Generic;
using NPCS.Schedule;
using UnityEngine;
using Grid = NPCS.Schedule.Grid;

namespace Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
  
        // For NPCs to call the pathfinding algorithm 
        public Grid Grid { get; private set; }
        private AStarPathfinding aStar;

        private void Start()
        {

            Grid = FindObjectOfType<CreateGrid>().Grid;
            
            aStar = new AStarPathfinding(Grid);

        }

        public List<AStarPathfinding.Node> FindPath(Vector2 startPos, Vector2 endPos)
        {
           return aStar.FindPath(startPos, endPos);
        }
        
        

       
    }
}