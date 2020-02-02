using System;
using GGJ20.World;
using UnityEngine;

namespace GGJ20.CardRules
{
    public class SpellAimCursor : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] sprites;

        private Vector2[] offsets;

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

    }
}