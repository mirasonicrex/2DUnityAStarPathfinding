using System.Collections.Generic;
using UnityEngine;
using Grid = NPCS.Schedule.Grid;

namespace Pathfinding
{
    public class AStarPathfinding
    {
        public class Node
        {
            public Vector2 Position { get; set; }
            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost { get; set; }
            public Node Parent { get; set; }

            public bool IsWalkable { get; set; }
            public float Walkability { get; set; }
        
            public bool IsBuildingTransition { get; set; }
            public Node(Vector2 pos, int c)
            {
                Position = pos;
                GCost = c;
                HCost = 0;
                FCost = 0;
                Parent = null;
                IsWalkable = true;
                Walkability = 1; // Preference to nodes with lower weights 
                IsBuildingTransition = false; 
            }


        }

        private Grid grid;
        private List<Node> openList;
        private HashSet<Node> closedList;
    
        private int mapWidth;
        private int mapHeight;
        private float cellSize;
        public AStarPathfinding(Grid grid)
        {
            this.grid = grid;
            mapWidth = grid.Width;
            mapHeight = grid.Height;
            cellSize = grid.CellSize;
        }

        public List<Node> FindPath(Vector2 startPos, Vector2 endPos)
        {
  
            openList = new List<Node>(); // nodes to be evaluated
            closedList = new HashSet<Node>(); // nodes already evaluated
        
            // Set the start and end nodes
            Node startNode = grid.GridArray[(int)startPos.x, (int)startPos.y].Node;
            startNode.HCost = CalculateHeuristic(startPos, endPos);
            startNode.FCost = startNode.GCost + startNode.HCost;
            Node endNode = grid.GridArray[(int)endPos.x, (int)endPos.y].Node;
   
            // For Debugging
            UpdateStartEndVisualization(startNode, endNode);  
       

            // Add the start node to the open list
            openList.Add(startNode);
    
            // Begin the A* algorithm
            while (openList.Count > 0)
            {
                Node currentNode = GetLowestCostNode();

                if (currentNode == endNode)
                {
                    // Path has been found, create a list of nodes to represent the path
                    List<Node> path = new List<Node>();

                    while (currentNode != startNode)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode.Parent;
                    }

                    path.Reverse();
                    // Use the path to move the player or other game object
                    VisualizePath(path);
                    return path;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    // if neighbor is not traversable or is in the closed list skip to the next neighbor
                    if (closedList.Contains(neighbor)) continue;
                    if (!neighbor.IsWalkable)
                    {
                        continue;
                    }

                    int newCost = currentNode.GCost + CalculateDistance(currentNode, neighbor);

                    // if new path to neighbor is shorter or neighbor is not in the open list 
                    if (newCost < neighbor.GCost || !openList.Contains(neighbor))
                    {
                        neighbor.GCost = newCost; 
                        neighbor.HCost = CalculateHeuristic(neighbor.Position, endPos); // calculate the new h cost
                        // if the fCosts are equal, break the tie by choosing the node with the lower hCost
           
                        neighbor.FCost = neighbor.GCost + neighbor.HCost;
                        neighbor.Parent = currentNode;
       
                        if (neighbor.FCost == currentNode.FCost)
                        {
                            if (neighbor.HCost < currentNode.HCost)
                            {
                                currentNode = neighbor;
                            }
                        }
                        // if neighbor is not in open list add the neighbor 
                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    } 
                }
            }

            // No path found
            Debug.Log("No path found");
            return null;
        }


        private int CalculateHeuristic(Vector2 neighborPosition, Vector2 endPos)
        {
            // Calculate the Manhattan distance between the neighbor position and the end position
            int xDistance = Mathf.Abs((int)neighborPosition.x - (int)endPos.x);
            int yDistance = Mathf.Abs((int)neighborPosition.y - (int)endPos.y);
            int heuristic = xDistance + yDistance;

            return heuristic;
        }

        private int CalculateDistance(Node a, Node b)
        {
    
            int xDistance = Mathf.Abs((int)a.Position.x - (int)b.Position.x);
            int yDistance = Mathf.Abs((int)a.Position.y - (int)b.Position.y);
            if (xDistance > yDistance)
            {
                int diagonalSteps = Mathf.Min(xDistance, yDistance);
                int straightSteps = Mathf.Abs(xDistance - yDistance);
                return (int)(14 * diagonalSteps * b.Walkability + 10 * straightSteps * b.Walkability);
            }
            else
            {
                int diagonalSteps = Mathf.Min(xDistance, yDistance);
                int straightSteps = Mathf.Abs(xDistance - yDistance);
                return (int)(14 * diagonalSteps * b.Walkability + 10 * straightSteps * b.Walkability);
            }
        }


        private Node GetLowestCostNode()
        {
            Node lowestCostNode = openList[0];
    
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < lowestCostNode.FCost)
                {
                    lowestCostNode = openList[i];
                }
            }
    

            return lowestCostNode;
        }
    
        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
        
            Vector2 gridPos = grid.GetGridPosition(node.Position);
        
            int x = Mathf.RoundToInt(gridPos.x);
            int y = Mathf.RoundToInt(gridPos.y);
        
    
            if (x > 0) neighbors.Add(grid.GridArray[x - 1, y].Node);
            if (x < mapWidth - 1) neighbors.Add(grid.GridArray[x + 1, y].Node);
            if (y > 0) neighbors.Add(grid.GridArray[x, y - 1].Node);
            if (y < mapHeight - 1) neighbors.Add(grid.GridArray[x, y + 1].Node);
            // Check diagonal (optional) 
            if (x > 0 && y > 0) neighbors.Add(grid.GridArray[x - 1, y - 1].Node);
            if (x < mapWidth - 1 && y > 0) neighbors.Add(grid.GridArray[x + 1, y - 1].Node);
            if (x > 0 && y < mapHeight - 1) neighbors.Add(grid.GridArray[x - 1, y + 1].Node);
            if (x < mapWidth - 1 && y < mapHeight - 1) neighbors.Add(grid.GridArray[x + 1, y + 1].Node);
            return neighbors;
        }

    
        // Debugging only 

        private void UpdateStartEndVisualization(Node startNode, Node endNode)
        {
               
            Vector2 startGridPos = grid.GetGridPosition(startNode.Position);
        
            int startX= Mathf.FloorToInt(startGridPos.x);
            int startY = Mathf.FloorToInt(startGridPos.y);
        
            GameObject start = grid.GridArray[startX,startY].Tile;
            start.GetComponent<SpriteRenderer>().color = new Color(1f, 0.11f, 0.88f, 0.84f);
            Vector2 endGridPos = grid.GetGridPosition(endNode.Position);
            int endX = Mathf.FloorToInt(endGridPos.x);
            int endY = Mathf.FloorToInt(endGridPos.y);
            GameObject end = grid.GridArray[endX,endY].Tile;
            end.GetComponent<SpriteRenderer>().color = new Color(1f, 0.11f, 0.88f, 0.84f);
        
        
        }

        public void VisualizePath(List<Node> path)
        {
            if (path != null)
            {
                foreach (Node node in path)
                {
                    Vector2 gridPos = grid.GetGridPosition(node.Position);
                    int x = Mathf.RoundToInt(gridPos.x);
                    int y = Mathf.RoundToInt(gridPos.y);
                    GameObject tile = grid.GridArray[x, y].Tile;
                    // Set the path node color
                    tile.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f);
                }
            }
        }
 
    }
}
