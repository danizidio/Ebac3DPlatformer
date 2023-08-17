using UnityEngine;

namespace CommonMethodsLibrary
{
    public class Singleton<T> : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

