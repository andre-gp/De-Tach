using UnityEditor;
using UnityEngine;

namespace DeTach.EditorDT
{
    public class HelpPopup : PopupWindowContent
    {
        public override Vector2 GetWindowSize()
        {
            return new Vector2(500, 80);
        }

        public override void OnGUI(Rect rect)
        {
            EditorGUILayout.LabelField("Help",
                new GUIStyle { fontStyle = FontStyle.Bold, fontSize = 13, normal = { textColor = Color.white } });

            EditorGUILayout.LabelField("Creating Multiple Types: 'int;float;Vector3;MyCustomType'");
            EditorGUILayout.LabelField("Adding Multiple Namespaces: 'UnityEngine;UnityEditor;MyProjectNamespace'");
        }
    }
}
