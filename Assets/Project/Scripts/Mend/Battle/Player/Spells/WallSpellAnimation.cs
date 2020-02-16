using PointNSheep.Common.Grid;
using PointNSheep.Common.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    [RequireComponent(typeof(SpellElement))]
    public class WallSpellAnimation : MonoBehaviour
    {
        private Animator animator;
        private SpellElement owner;
        private Stopwatch timer = new Stopwatch();

        [Inject]
        private WorldGrid grid;
        [Inject]
        private void Init()
        {
            owner = GetComponent<SpellElement>();
            animator = GetComponent<Animator>();
            owner.OnSetup.AddListener(Animate);
        }

        private void Animate()
        {
            var pat = grid.GridPattern(owner.GridPos);
            timer.Restart();

            switch (pat)
            {
                case WorldGrid.Pattern.Light:
                    animator.SetInteger("Pattern", 0);
                    break;
                case WorldGrid.Pattern.Dark:
                    animator.SetInteger("Pattern", 1);
                    break;
                default:
                    throw new NotImplementedException();
            }
            animator.SetTrigger("Rise");
        }
        private void Update()
        {
            if (timer.ElapsedSeconds > owner.WallDuration)
            {
                owner.OnLower();
                animator.SetTrigger("Lower");
            }
        }
    }
}
