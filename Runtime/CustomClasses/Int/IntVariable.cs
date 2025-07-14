using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "IntVariable", menuName = "DeTach/Int/IntVariable")]
    public class IntVariable : GenericVariable<int, IntEvent> { }
}