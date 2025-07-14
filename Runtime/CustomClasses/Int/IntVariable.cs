using UnityEngine;

namespace DeTach
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "DeTach/Int/IntVariable")]
    public class IntVariable : GenericVariable<int, IntEvent> { }
}