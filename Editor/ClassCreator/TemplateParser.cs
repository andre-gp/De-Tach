using UnityEngine;

namespace DeTach.EditorDT
{
    public class TemplateParser
    {
        const string TYPE_KEY = "#TYPE#";
        const string CUSTOM_NAME_KEY = "#CUSTOMNAME#";

        public static string Parse(string template, string typeName, string customName)
        {
            return template.Replace(TYPE_KEY, typeName).Replace(CUSTOM_NAME_KEY, customName);
        }
    }

}
