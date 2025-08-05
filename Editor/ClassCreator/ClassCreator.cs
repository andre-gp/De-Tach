using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
    public class ClassCreator
    {
        public static void CreateClass(ClassInfo classInfo, List<Tuple<string, string>> pathAndTemplateContent, string savePath)
        {
            var mainFolder = Path.Combine(savePath, classInfo.customName);

            var runtimeFolder = Path.Combine(mainFolder, "Runtime/");
            Directory.CreateDirectory(runtimeFolder);

            var editorFolder = Path.Combine(mainFolder, "Editor/");
            Directory.CreateDirectory(editorFolder);

            foreach (var template in pathAndTemplateContent)
            {
                string relativePath = template.Item1;
                string templateContent = template.Item2;

                var parsedRelativePath = relativePath.Replace(".txt", ".cs").Replace("#CUSTOMNAME#", classInfo.customName);
                parsedRelativePath = TemplateParser.Parse(parsedRelativePath, classInfo);

                var fileContents = TemplateParser.Parse(templateContent, classInfo);

                if (parsedRelativePath.EndsWith(".cs"))
                {
                    fileContents = AddNamespaces(fileContents, classInfo.namespaces);
                }

                File.WriteAllText(Path.Combine(mainFolder, parsedRelativePath), fileContents);
            }

            Debug.Log($"Succesfully created extensions for the type {classInfo.typeName}");

            AssetDatabase.Refresh();
        }

        public static string AddNamespaces(string content, string[] namespaces)
        {
            foreach (var ns in namespaces)
            {
                content = content.Insert(0, $"using {ns};{Environment.NewLine}");
            }

            return content;
        }
    }


    public class ClassInfo
    {
        public string typeName;
        public string customName;
        public string[] namespaces;
        public string iconColor;
        public string menuName;

        public ClassInfo(string typeName, string customName, string[] namespaces, string iconColor, string menuName)
        {
            this.typeName = typeName;
            this.customName = customName;
            this.namespaces = namespaces;
            this.iconColor = iconColor;
            this.menuName = menuName;
        }
    }
}
