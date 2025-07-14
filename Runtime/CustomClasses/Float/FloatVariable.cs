using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Blue.png")]
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "DeTach/Float/FloatVariable")]
    public class FloatVariable : GenericVariable<float, FloatEvent> { }
}