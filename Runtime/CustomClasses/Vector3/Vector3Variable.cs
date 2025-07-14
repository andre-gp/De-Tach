using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_Red.png")]
    [CreateAssetMenu(fileName = "Vector3Variable", menuName = "DeTach/Vector3/Vector3Variable")]
    public class Vector3Variable : GenericVariable<Vector3, Vector3Event> { }
}