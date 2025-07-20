using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "DeTach/Float/FloatVariable", order = -1)]
    public class FloatVariable : GenericVariable<float, FloatEvent> { }
}