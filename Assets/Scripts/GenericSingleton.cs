using UnityEngine;

/// <summary>
/// A generic singleton mother class that can be inherited
/// by other singleton scripts to avoid boilerplate code.
/// </summary>
/// <typeparam name="T">T is a parameter of type Component</typeparam>

public class GenericSingleton<T> : MonoBehaviour where T : Component {
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null) {
                    GameObject go = new GameObject {
                        name = typeof(T).Name
                    };

                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake() {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
