
using DG.Tweening;
using UnityEngine;

namespace PointNSheep.Mend.Battle {
    [SerializeField]
    public class TileAppear : MonoBehaviour {
        [SerializeField]
        private float seconds;
        [SerializeField]
        private float startDelay;
        private Tween tween;
        void OnEnable()
        {
            Sequence sequence = DOTween.Sequence()
            .AppendInterval(startDelay)
            .Append(transform.DOScale(Vector3.one, seconds));
        }
    }
}