using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Green.png")]
    [CreateAssetMenu(fileName = "ColorVariable", menuName = "DeTach/Unity/Color/ColorVariable", order = -1)]
    public class ColorVariable : GenericVariable<Color, ColorEvent> { }
}