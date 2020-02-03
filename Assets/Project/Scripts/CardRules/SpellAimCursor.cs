using System;
using System.Collections.Generic;
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

        [SerializeField]
        private GameObject floorPrefab;

        private List<GameObject> aimFLoors;
        private Spell.Description currentSpell;

        private Vector3 FLOOR_OFFSET = new Vector2(-0.25f, -0.25f);

        private void Start()
        {
            GetOffsets();
            Hide();
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
            currentSpell = spell;
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
            RemoveCells();
        }

        public void PositionAt(WorldGrid grid, Vector2 gridPos)
        {
            Vector2 position = grid.GridToWorld(gridPos + new Vector2(.5f, .5f));
            for (int i = 0; i < sprites.Length; i++)
            {
                GameObject sprite = sprites[i];
                if(offsets == null)
                {
                    continue;
                }
                Vector2 offset = offsets[i];
                sprite.transform.position = position + offset;
            }
            PositionAllCells(currentSpell, position);
        }

        public void RemoveCells()
        {
            if (aimFLoors != null)
            {
                foreach (GameObject go in aimFLoors)
                {
                    Destroy(go);
                }
            }
            aimFLoors = null;
        }
        public void SetAllCells(Spell.Description spell, Vector3 offset) {
            Spell.Hit hit = spell.hits[0];

            RemoveCells();

            aimFLoors = new List<GameObject>();


            foreach (Vector2Int pos in hit.Locations)
            {
                GameObject floor = Instantiate(floorPrefab);
                aimFLoors.Add(floor);
            }
        }

        public void PositionAllCells(Spell.Description spell, Vector3 offset)
        {
            Spell.Hit hit = spell.hits[0];
            if(aimFLoors == null || hit.Locations.Count != aimFLoors.Count) {
                SetAllCells(spell, offset);
            }

            for (int i = 0; i < aimFLoors.Count; i++)
            {
                Vector2Int pos = hit.Locations[i];
                GameObject floor = aimFLoors[i];
                if(floor == null) {
                    continue;
                }
                
                //pat = grid.GridPattern(pos);
                Vector3 floorPos = grid.GridToWorld(pos, WorldGrid.PlaceMode.TileCenter) + offset;
                floor.transform.position = floorPos + FLOOR_OFFSET;
            }
        }

    }
}