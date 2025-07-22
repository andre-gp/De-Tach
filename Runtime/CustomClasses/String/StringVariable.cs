using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "StringVariable", menuName = "DeTach/Built-in/String/StringVariable", order = -1)]
    public class StringVariable : GenericVariable<string, StringEvent> { }
}