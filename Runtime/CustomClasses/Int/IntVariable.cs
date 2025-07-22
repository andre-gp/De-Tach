using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "IntVariable", menuName = "DeTach/Built-in/Int/IntVariable", order = -1)]
    public class IntVariable : GenericVariable<int, IntEvent> { }
}