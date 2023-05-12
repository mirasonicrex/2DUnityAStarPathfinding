using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tilemap = UnityEngine.Tilemaps.Tilemap;
namespace NPCS.Schedule
{
    [CreateAssetMenu(fileName = "PathTiles", menuName = "ThisIsTheGameCopy - Copy (2)/TileReferences", order = 0)]
    public class PathTileMapReferences : ScriptableObject
    {
        private List<Tilemap> pathTileMaps;

        public List<Tilemap> PathTileMaps => pathTileMaps; 
        public void Add(Tilemap tilemap)
        {
           // Debug.Log($" Tilemap: {tilemap}");
            pathTileMaps.Add(tilemap);
        }
    }
}