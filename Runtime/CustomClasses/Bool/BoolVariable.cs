using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "DeTach/Bool/BoolVariable", order = -1)]
    public class BoolVariable : GenericVariable<bool, BoolEvent> { }
}