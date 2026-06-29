using UnityEngine;

namespace DeTach
{
    public abstract class DeTachObject : ScriptableObject
    {       
        [Tooltip("When on, Resources.UnloadUnusedAssets() may unload this asset on " +
                 "scene loads, discarding its runtime value if no scene references it. " +
                 "Leave off to keep the asset in memory across scenes.")]
        [SerializeField] bool unloadUnusedAsset = false;

        public bool UnloadUnusedAsset => unloadUnusedAsset;

        protected virtual void OnEnable()
        {
            if (unloadUnusedAsset)
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
        }
    }

    public enum DeTachType
    {
        Variable = 0,
        Event = 1
    }
}
