using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingletonN<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        // ��֤MonoMgr�����Ψһ��
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    public void AddUpdateLister(UnityAction action)
    {
        controller.AddUpdateLister(action);
    }

    public void RemovedUpdateLister(UnityAction action)
    {
        controller.RemoveUpdateLister(action);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
}
