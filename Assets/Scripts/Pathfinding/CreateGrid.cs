using UnityEngine;
using UnityEngine.Tilemaps;
using Grid = NPCS.Schedule.Grid;

namespace Pathfinding
{
   
    public class CreateGrid : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        public Grid Grid { get; private set; }

        [SerializeField] public int width = 100;
        [SerializeField] public int height = 100;

        [SerializeField] public TileData pathTileData;
        [SerializeField] public Tilemap pathTileMap;
        [SerializeField] public Vector2 gridCenter;
        [SerializeField] public float cellSize = 0.64f; 
        private void Awake()
        {
            Grid = new Grid(width, height, cellSize, tilePrefab, pathTileData, pathTileMap, gridCenter);
        }

        // For Debugging
        private void OnDrawGizmos()
        {
            if (Grid != null)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if(!Grid.GridArray[x,y].Node.IsWalkable)
                        {
                   
                            Gizmos.color = Color.red;
                     
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].Node.Position, new Vector3(0.2f, 0.2f, 0));
                        }
                        else if (Grid.GridArray[x, y].Node.Walkability < 0.8f)
                        {
                            Gizmos.color = Color.black;
                     
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].Node.Position, new Vector3(0.2f,0.2f, 0));
                        }
                        else
                        {
                            Gizmos.color = Color.blue;
                  
                            Gizmos.DrawWireCube(Grid.GridArray[x,y].Node.Position, new Vector3(0.2f, 0.2f, 0));
                        }
                    
                    }
                }
            }
        }
    }
}