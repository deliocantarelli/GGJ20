using System;
using UnityEngine;

namespace GGJ20.World
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField]
        Vector2 scale = new Vector2(.5f, .5f);

        [SerializeField]
        Vector2Int topLeft = new Vector2Int(-8, 15);
        [SerializeField]
        Vector2Int bottomRight = new Vector2Int(8, -9);

        public enum SnapMode
        {
            None, Tile, Round, Ceil, Floor
        }

        public enum PlaceMode
        {
            Axis, TileCenter
        }
        public Vector2 WorldToGrid(Vector3 pos, SnapMode snapMode = SnapMode.None)
        {
            pos = transform.InverseTransformPoint(pos);
            pos = pos / scale;

            switch (snapMode)
            {
                case SnapMode.None:
                    break;
                case SnapMode.Round:
                    pos.x = Mathf.Round(pos.x);
                    pos.y = Mathf.Round(pos.y);
                    break;
                case SnapMode.Ceil:
                    pos.x = Mathf.Ceil(pos.x);
                    pos.y = Mathf.Ceil(pos.y);
                    break;
                case SnapMode.Tile:
                case SnapMode.Floor:
                    pos.x = Mathf.Floor(pos.x);
                    pos.y = Mathf.Floor(pos.y);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return pos;
        }
        public Vector3 GridToWorld(Vector2 pos, PlaceMode placeMode = PlaceMode.Axis)
        {
            switch (placeMode)
            {
                case PlaceMode.Axis:
                    break;
                case PlaceMode.TileCenter:
                    pos = pos + Vector2.one * 0.5f;
                    break;
                default:
                    throw new NotImplementedException();
            }
            pos = transform.TransformPoint(pos);
            return pos * scale;
        }

        public bool IsInGrid(Vector2 gridPos)
        {
            return gridPos.x > topLeft.x && gridPos.x < bottomRight.x
                && gridPos.y < topLeft.y && gridPos.y > bottomRight.y;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector2 avg = 0.5f * (GridToWorld(topLeft) + GridToWorld(bottomRight));
            Vector2 span = GridToWorld(topLeft) - GridToWorld(bottomRight);
            span.x = Mathf.Abs(span.x);
            span.y = Mathf.Abs(span.y);
            Gizmos.DrawWireCube(avg, span);
        }
    }
}