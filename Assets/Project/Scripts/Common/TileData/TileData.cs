using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PointNSheep.Common.TileData
{
    [System.Serializable]
    public class TileData
    {
        [System.Serializable]
        public struct rowData
        {
            //item inside row
            public bool[] row;
        }

        public rowData[] rows = new rowData[7];

        public List<Vector2Int> GetLocations(Vector2Int center)
        {
            List<Vector2Int> spots = new List<Vector2Int>();

            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < rows[y].row.Length; x++)
                {
                    if (rows[y].row[x])
                        spots.Add(new Vector2Int(x, y) - center);
                }
            }
            return spots;
        }
    }
}