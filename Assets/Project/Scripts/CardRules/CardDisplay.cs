using DG.Tweening;
using GGJ20.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace GGJ20.CardRules
{
    public class CardDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [InjectOptional]
        public Card Card { get; private set; }
        [Inject]
        private ICardDisplayListener playerHand;


        [SerializeField]
        private Button button;
        [SerializeField]
        private Image art;
        [SerializeField]
        private Image shapeArt;
        [SerializeField]
        private Image bgArt;
        [SerializeField]
        private Text manaCost;
        [SerializeField]
        private Text powerDisplay;
        [SerializeField]
        private CanvasGroup cg;
        [SerializeField]
        private bool draggable;

        public UnityEvent DragBegin;
        public UnityEvent DragCancel;
        public UnityEvent DragSubmit;

        private bool listenForMana;
        private bool dragging;

        [Inject]
        private void Init()
        {
            //if (Card != null)
            //    SetCard(Card);

            button.onClick.AddListener(OnSelected);
        }

        public void SetCard(Card card)
        {
            this.Card = card;
            UpdateToMatchCard();
        }


        private void UpdateToMatchCard()
        {
            this.art.sprite = Card.Art;
            this.shapeArt.sprite = Card.ShapeArt;
            this.bgArt.sprite = Card.Background;
            this.manaCost.text = Card.ManaCost.ToString();
            this.powerDisplay.text = Card.GetPowerText();
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
            if (!draggable || !cg.interactable)
                return;
            if (eventData.pointerId == 0 || eventData.pointerId == -1)
            {
                DragBegin.Invoke();
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
                if(playerHand.OnConfirmed(this))
                {
                    DragSubmit.Invoke();
                } else
                {
                    DragCancel.Invoke();
                }
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
