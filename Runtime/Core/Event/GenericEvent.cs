using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
    public abstract class GenericEvent<T> : BaseEvent
    {
        protected event Action<T> OnChange;

        [SerializeField] protected T lastValue;

#if UNITY_EDITOR
        [SerializeField] T invokeVal;
#endif

        public void Invoke(T value)
        {
            base.Invoke();

            OnChange?.Invoke(value);

            lastValue = value;
        }

        public void Add(Action<T> action)
        {
            if (lastValue != null)
            {
                action.Invoke(lastValue);
            }

            OnChange += action;

#if UNITY_EDITOR
            Listeners.Add(action.Target as UnityEngine.Object);
#endif
        }

        public void Remove(Action<T> action)
        {
            OnChange -= action;

#if UNITY_EDITOR
            Listeners.Remove(action.Target as UnityEngine.Object);
#endif
        }

        private void UnregisterAll()
        {
            OnChange = null;

#if UNITY_EDITOR
            Listeners.Clear();
#endif
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnChangePlayModeState;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnChangePlayModeState;
        }

        private void OnChangePlayModeState(PlayModeStateChange change)
        {
            // We reset the listeners BEFORE starting the game
            if (change == PlayModeStateChange.ExitingEditMode ||
               change == PlayModeStateChange.EnteredEditMode)
            {
                lastValue = default;
                UnregisterAll();
            }
        }
#endif
    }


}
