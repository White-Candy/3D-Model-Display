using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> namelist = new List<string>{"cube", "cube", "cube" };

        GlobalParams.NeedCnt = namelist.Count;
        foreach (var name in namelist)
        {
            GlobalParams.NeedTools.Enqueue(name);
        }
    }
}
