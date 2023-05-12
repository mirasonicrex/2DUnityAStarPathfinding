using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinding
{
    [
        CreateAssetMenu(
            fileName = "TileData",
            menuName = "Data/TileData",
            order = 0)
    ]
    public class TileData : ScriptableObject
    {
        public List<TileBase> tiles; // Any tiles that you want to add weights to

    }
}
