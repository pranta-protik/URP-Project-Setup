using UnityEngine;

namespace MyTools
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected bool _dontDestroyOnLoad;

        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                DebugUtils.LogWarning($"There's more than one instance of {typeof(T).Name} in the scene!");
                Destroy(gameObject);
                return;
            }

            Instance = this as T;

            if (_dontDestroyOnLoad)
            {
                transform.parent = null;
                DontDestroyOnLoad(this);
            }

            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}