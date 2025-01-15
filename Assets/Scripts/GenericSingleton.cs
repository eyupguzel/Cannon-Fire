using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T> 
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        }
    }
}

