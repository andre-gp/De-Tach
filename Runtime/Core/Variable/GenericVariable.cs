using UnityEditor;
using UnityEngine;

namespace DeTach
{
    public class GenericVariable<T, TEvent> : BaseVariable where TEvent : GenericEvent<T>
    {
        [SerializeField] TEvent OnChangeValue;
        [SerializeField] T initialValue;
        [SerializeField] T value;

        [SerializeField] string documentation;

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                this.OnChangeValue?.Invoke(value);
            }
        }

        public void InvokeValueChange()
        {
            this.OnChangeValue?.Invoke(value);
        }

        public override string ValueToString()
        {
            return this.value.ToString();
        }

        public override BaseEvent GetBaseEvent() { return OnChangeValue; }

        public TEvent Event { get { return OnChangeValue; } }

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
                value = initialValue;
            }
        }
#endif
    }
}
