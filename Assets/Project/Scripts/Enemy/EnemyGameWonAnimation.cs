

using UnityEngine;

namespace GGJ20.Enemy {
    [RequireComponent(typeof(Animator))]
    public class EnemyGameWonAnimation : MonoBehaviour {
        void OnEnable() {
            Animator animator = GetComponent<Animator>();

            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            if(clips.Length == 0) {
                return;
            }
            int index = Random.Range(0, clips.Length);

            string animationName = clips[index].name;

            animator.Play(animationName);
        }
    }
}