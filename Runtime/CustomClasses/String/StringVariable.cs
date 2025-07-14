using UnityEngine;

namespace DeTach
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "DeTach/String/StringVariable")]
    public class StringVariable : GenericVariable<string, StringEvent> { }
}