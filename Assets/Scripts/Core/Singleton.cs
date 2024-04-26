using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T m_Instance = null;

    public static T Instance 
    {
        get 
        {
            if (m_Instance == null)
            {
                GameObject go = new GameObject(typeof(T).ToString());
                m_Instance = go.AddComponent<T>();
                go.name= typeof(T).ToString();
            }
            return m_Instance;
        }
    }


    public virtual void Awake() 
    {
        m_Instance = this as T;
    }

    public static bool IsNull { get { return m_Instance == null; } }

}


 

//非MonoBehaviour单例基类, 约束（类，空构造函数）
public class SingletonN<T> where T : class,new()
{
    public static T m_Instance = null;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }
            return m_Instance;
        }
    }
}