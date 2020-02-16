using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PointNSheep.Common.Timers
{
    public class Stopwatch
    {
        private float _startTime;

        public Stopwatch() { }
        bool started = false;

        public float ElapsedSeconds { get { return started? Time.time - _startTime : 0; } }

        public void Restart()
        {
            started = true;
            _startTime = Time.time;
        }

        public void ClearAndStop()
        {
            started = false;
        }




        public static Stopwatch CreateAndStart()
        {
            var result = new Stopwatch();
            result.Restart();
            return result;
        }
    }
}
