using System;
using System.IO;
using System.Threading.Tasks;
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
        Button saveButton;

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

                RefreshSaveButtonState();
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            root.Bind(serializedObject);

            TVariable variable = target as TVariable;

            /* --- SCRIPTABLE OBJ --- */
            ObjectField scriptableField = new ObjectField();
            scriptableField.value = variable;
            scriptableField.enabledSelf = false;
            root.Add(scriptableField);

            /* --- CALLBACK --- */
            VisualElement callbackRoot = new VisualElement()
            {
                style =
            {
                flexDirection = FlexDirection.Row
            }
            };

            root.Add(callbackRoot);

            /* --- CREATE EVENT --- */
            var createEventButton = new Button() { text = "+" };

            createEventButton.clickable.clicked += () =>
            {
                if(eventField != null && eventField.BaseField != null)
                    eventField.BaseField.value = CreateEvent();
            };

            /* --- DELETE EVENT --- */

            var deleteEventButton = new Button() { text = "-" };

            deleteEventButton.clickable.clicked += () =>
            {
                if (eventField != null && eventField.BaseField != null)
                {
                    AssetDatabase.RemoveObjectFromAsset(eventField.BaseField.value);
                    Undo.DestroyObjectImmediate(eventField.BaseField.value);
                    AssetDatabase.SaveAssets();

                    eventField.BaseField.value = null;

                    //AssetDatabase.getsub(eventField.BaseField.value);
                }
            };

            var eventPropField = new PropertyField(serializedObject.FindProperty("OnChangeValue"));
            eventPropField.style.minWidth = 300;
            eventField = new CompletePropertyField<UnityEngine.Object>(eventPropField, evt =>
            {
                createEventButton.style.display = evt == null ? DisplayStyle.Flex : DisplayStyle.None;
                deleteEventButton.style.display = evt == null ? DisplayStyle.None : DisplayStyle.Flex;
            });
            callbackRoot.Add(eventPropField);


            callbackRoot.Add(createEventButton);
            callbackRoot.Add(deleteEventButton);

            /* --- Initial Value --- */
            var initialValuePropField = new PropertyField(serializedObject.FindProperty("initialValue"));
            initialVal = new CompletePropertyField<T>(initialValuePropField, value =>
            {
                // Copy the initial value to the current value when not playing.
                if (!EditorApplication.isPlaying && currentVal != null && currentVal.BaseField != null)
                    currentVal.BaseField.value = value;

                RefreshSaveButtonState();
            });
            root.Add(initialValuePropField);

            /* --- CURRENT VALUE --- */
            var currentValue = new PropertyField(serializedObject.FindProperty("value"));
            currentVal = new CompletePropertyField<T>(currentValue, value =>
            {
                if (EditorApplication.isPlaying)
                {
                    variable.InvokeValueChange();
                }

                RefreshSaveButtonState();
            });
            root.Add(currentValue);

            /* --- SAVE BUTTON --- */
            saveButton = new Button(() => { initialVal.BaseField.value = currentVal.BaseField.value; })
            {
                text = "Save Value"
            };
            saveButton.style.maxWidth = 200;
            saveButton.style.alignSelf = Align.Center;
            saveButton.style.marginTop = 10;
            root.Add(saveButton);

            /* --- DOCUMENTATION --- */
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
            var assetPath = AssetDatabase.GetAssetPath(target);
            var directoryPath = Path.GetDirectoryName(assetPath);

            string eventName = $"VarEvent_{target.name}";
            var newEvent = ScriptableObject.CreateInstance<TEvent>();
            newEvent.name = eventName;

            //AssetDatabase.CreateAsset(newEvent, Path.Combine(path, eventName + ".asset"));
            AssetDatabase.AddObjectToAsset(newEvent, assetPath);
            EditorGUIUtility.PingObject(newEvent);

            AssetDatabase.SaveAssets();

            return newEvent;
        }

        public static void AddScriptField(VisualElement root, SerializedObject serializedObject)
        {
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            var scriptField = new PropertyField(scriptProp);
            scriptField.SetEnabled(false); // Disable so user can't change script
            root.Add(scriptField);
        }

        private void RefreshSaveButtonState()
        {
            if (saveButton == null)
                return;

            saveButton.style.display = EditorApplication.isPlaying ? DisplayStyle.Flex : DisplayStyle.None;

            if(initialVal != null && initialVal.BaseField != null && currentVal != null && currentVal.BaseField != null)
            {
                saveButton.enabledSelf = !initialVal.BaseField.value.Equals(currentVal.BaseField.value);
            }
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
                WaitToQuery(OnChangeBaseFieldValue);
            });

        }

        async void WaitToQuery(Action<T> OnChangeBaseFieldValue = null)
        {
            // Wait one frame till the panel is properly attached.
            await Task.Delay(1);

            BaseField = PropertyField.Q<BaseField<T>>();

            if (BaseField == null)
            {
                return;
            }

            OnChangeBaseFieldValue?.Invoke(BaseField.value);

            BaseField.RegisterValueChangedCallback(evt =>
            {
                OnChangeBaseFieldValue?.Invoke(evt.newValue);
            });
        }
    }
}
