using Unity.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace DeTach.EditorDT
{
    [CustomEditor(typeof(BaseListener))]
    public class ListenerInspector<T, TEvent, TVariable, TUnityEvent, TListener> : Editor 
                                                    where TEvent : GenericEvent<T>
                                                    where TVariable : GenericVariable<T,TEvent>
                                                    where TUnityEvent : UnityEvent<T>
                                                    where TListener : GenericListener<T, TEvent, TVariable, TUnityEvent>
    {
        public override VisualElement CreateInspectorGUI()
        {            
            TListener listener = target as TListener;

            VisualElement root = new VisualElement();
            root.dataSource = listener;


            VisualElement detachObjectRoot = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    marginTop = 5,
                    marginBottom = 10
                }
            };
            root.Add(detachObjectRoot);

            /* --- DETACH OBJECT --- */

            var detachObjectField = new ObjectField();
            detachObjectField.SetBinding(nameof(ObjectField.value), new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(listener.deTachObject)),
                bindingMode = BindingMode.TwoWay
            });
            



            /* --- TYPE --- */
            var typeField = new EnumField(DeTachType.Variable)
            {
                style = { width = 70, marginRight = 5 }
            };

            typeField.SetBinding(nameof(EnumField.value), new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(listener.type)),
                bindingMode = BindingMode.TwoWay
            });

            typeField.RegisterValueChangedCallback(evt =>
            {
                DeTachType detachType = (DeTachType)evt.newValue;
                RefreshObjectFieldType(detachObjectField, detachType);
            });

            detachObjectRoot.Add(typeField);

            detachObjectRoot.Add(detachObjectField);

            RefreshObjectFieldType(detachObjectField, (DeTachType)typeField.value);

            /* --- LISTENERS --- */
            root.Add(new PropertyField(serializedObject.FindProperty("Responses")));

            return root;
        }

        private static void RefreshObjectFieldType(ObjectField detachObjectField, DeTachType detachType)
        {
            System.Type type = detachType == DeTachType.Variable ? typeof(TVariable) : typeof(TEvent);

            detachObjectField.objectType = type;

            if (!type.IsInstanceOfType(detachObjectField.value))
            {
                //detachObjectField.value = null;
            }
        }
    }
}
