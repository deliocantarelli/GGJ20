using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace PointNSheep.Common.Audio
{ 
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        AudioMixerSnapshot game;
        [SerializeField]
        AudioMixerSnapshot menu;
        [SerializeField]
        AudioMixer mixer;
        [SerializeField]
        AudioSource source;

        private float transitionTime = .5f;

        public void OnMenu()
        {
            mixer.TransitionToSnapshots(new[] { menu }, new float[] { 1 }, transitionTime);
        }
        public void OnGame()
        {
            mixer.TransitionToSnapshots(new[] { game }, new float[] { 1 }, transitionTime);
        }
    }
}
