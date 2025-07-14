using DeTach.EditorDT.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace DeTach.EditorDT
{
    [CustomEditor(typeof(BaseEvent))]
    public class EventInspector<T, TEvent> : Editor where TEvent : GenericEvent<T>
    {
        Foldout foldout;

        PlayModeInfos playModeInfos;

        public EventInspector()
        {
            playModeInfos = new PlayModeInfos();
            playModeInfos.OnChangeIsPlaying += OnChangeIsPlaying;
        }

        private void OnChangeIsPlaying(bool isPlaying)
        {
            if (foldout != null)
            {
                foldout.value = isPlaying;
            }
        }

        private void OnDestroy()
        {
            playModeInfos.Dispose();
        }

        public override VisualElement CreateInspectorGUI()
        {
            TEvent baseEvent = target as TEvent;
            var root = new VisualElement();

            /* --- Scriptable Obj Field --- */
            ObjectField scriptableField = new ObjectField();
            scriptableField.value = baseEvent;
            scriptableField.enabledSelf = false;
            root.Add(scriptableField);


            /* --- Editor Invoke Value --- */
            VisualElement editorInvokeRoot = new VisualElement();
            editorInvokeRoot.style.flexDirection = FlexDirection.Row;
            editorInvokeRoot.style.alignItems = Align.Center;
            editorInvokeRoot.style.marginTop = 10;

            Label label = new Label("Debug:");
            editorInvokeRoot.Add(label);

            var temporaryValue = new PropertyField(serializedObject.FindProperty("invokeVal"));
            temporaryValue.label = "";
            temporaryValue.style.minWidth = 200;
            temporaryValue.style.paddingLeft = 15;

            Button raiseBtn = new Button(() =>
            {
                // Unity doesn't expose access to a .value in the 'PropertyField'
                // It is a wrapper around a BaseField of value T
                var editorInvokeValue = temporaryValue.Q<BaseField<T>>().value;

                baseEvent.Invoke(editorInvokeValue);

            });

            raiseBtn.text = "Invoke";
            raiseBtn.style.marginLeft = 10;

            editorInvokeRoot.Add(temporaryValue);
            editorInvokeRoot.Add(raiseBtn);
            root.Add(editorInvokeRoot);

            /* --- Listeners --- */
            root.Add(new Separator());

            foldout = new Foldout()
            {
                text = "Listeners"
            };
            foldout.style.marginLeft = 10;

            ListView list = new ListView(baseEvent.Listeners);
            list.selectionType = SelectionType.Single;
            list.selectionChanged += evt => { EditorGUIUtility.PingObject(list.selectedItem as Object); };
            list.makeNoneElement += () => { return null; };
            list.showBorder = true;
            list.style.unityTextAlign = TextAnchor.MiddleLeft;
            list.showAlternatingRowBackgrounds = AlternatingRowBackground.All;

            root.Add(list);

            foldout.Add(list);

            root.Add(foldout);

            playModeInfos.RefreshPlayModeState(true);

            return root;
        }

        void UpdateListLabel(Foldout element, BaseEvent baseEvent)
        {
            Debug.Log("Changing");
            element.text = $"Listeners ({baseEvent.Listeners.Count})";
        }
    }
}
