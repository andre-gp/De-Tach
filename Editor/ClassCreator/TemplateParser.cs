using UnityEngine;

namespace DeTach.EditorDT
{
    public class TemplateParser
    {
        const string TYPE_KEY = "#TYPE#";
        const string CUSTOM_NAME_KEY = "#CUSTOMNAME#";
        const string COLOR_KEY = "#COLOR#";

        public static string Parse(string template, ClassInfo classInfo)
        {
            return template.Replace(TYPE_KEY, classInfo.typeName)
                           .Replace(CUSTOM_NAME_KEY, classInfo.customName)
                           .Replace(COLOR_KEY, classInfo.iconColor);
        }
    }

}
