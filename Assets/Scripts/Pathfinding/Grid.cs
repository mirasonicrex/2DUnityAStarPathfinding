using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace NPCS.Schedule
{
    public class GridObject
    {
        public AStarPathfinding.Node node;

        public GameObject tile;
    }
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }

        public float CellSize { get; }

        float colliderSize = 0.2f;

        public GridObject[,] GridArray { get; }

        PathTileMapReferences pathTileRefs;

        private Vector2 centerPosition;

        private Vector2 bottomLeftPosition;
        public Grid(int width, int height, float cellSize, GameObject tilePrefab, Vector2 centerPosition)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
 
            this.centerPosition = centerPosition;
            GridArray = new GridObject[width, height];
            bottomLeftPosition = new Vector2(
              -CellSize * (Width - 1) / 2,
              -CellSize * (Height - 1) / 2) ;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2 cellPosition = GetCellPosition(bottomLeftPosition, x, y);
                    GridArray[x, y] = new GridObject();
                    GridArray[x, y].node = new AStarPathfinding.Node(new Vector2(x, y), 0);

                    GridArray[x, y].node.Position = cellPosition;

                    var spawnedTile = Object.Instantiate(tilePrefab, GridArray[x,y].node.Position, Quaternion.identity);
                    GridArray[x, y].tile = spawnedTile;
                    GridArray[x, y].node.IsWalkable = IsWalkable(GridArray[x,y].node.Position);

                    if (GridArray[x, y].node.IsWalkable)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().color = new Color(0.19f, 0.3f, 1f, 0.05f);
                    }
                        
                    if (GridArray[x, y].node.IsBuildingTransition)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().color = new Color(0.05f, 1f, 0.22f, 0.09f);
                    }
                    
                }
            }

        }

        private Vector2 GetCellPosition(Vector2 bottomLeftPosition, int x, int y)
        {
            
            Vector2 newPos = (bottomLeftPosition + new Vector2(x * CellSize, y * CellSize) )+ new Vector2(centerPosition.x * CellSize, centerPosition.y * CellSize);

            return newPos;

        }

        public Vector2 GetGridPosition(Vector2 worldPosition)
        { 

           // Subtract centerPosition from worldPosition to account for the grid's center position
           Vector2 adjustedWorldPosition = worldPosition -  new Vector2(centerPosition.x * CellSize, centerPosition.y * CellSize);
    
           int gridIndexX = Mathf.FloorToInt(adjustedWorldPosition.x / CellSize + (Width - 1) / 2f);
           int gridIndexY = Mathf.FloorToInt(adjustedWorldPosition.y / CellSize + (Height - 1) / 2f);
           Vector2 gridPosition = new Vector2(gridIndexX, gridIndexY);
           
           return gridPosition;
        }


        public bool IsWalkable(Vector2 pos)
        {
            Vector2 gridPos = GetGridPosition(pos);
            // Check if the position is within the bounds of the map
            if (gridPos.x < 0 || gridPos.x >= Width || gridPos.y < 0 || gridPos.y >= Height)
            {
                return false;
            }

            // Check if there is an obstacle at the position
            Collider2D obstacle = Physics2D.OverlapBox(pos, new Vector2(colliderSize, colliderSize), 0);
            if (obstacle != null)
            {
                return false;
            }

            // Position is walkable
            return true;
        }
        

    }
}
