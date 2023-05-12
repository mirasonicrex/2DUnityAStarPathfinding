using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tilemap = UnityEngine.Tilemaps.Tilemap;
namespace NPCS.Schedule
{
    public class FindPathTileMap : MonoBehaviour
    {
        [SerializeField]
        PathTileMapReferences pathTileMapRefs;

        private Tilemap pathTile;

        private IEnumerator Start()
        {

            yield return null;
           // FindPathTileMaps();
        }

        private void FindPathTileMaps()
        {
            
            // find the path tilemap if it exists in the scene 
            pathTile = GameObject.Find("path").GetComponent<Tilemap>();
            // need to find path 
            if (pathTile == null)
            {
                Debug.Log($"NULL REFERENCE FOR PATHTILE {pathTile}");
                return;
            }
            pathTileMapRefs.Add(pathTile);
            
            // TODO delete extra references 
        } 
    }
}