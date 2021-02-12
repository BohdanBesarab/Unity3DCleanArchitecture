using UnityEngine;

namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public abstract class Singleton<T> : MonoBehaviour, IResult
        where T : Singleton<T>
    {
        private static bool ApplicationIsQuitting = false;

        private const string PARENT_NAME = "[Singleton]";
        private const string FORMATING_STRING = "{0} {1}";

        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null && !ApplicationIsQuitting)
                    {
                        var intermediate = new GameObject(string.Format(FORMATING_STRING, PARENT_NAME, typeof(T).Name)).AddComponent<T>();
                        if (intermediate.IsLoadFromPrefab)
                        {
                            var prefab = Resources.Load<GameObject>(intermediate.GetPrefabPath);
                            if (prefab != null)
                            {
                                DestroyImmediate(intermediate.gameObject);
                                var go = Instantiate(prefab, null) as GameObject;
                                go.name = prefab.name;
                                intermediate = go as T;
                                if (intermediate == null)
                                {
                                    intermediate = go.GetComponentInChildren<T>();
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Singleton self creator set to prefab mode, but no path is given! " + intermediate.GetPrefabPath);
                            }
                        }
                        _instance = intermediate;
                    }

                    if (_instance != null)
                    {
                        _instance.CheckInstance(_instance);
                    }
                }

                return _instance;
            }
        }

        protected virtual string GetPrefabPath { get; }
        protected virtual bool IsLoadFromPrefab { get { return false; } }
        protected virtual bool IsDontDestroy { get { return false; } }
        public static bool IsExist { get { return _instance != null; } }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(this.gameObject);
            }
        }

        protected virtual void Start()
        {
            if (Instance != this)
            {
                DestroyImmediate(this.gameObject);
            }
        }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
        private void CheckInstance(Singleton<T> instanceCheck)
        {
            if (instanceCheck != null && instanceCheck != this)
            {
                DestroyImmediate(this);
                return;
            }

            _instance = this as T;
            _instance.InitInstance();
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            if (_instance.IsDontDestroy)
            {
                DontDestroyOnLoad(_instance.transform.root.gameObject);
            }
        }

        protected virtual void InitInstance()
        {
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            ApplicationIsQuitting = true;
        }

    }

    public abstract class SingletonPersistent<T> : Singleton<T> where T : SingletonPersistent<T>
    {
        protected override bool IsDontDestroy { get { return true; } }
    }

    public abstract class SingletonSelfCreator<T> : Singleton<T> where T : SingletonSelfCreator<T>
    {
        protected abstract string PrefabPath { get; }
        protected override string GetPrefabPath { get { return PrefabPath; } }
        protected override bool IsLoadFromPrefab { get { return true; } }
        protected override bool IsDontDestroy { get { return true; } }
    }
}