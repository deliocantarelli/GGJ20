using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GGJ20.CardRules
{
    public class CardDisplay : MonoBehaviour
    {
        public Card Card { get; private set; }
        private Player player;
        [Inject]
        private PlayerHandController playerHand;

        [SerializeField]
        private Button button;
        [SerializeField]
        private Image art;
        [SerializeField]
        private Text manaCost;
        [SerializeField]
        private CanvasGroup cg;

        [Inject]
        private void Init(Card card, Player player)
        {
            SetCard(card);
            this.player = player;

            player.UsableManaChanged += CheckPlayable;
            button.onClick.AddListener(OnSelected);
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



        public IEnumerator UsageAnimationCoroutine()
        {
            Tween t = cg.DOFade(0, .3f).SetLoops(5, LoopType.Yoyo);

            yield return new WaitUntil(() => !t.IsPlaying());
        }
        public IEnumerator NewCardCoroutine()
        {
            Tween t = cg.DOFade(1, .5f);

            yield return new WaitUntil(() => !t.IsPlaying());
        }
    }
}
