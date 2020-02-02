using System;
using GGJ20.World;
using UnityEngine;

namespace GGJ20.CardRules
{
    public class SpellAimCursor : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprite;

        private void Start()
        {
            Hide();
        }
        public void ChangeToAndShow(CardDisplay cardDisplay)
        {
            sprite.sprite = cardDisplay.Card.Art;
            Show();
        }
        public void Show()
        {
            sprite.enabled = true;
        }
        public void Hide()
        {
            sprite.enabled = false;
        }

        public void PositionAt(WorldGrid grid, Vector2 gridPos)
        {
            transform.position = grid.GridToWorld(gridPos + new Vector2(.5f, .5f));
        }

    }
}