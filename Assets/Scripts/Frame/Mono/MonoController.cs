using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
            updateEvent();
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateLister(UnityAction action)
    {
        updateEvent += action;
    }

    public void RemoveUpdateLister(UnityAction action)
    {
        updateEvent -= action;
    }
}
