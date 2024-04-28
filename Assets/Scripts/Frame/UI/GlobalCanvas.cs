using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCanvas : MonoBehaviour
{
    private static GlobalCanvas instance;

    public static GlobalCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject canvas = Instantiate<GameObject>(Resources.Load("Resources/Prefabs/UI/GlobalCanvas") as GameObject);
                instance = canvas.GetComponent<GlobalCanvas>();
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
}
