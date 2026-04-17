using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DeTach
{
    public abstract class GenericEvent<T> : BaseEvent
    {
        protected event Action<T> onChange;

#if UNITY_EDITOR
        [SerializeField] T invokeVal;
#endif

        public void Invoke(T value)
        {
            base.Invoke();

            onChange?.Invoke(value);
        }

        public event Action<T> OnChange
        {
            add
            {

                onChange += value;

#if UNITY_EDITOR
                Listeners.Add(value.Target as UnityEngine.Object);
#endif
            }
            remove
            {
                onChange -= value;

#if UNITY_EDITOR
                Listeners.Remove(value.Target as UnityEngine.Object);
#endif
            }
        }

        private void UnregisterAll()
        {
            onChange = null;

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
                UnregisterAll();
            }
        }
#endif
    }


}
