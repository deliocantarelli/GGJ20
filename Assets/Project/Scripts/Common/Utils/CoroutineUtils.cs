using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ20.Common
{
    public static class CoroutineUtils
    {
        public static IEnumerator WaitThenDo(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
