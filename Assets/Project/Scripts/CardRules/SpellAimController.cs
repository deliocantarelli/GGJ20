using GGJ20.World;
using System;
using UnityEngine;
using Zenject;

namespace GGJ20.CardRules
{
    public class SpellAimController : MonoBehaviour
    {
        [SerializeField]
        private SpellAimCursor cursor;

        private bool aiming = false;

        private WorldGrid grid;
        public Vector2 gridPos { get; private set; }
        public bool IsGridPosValid { get { return grid.IsInGrid(gridPos); } }

        [Inject]
        private void Init(WorldGrid grid)
        {
            this.grid = grid;
            cursor.Hide();
        }

        public void StartAiming(CardDisplay cardDisplay)
        {
            aiming = true;
            cursor.ChangeToAndShow(cardDisplay);
        }

        public void StopAiming()
        {
            aiming = false;
            cursor.Hide();
        }



        private void Update()
        {
            if (aiming)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                gridPos = grid.WorldToGrid(pos, WorldGrid.SnapMode.Tile);

                if (grid.IsInGrid(gridPos))
                {
                    cursor.Show();
                    cursor.PositionAt(grid, gridPos);
                }
                else
                {
                    cursor.Hide();
                }
            }
        }
    }
}