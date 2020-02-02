using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GGJ20.Utils
{
    public class Stopwatch
    {
        private float _startTime;

        private Stopwatch() { }

        public float ElapsedSeconds { get { return Time.time - _startTime; } }

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            _startTime = Time.time;
        }




        public static Stopwatch CreateAndStart()
        {
            var result = new Stopwatch();
            result.Start();
            return result;
        }
    }
}
