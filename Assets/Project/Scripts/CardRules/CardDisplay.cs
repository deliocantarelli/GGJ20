using DG.Tweening;
using GGJ20.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace GGJ20.CardRules
{
    public class CardDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public Card Card { get; private set; }
        private Player player;
        [Inject]
        private ICardDisplayListener playerHand;


        [SerializeField]
        private Button button;
        [SerializeField]
        private Image art;
        [SerializeField]
        private Text manaCost;
        [SerializeField]
        private CanvasGroup cg;
        [SerializeField]
        private bool draggable;


        private bool dragging;

        [Inject]
        private void Init(Card card, Player player)
        {
            SetCard(card);
            this.player = player;

            player.UsableManaChanged += CheckPlayable;
            button.onClick.AddListener(OnSelected);
            CheckPlayable(player);
        }
        private void OnDestroy()
        {
            player.UsableManaChanged -= CheckPlayable;
        }


        public void SetCard(Card card)
        {
            this.Card = card;
            UpdateToMatchCard();
        }


        private void CheckPlayable(Player player)
        {
            cg.interactable = player.CanPlayCard(Card);
        }
        private void UpdateToMatchCard()
        {
            this.art.sprite = Card.Art;
            this.manaCost.text = Card.ManaCost.ToString();
        }
        private void OnSelected()
        {
            playerHand.OnSelected(this);
        }

        public void Deselect()
        {
            ShowRelease();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!draggable)
                return;
            if (eventData.pointerId == 0 || eventData.pointerId == -1)
            {
                dragging = true;
                playerHand.OnSelected(this);
            }
        }
        public void OnDrag(PointerEventData eventData)
        { }


        private void Update()
        {
            if (dragging && Input.GetKeyUp(KeyCode.Mouse0))
            {
                dragging = false;
                playerHand.OnConfirmed(this);
            }
        }


        public IEnumerator UsageAnimationCoroutine()
        {
            cg.alpha = 1;
            Tween t = cg.DOFade(0, .3f).SetLoops(5, LoopType.Yoyo);

            yield return new WaitUntil(() => !t.IsPlaying());
        }
        public IEnumerator NewCardCoroutine()
        {
            Tween t = cg.DOFade(1, .5f);

            yield return new WaitUntil(() => !t.IsPlaying());
        }
        public void ShowHeld()
        {
            cg.alpha = .5f;
        }
        public void ShowRelease()
        {
            cg.alpha = 1;
        }
    }
}
