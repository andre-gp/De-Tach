using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Green.png")]
    [CreateAssetMenu(fileName = "GameObjectVariable", menuName = "DeTach/Unity/GameObject/GameObjectVariable", order = -1)]
    public class GameObjectVariable : GenericVariable<GameObject, GameObjectEvent> { }
}