using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
    public class GenericEventListener<T, TEvent, TVariable, TUnityEvent> : MonoBehaviour
                                                                where TEvent : GenericEvent<T>
                                                                where TVariable : GenericVariable<T, TEvent>
                                                                where TUnityEvent : UnityEvent<T>
    {
        //public TEvent Event;

        public TVariable Variable;

        public TUnityEvent Listeners;

        private void OnEnable()
        {
            if (Variable != null)
            {
                if(Variable.Event != null)
                {
                    Variable.Event.Add(OnValueChanged);
                }

                Listeners.Invoke(Variable.Value);
            }
        }

        private void OnDisable()
        {
            if (Variable != null)
            {
                if (Variable.Event != null)
                {
                    Variable.Event.Remove(OnValueChanged);
                }
            }
        }

        private void OnValueChanged(T value)
        {
            Listeners?.Invoke(value);
        }
    }
}
