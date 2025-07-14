using System;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DeTach.EditorDT
{
    [CustomEditor(typeof(BaseVariable))]
    public class VariableInspector<TVariable, T, TEvent> : Editor where TEvent : GenericEvent<T>
                                                              where TVariable : GenericVariable<T, TEvent>
    {
        const string DOCUMENTATION_PROPERTY = "documentation";

        CompletePropertyField<UnityEngine.Object> eventField;
        CompletePropertyField<T> initialVal;
        CompletePropertyField<T> currentVal;

        PlayModeInfos playModeInfos;



        public VariableInspector()
        {
            playModeInfos = new PlayModeInfos();
            playModeInfos.OnChangeIsPlaying += OnChangeIsPlaying;
        }

        private void OnDestroy()
        {
            playModeInfos.Dispose();
        }

        private void OnChangeIsPlaying(bool isInPlayMode)
        {
            if (initialVal != null)
            {
                eventField.PropertyField.enabledSelf = !isInPlayMode;

                initialVal.PropertyField.enabledSelf = !isInPlayMode;

                currentVal.PropertyField.enabledSelf = isInPlayMode;
                currentVal.PropertyField.style.display = isInPlayMode ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            root.Bind(serializedObject);

            TVariable variable = target as TVariable;

            /* --- Scriptable Obj --- */
            ObjectField scriptableField = new ObjectField();
            scriptableField.value = variable;
            scriptableField.enabledSelf = false;
            root.Add(scriptableField);

            /* --- Callback --- */
            VisualElement callbackRoot = new VisualElement()
            {
                style =
            {
                flexDirection = FlexDirection.Row
            }
            };

            root.Add(callbackRoot);

            var createEventButton = new Button() { text = "+" };

            createEventButton.clickable.clicked += () =>
            {
                eventField.BaseField.value = CreateEvent();
            };

            var eventPropField = new PropertyField(serializedObject.FindProperty("OnChangeValue"));
            eventPropField.style.minWidth = 300;
            eventField = new CompletePropertyField<UnityEngine.Object>(eventPropField, evt =>
            {
                createEventButton.style.display = evt == null ? DisplayStyle.Flex : DisplayStyle.None;
            });
            callbackRoot.Add(eventPropField);


            callbackRoot.Add(createEventButton);

            /* --- Initial Value --- */
            var initialValuePropField = new PropertyField(serializedObject.FindProperty("initialValue"));
            initialVal = new CompletePropertyField<T>(initialValuePropField, value =>
            {
                if (currentVal != null && currentVal.BaseField != null)
                    currentVal.BaseField.value = value;
            });
            root.Add(initialValuePropField);

            /* --- Current Value --- */
            var currentValue = new PropertyField(serializedObject.FindProperty("value"));
            currentVal = new CompletePropertyField<T>(currentValue);
            root.Add(currentValue);

            /* --- Documentation --- */
            var foldout = new Foldout() { text = "Documentation" };
            foldout.style.marginTop = 20;
            foldout.style.marginLeft = 10;
            foldout.value = !string.IsNullOrWhiteSpace(serializedObject.FindProperty(DOCUMENTATION_PROPERTY).stringValue);

            var textField = new TextField() { multiline = true };
            textField.bindingPath = DOCUMENTATION_PROPERTY;

            foldout.Add(textField);
            root.Add(foldout);


            OnChangeIsPlaying(playModeInfos.IsInPlayMode);

            return root;
        }


        private TEvent CreateEvent()
        {
            var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(target));

            string eventName = $"On{target.name}Changed";
            var newEvent = ScriptableObject.CreateInstance<TEvent>();
            newEvent.name = eventName;

            AssetDatabase.CreateAsset(newEvent, Path.Combine(path, eventName + ".asset"));
            EditorGUIUtility.PingObject(newEvent);

            return newEvent;
        }

        public static void AddScriptField(VisualElement root, SerializedObject serializedObject)
        {
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            var scriptField = new PropertyField(scriptProp);
            scriptField.SetEnabled(false); // Disable so user can't change script
            root.Add(scriptField);
        }


    }

    /// <summary>
    /// This class holds a reference to a property field, and its BaseField value.
    /// This way we can access the T value of the field
    /// </summary>
    class CompletePropertyField<T>
    {
        public BaseField<T> BaseField { get; set; }

        public PropertyField PropertyField { get; set; }

        public CompletePropertyField(PropertyField field, Action<T> OnChangeBaseFieldValue = null)
        {
            PropertyField = field;

            PropertyField.RegisterCallback<AttachToPanelEvent>(evt =>
            {
                BaseField = field.Q<BaseField<T>>();

                if (BaseField == null)
                    return;

                OnChangeBaseFieldValue?.Invoke(BaseField.value);

                BaseField.RegisterValueChangedCallback(evt =>
                {
                    OnChangeBaseFieldValue?.Invoke(evt.newValue);
                });
            });

        }
    }
}
