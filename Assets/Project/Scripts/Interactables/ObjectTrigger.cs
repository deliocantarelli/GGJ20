using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AntonioHR.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class ObjectTrigger<T> : MonoBehaviour
    {
        [SerializeField]
        private bool allowMultipleTriggers = false;

        private bool triggered;

        private void Awake()
        {
            Debug.Assert(gameObject.GetComponentsInChildren<Collider>().Any(x=>x.isTrigger));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!allowMultipleTriggers && triggered)
                return;

            var target = other.GetComponent<T>();

            if(target == null)
            {
                var proxy = other.GetComponent<IProxyFor<T>>();
                if (proxy != null)
                    target = proxy.Owner;
            }

            if(target != null)
            {
                triggered = true;
                OnTriggered(target);
            }
        }

        protected abstract void OnTriggered(T obj);

        public void Reset()
        {
            triggered = false;
        }
    }
}
