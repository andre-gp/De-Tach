using System;
using System.Collections.Generic;
using UnityEditor;

namespace DeTach.EditorDT
{
    public class PlayModeInfos : IDisposable
    {
        public bool IsInPlayMode { get => EditorApplication.isPlaying; }

        /// <summary>
        /// Simplified event when you don't care about the current specific step of play mode, only if its playing or not.
        /// </summary>
        public Action<bool> OnChangeIsPlaying;

        Dictionary<PlayModeStateChange, Action> callbacks;

        public PlayModeInfos()
        {
            callbacks = new Dictionary<PlayModeStateChange, Action>();

            EditorApplication.playModeStateChanged += OnChangePlaymode;
        }

        public void Dispose()
        {
            EditorApplication.playModeStateChanged -= OnChangePlaymode;
        }

        public void Subscribe(PlayModeStateChange change, Action callback)
        {
            if (!callbacks.ContainsKey(change))
            {
                callbacks.Add(change, null);
            }

            callbacks[change] += callback;
        }

        public void Unsubscribe(PlayModeStateChange change, Action callback)
        {
            if (callbacks.ContainsKey(change))
            {
                callbacks[change] -= callback;
            }
        }

        private void OnChangePlaymode(PlayModeStateChange change)
        {
            RefreshPlayModeState();

            if (callbacks.TryGetValue(change, out Action callback))
            {
                callback.Invoke();
            }

        }

        public void RefreshPlayModeState(bool forceCallback = false)
        {
            if (IsInPlayMode != EditorApplication.isPlaying || forceCallback)
            {
                forceCallback = false;

                OnChangeIsPlaying?.Invoke(IsInPlayMode);
            }
        }
    }

}
