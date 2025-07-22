using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Green.png")]
    [CreateAssetMenu(fileName = "Vector3Variable", menuName = "DeTach/Unity/Vector3/Vector3Variable", order = -1)]
    public class Vector3Variable : GenericVariable<Vector3, Vector3Event> { }
}