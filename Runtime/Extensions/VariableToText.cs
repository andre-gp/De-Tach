using UnityEngine;

namespace DeTach
{
    public class VariableToText : MonoBehaviour
    {
        [SerializeField] BaseVariable variable;
        [SerializeField] StringUnityEvent listeners;


        private void OnEnable()
        {
            if (variable != null)
            {
                variable.GetBaseEvent().OnChangeBaseEvent += () => { listeners.Invoke(variable.ValueToString()); };
                listeners.Invoke(variable.ValueToString());
            }
        }

        private void OnDisable()
        {

        }



    }
}
