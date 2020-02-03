using System;
using GGJ20.World;
using UnityEngine;
using Zenject;

namespace GGJ20.CardRules
{
    public class SpellAimCursor : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] sprites;
        [Inject]
        private WorldGrid grid;

        private Vector2[] offsets;

        private void Start()
        {
            GetOffsets();
            Hide();
        }
        public class Factory : PlaceholderFactory<UnityEngine.Object, SpellAimCursor>
        {
        }

        private void GetOffsets()
        {
            offsets = new Vector2[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                GameObject sprite = sprites[i];
                offsets[i] = sprite.transform.position;
            }
        }

        public void ChangeToAndShow(CardDisplay cardDisplay)
        {
            // sprite.sprite = cardDisplay.Card.Art;
            Spell.Description spell = cardDisplay.Card.Spell;
            SetAllCells(spell);
            Show();
        }
        public void Show()
        {
            foreach(GameObject sprite in sprites) {
                sprite.SetActive(true);
            }
        }
        public void Hide()
        {
            foreach(GameObject sprite in sprites) {
                sprite.SetActive(false);
            }
        }

        public void PositionAt(WorldGrid grid, Vector2 gridPos)
        {
            Vector2 position = grid.GridToWorld(gridPos + new Vector2(.5f, .5f));
            for (int i = 0; i < sprites.Length; i++)
            {
                GameObject sprite = sprites[i];
                Vector2 offset = offsets[i];
                sprite.transform.position = position + offset;
            }
        }

        public void SetAllCells(Spell.Description spell) {
            Spell.Hit hit = spell.hits[0];

            Debug.Log(grid != null);
            // spellElsPool.Spawn(this, hit, pos);

            // GridPos = pos;
            // pat = grid.GridPattern(pos);
            // transform.position = grid.GridToWorld(pos, WorldGrid.PlaceMode.TileCenter);
        }

    }
}