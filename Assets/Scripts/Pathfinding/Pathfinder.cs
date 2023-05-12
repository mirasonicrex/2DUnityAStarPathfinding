using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace NPCS.Schedule
{
    public class Pathfinder : MonoBehaviour
    {
  
        // For NPC's to call the pathfinding algorithm 
        public Grid Grid { get; private set; }
        private AStarPathfinding aStar;

        private void Start()
        {
            // Need to make the grid instances separate 
            
            Grid = FindObjectOfType<CreateGrid>().Grid;
            
            aStar = new AStarPathfinding(Grid);
            
            

        }

        public List<AStarPathfinding.Node> FindPath(Vector2 startPos, Vector2 endPos)
        {
           return aStar.FindPath(startPos, endPos);
        }
        
        

       
    }
}