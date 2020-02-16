using DG.Tweening;
using GGJ20.CardRules;
using GGJ20.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public  abstract class CardPickerPanel : MonoBehaviour, ICardDisplayListener
    {
        protected BattleSceneController scene;
        protected GameStateController game;
        [SerializeField]
        protected CanvasGroup cg;

        protected List<CardDisplay> displays; 


        [Inject]
        private void Inject(BattleSceneController scene, GameStateController game)
        {
            this.scene = scene;
            this.game = game;
            cg = GetComponent<CanvasGroup>();

            cg.alpha = 0;
            cg.interactable = false;

            displays = GetComponentsInChildren<CardDisplay>().ToList();
            int i = 0;
            foreach (var card in GenerateCards())
            {
                displays[i].SetCard(card);
                i++;
            }
            OnInit();
        }

        protected abstract void OnInit();

        protected abstract IEnumerable<Card> GenerateCards();


        protected void Show()
        {
            cg.interactable = true;
            cg.alpha = 1;
            GetPanelTransform().DOMoveY(-1000, .5f).From();
        }

        private Transform GetPanelTransform()
        {
            return transform.GetChild(0);
        }

        public bool OnConfirmed(CardDisplay cardDisplay)
        {
            return true;
        }

        public void OnSelected(CardDisplay cardDisplay)
        {
            cg.interactable = false;
            GetPanelTransform().DOMoveY(-1000, .5f)
                .OnComplete(() =>
            {
                gameObject.SetActive(false);
                CardPicked(cardDisplay);
            });
        }

        protected abstract void CardPicked(CardDisplay cardDisplay);
    }
}
