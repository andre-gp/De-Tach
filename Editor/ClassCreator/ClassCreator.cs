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
            var runtimeFolder = Path.Combine(savePath, classInfo.customName);
            Directory.CreateDirectory(runtimeFolder);

            var editorFolder = Path.Combine(runtimeFolder, "Editor/");
            Directory.CreateDirectory(editorFolder);

            foreach (var template in pathAndTemplateContent)
            {
                string path = template.Item1;
                string templateContent = template.Item2;

                var newFileName = Path.GetFileName(path);
                newFileName = newFileName.Replace(".txt", ".cs").Replace("#CUSTOMNAME#", classInfo.customName);
                newFileName = TemplateParser.Parse(newFileName, classInfo);

                var fileContents = TemplateParser.Parse(templateContent, classInfo);
                fileContents = AddNamespaces(fileContents, classInfo.namespaces);

                bool isEditor = newFileName.Contains("Editor_");

                newFileName = newFileName.Replace("Editor_", "");

                string folderPath = isEditor ? editorFolder : runtimeFolder;

                File.WriteAllText(Path.Combine(folderPath, newFileName), fileContents);

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
