using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Red.png")]
    [CreateAssetMenu(fileName = "SpriteVariable", menuName = "DeTach/Unity/Sprite/SpriteVariable", order = -1)]
    public class SpriteVariable : GenericVariable<Sprite, SpriteEvent> { }
}