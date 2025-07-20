using UnityEngine;

namespace DeTach
{
    [Icon("Packages/com.gaton.de-tach/Icons/Variable_White.png")]
    public abstract class BaseVariable : DeTachObject
    {
        public abstract string ValueToString();

        public abstract BaseEvent GetBaseEvent();
    }
}
