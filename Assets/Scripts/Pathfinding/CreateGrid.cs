using UnityEngine;
using UnityEngine.Tilemaps;
using Grid = NPCS.Schedule.Grid;

namespace Pathfinding
{
   
    public class CreateGrid : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;

        [SerializeField]
        private float cellSize = 0.64f; // change according to cell size
        public Grid Grid { get; private set; }
        [field:SerializeField] public int Width { get; private set; }
        [field:SerializeField]  public int Height { get; private set; }
        [field: SerializeField] public Vector2 gridCenter;
        private void Awake()
        {
            Width = 10; 
            Height = 10;
            Grid = new Grid(Width, Height, cellSize, tilePrefab, gridCenter);
        }

        
        // For Debugging purposes
        private void OnDrawGizmos()
        {
            if (Grid != null)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if(!Grid.GridArray[x,y].node.IsWalkable)
                        {
                   
                            Gizmos.color = Color.red;
                     
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].node.Position, new Vector3(0.2f, 0.2f, 0));
                        }
                        else if (Grid.GridArray[x, y].node.Walkability < 0.8f)
                        {
                            Gizmos.color = Color.black;
                     
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].node.Position, new Vector3(0.2f,0.2f, 0));
                        }
                        else
                        {
                            Gizmos.color = Color.blue;
                  
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].node.Position, new Vector3(0.2f, 0.2f, 0));
                        }
                    
                    }
                }
            }
        }
    }
}