using PointNSheep.Common.Grid;
using System;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class SpellAimController : MonoBehaviour
    {
        [SerializeField]
        private SpellAimCursor cursor;
        

        private bool aiming = false;
        private CardDisplay aimedCard;
        private WorldGrid grid;
        private Spell.Factory spellFactory;

        public Vector2Int gridPos { get; private set; }
        public bool IsGridPosValid { get { return grid.IsInGrid(gridPos); } }

        [Inject]
        private void Init(WorldGrid grid, Spell.Factory spellFactory)
        {
            this.grid = grid;
            this.spellFactory = spellFactory;
            cursor.Hide();
        }

        public void StartAiming(CardDisplay cardDisplay)
        {
            aiming = true;
            aimedCard = cardDisplay;
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
                var p = grid.WorldToGrid(pos, WorldGrid.SnapMode.Tile);
                gridPos = new Vector2Int((int)p.x, (int)p.y);

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

        public Spell SpawnSpell()
        {
            return spellFactory.Create(aimedCard.Card, gridPos);
        }
    }
}