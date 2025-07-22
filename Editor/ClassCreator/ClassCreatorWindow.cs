using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;
using System.IO;
using DeTach.EditorDT.UIElements;
using System.Linq;

namespace DeTach.EditorDT
{
    public class ClassCreatorWindow : EditorWindow
    {
        const string TEMPLATES_SEARCH_FILTER = "glob:\"Packages/com.gaton.de-tach/Editor/Templates/*.txt\"";
        const string ICONS_SEARCH_FILTER = "glob:\"Packages/com.gaton.de-tach/Icons/Event_*.png\"";

        const string DEFAULT_PATH = "Assets/Plugins/DeTach/CustomClasses/";


        [MenuItem("DeTach/Class Creator")]
        public static void ShowExample()
        {
            ClassCreatorWindow wnd = GetWindow<ClassCreatorWindow>(true);
            wnd.titleContent = new GUIContent("Class Creator");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            /* --- HEADER --- */
            VisualElement header = CreateHeader();

            root.Add(header);

            root.Add(new Separator() { Height = 2 });

            /* --- TARGET PATH --- */
            var pathField = new TextField("Target Path");
            pathField.value = DEFAULT_PATH;
            root.Add(pathField);

            /* --- C# Type --- */
            var typeName = new TextField("C# Type");
            typeName.tooltip = "You can define multiple types by separating them with ';'";
            root.Add(typeName);

            /* --- CUSTOM NAME --- */
            var customName = new TextField("Custom Name");
            root.Add(customName);

            /* --- ICON COLOR --- */
            var colorDropdown = new DropdownField("Icon Color", LoadIconColorOptions(), 0);
            root.Add(colorDropdown);

            /* --- MENU NAME --- */
            var menuName = new TextField("Menu Name") { value = Application.productName };
            menuName.tooltip = "The Menu Name in which your classes will be acessible";
            root.Add(menuName);

            /* --- NAMESPACES --- */
            VisualElement namespacesRoot = new VisualElement()
            {
                style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                alignContent = Align.Center
            }
            };

            var addNamespaces = new Toggle();
            namespacesRoot.Add(addNamespaces);

            var namespacesLabel = new Label("Add Namespaces") { enabledSelf = false };
            namespacesRoot.Add(namespacesLabel);

            var namespacesField = new TextField() { enabledSelf = false, style = { flexGrow = 1, minHeight = 20 } };
            namespacesRoot.Add(namespacesField);

            root.Add(namespacesRoot);


            addNamespaces.RegisterValueChangedCallback(evt =>
            {
                namespacesField.enabledSelf = addNamespaces.value;
                namespacesLabel.enabledSelf = addNamespaces.value;
            });

            /* --- PREVIEW --- */
            var previewField = new Label();
            previewField.style.marginTop = 5;
            previewField.style.marginLeft = 4;
            UpdatePreview(previewField, "");
            root.Add(previewField);

            /* --- REGISTER CALLBACKS --- */
            typeName.RegisterValueChangedCallback(evt =>
            {
                string input = evt.newValue;

                if (input.Length >= 1)
                {
                    input = RemoveWhiteSpaces(input);

                    input = char.ToUpper(input[0]) + input.Substring(1);

                    // Convert every character followed by a ';' or ',' to be uppercase.
                    input = Regex.Replace(input, @"([;,])(.)", match => match.Groups[1].Value + match.Groups[2].Value.ToUpper());
                }

                customName.value = input;
            });


            customName.RegisterValueChangedCallback(evt =>
            {
                string input = evt.newValue;
                UpdatePreview(previewField, input);
            });


            /* --- CREATE BUTTON --- */
            Button createButton = new Button() { text = "Create Classes" };
            createButton.style.marginTop = 15;
            createButton.clickable.clicked += () =>
            {
                string namespaceInput = addNamespaces.value ? namespacesField.value : "";
                CreateAllClasses(typeName.text, customName.text, pathField.value, namespaceInput, colorDropdown.value, menuName.text);
            };

            root.Add(createButton);

            root.Add(new Separator() { Height = 2 });

        }

        private static VisualElement CreateHeader()
        {
            var header = new VisualElement()
            {
                style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                fontSize = 14,
                marginTop = 6,
                marginLeft = 10
            }
            };

            header.Add(new Label("<b>Fill the type informations</b>"));

            var helpButton = new Button() { text = "<b>?</b>", style = { alignSelf = Align.Center } };

            helpButton.clickable.clicked += () =>
            {
                Rect rect = helpButton.contentRect;
                rect.center += new Vector2(0, -80);

                UnityEditor.PopupWindow.Show(rect, new HelpPopup());
            };

            header.Add(helpButton);


            return header;
        }

        public void CreateAllClasses(string typeInput, string customNameInput, string savePath, string namespaceInput, string iconColor, string menuName)
        {
            string[] templateGuids = AssetDatabase.FindAssets(TEMPLATES_SEARCH_FILTER);

            typeInput = RemoveWhiteSpaces(typeInput);

            string[] types = ExtractInputs(typeInput);
            string[] customNames = ExtractInputs(customNameInput);
            string[] namespaces = ExtractInputs(namespaceInput);

            if (types.Length != customNames.Length)
            {
                Debug.LogError("The amount of types and custom names are different! \n" +
                               "If you are unsure on what's happening, type only on the C# Type field and let the program automatically parse the Custom Name for you.");

                return;
            }

            List<Tuple<string, string>> pathAndTemplateContent = new List<Tuple<string, string>>();

            foreach (var guid in templateGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string template = File.ReadAllText(path);

                pathAndTemplateContent.Add(new Tuple<string, string>(path, template));
            }

            for (int i = 0; i < types.Length; i++)
            {
                ClassInfo infos = new ClassInfo(types[i], customNames[i], namespaces, iconColor, menuName);
                ClassCreator.CreateClass(infos, pathAndTemplateContent, savePath);
            }
        }

        private static string RemoveWhiteSpaces(string typeInput)
        {
            return typeInput.Replace(" ", string.Empty);
        }

        void UpdatePreview(Label label, string input)
        {
            input = RemoveWhiteSpaces(input);

            string[] types = ExtractInputs(input);

            StringBuilder previewBuilder = new StringBuilder();

            if (types.Length > 1)
            {
                foreach (var type in types)
                {
                    int padAmount = 60;
                    string variablePreview = $"{type}Variable";
                    variablePreview = PadString(variablePreview, padAmount);

                    string eventPreview = $"{type}Event";
                    eventPreview = PadString(eventPreview, padAmount);

                    string inspectorPreview = $"{type}Inspector";
                    inspectorPreview = PadString(inspectorPreview, padAmount);

                    previewBuilder.AppendLine($"{variablePreview}\t{eventPreview}\t{inspectorPreview}");
                }
            }
            else
            {
                previewBuilder.AppendLine($"{input}Variable\n{input}Event\n{input}Inspector");
            }

            label.text = previewBuilder.ToString();
        }

        public string[] ExtractInputs(string input)
        {
            string[] types = input.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            return types;
        }

        string PadString(string text, int padAmount)
        {
            return text.PadRight(Mathf.Max(0, padAmount - text.Length));
        }

        public List<string> LoadIconColorOptions()
        {
            var iconsGuids = AssetDatabase.FindAssets(ICONS_SEARCH_FILTER);

            var iconsNames = new List<string>();

            return iconsGuids.Select(guid => {
                string filePath = AssetDatabase.GUIDToAssetPath(guid);
                string colorName = Path.GetFileNameWithoutExtension(filePath).Split('_')[1];
                return colorName;
            }).ToList();
        }
    }
}
