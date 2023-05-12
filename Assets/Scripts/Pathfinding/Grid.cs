using System.Collections.Generic;
using Pathfinding;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileData = Pathfinding.TileData;

namespace NPCS.Schedule
{
    public class GridObject
    {
        public AStarPathfinding.Node Node;

        public GameObject Tile;
    }
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }

        public float CellSize { get; }

        private float HalfCellSize { get; }

        float colliderSize = 0.2f;

        public GridObject[,] GridArray { get; }
        
        TileData tileData;
        
        Tilemap tilemap;

        private Vector2 centerPosition;

        private Vector2 bottomLeftPosition;
        public Grid(int width, int height, float cellSize, GameObject tilePrefab, TileData tileData, Tilemap tilemap, Vector2 centerPosition)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            HalfCellSize = cellSize * 0.5f;
            this.tileData = tileData;
     
            this.tilemap = tilemap;
            this.centerPosition = centerPosition;

            GridArray = new GridObject[width, height];
            bottomLeftPosition = new Vector2(
              -CellSize * (Width - 1) / 2,
              -CellSize * (Height - 1) / 2) ;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
               
                    Vector2 cellPosition = GetCellPosition( x, y);
                ;
                    GridArray[x, y] = new GridObject();
                    GridArray[x, y].Node = new AStarPathfinding.Node(new Vector2(x, y), 0);

                    GridArray[x, y].Node.Position = cellPosition;
         
                    var spawnedTile = Object.Instantiate(tilePrefab, GridArray[x,y].Node.Position, Quaternion.identity);
                    GridArray[x, y].Tile = spawnedTile;
                    GridArray[x, y].Node.IsWalkable = IsWalkable(GridArray[x,y].Node.Position);
                    GridArray[x, y].Node.Walkability = Walkability(GridArray[x,y].Node.Position);

                    if (GridArray[x, y].Node.IsWalkable)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().color = new Color(0.19f, 0.3f, 1f, 0.05f);
                    }
                         
                    if (GridArray[x, y].Node.IsBuildingTransition)
                    {
                         spawnedTile.GetComponent<SpriteRenderer>().color = new Color(0.05f, 1f, 0.22f, 0.09f);
                    }
                    
                }
            }
            // If there is a collider on the grid that is a square we can not walk on 
        
        }

        private Vector2 GetCellPosition( int x, int y)
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

        private float Walkability(Vector2 nodePosition)
        {
            int gridIndexX = Mathf.FloorToInt(nodePosition.x / CellSize - HalfCellSize);
            int gridIndexY = Mathf.FloorToInt(nodePosition.y / CellSize - HalfCellSize);
            Vector3Int convertedGridPos = new Vector3Int((int)gridIndexX, (int)gridIndexY, 0);
            // Need a list of tile base that holds a reference to all the path tiles
            return GetTileBase(convertedGridPos) ? Random.Range(0.3f, 0.8f) : 1f;
        }

        private bool GetTileBase(Vector3Int gridPos)
        {
            TileBase tile = tilemap.GetTile(gridPos);
            return tileData.tiles.Contains(tile);
        }


        private bool IsWalkable(Vector2 pos)
        {
            Vector2 gridPos = GetGridPosition(pos);
            // Check if the position is within the bounds of the map
            if (gridPos.x < 0 || gridPos.x >= Width || gridPos.y < 0 || gridPos.y >= Height)
            {
                Debug.Log("Not within bounds");
                return false;
            }

            // Check if there is an obstacle at the position
            Collider2D obstacle = Physics2D.OverlapBox(pos, new Vector2(colliderSize, colliderSize), 0);
            if (obstacle != null && !obstacle.CompareTag("NPC")) // if your NPCs have colliders add tags to make sure the grid square they spawn on is considered walkable
            {
                return false;
            }

            // Position is walkable
            return true;
        }
        

    }
}
