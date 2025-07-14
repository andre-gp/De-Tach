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
    }
}
