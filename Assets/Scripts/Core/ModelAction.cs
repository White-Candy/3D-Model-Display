using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ModelAction : BaseAction
{
    private static ModelAction instance;

    private GameObject currentModel;

    public Dictionary<string, GameObject> m_ModelPartDic = new Dictionary<string, GameObject>();

    public static ModelAction Get()
    {
        if (instance == null)
        {
            instance = new ModelAction();
        }
        return instance;
    }

    public override void Show()
    {
        MonoMgr.Instance.StartCoroutine(InitAsset());
    }

    public override void UpdateData()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    private void ModelDisplay(string name)
    {
        Debug.Log("ModelDisplay: " + name);
        if (currentModel != null)
        {
            GameObject.Destroy(currentModel);
        }
        GameObject go;
        m_ModelPartDic.TryGetValue(name, out go);
        GameObject obj = GameObject.Instantiate(go);
        CameraMovementController.Instance.UpdateData(obj.transform);
        currentModel = obj;
    }

    private IEnumerator InitAsset()
    {
        yield return MonoMgr.Instance.StartCoroutine(LoadAsset());
        ModelDisplay(m_ModelPartDic.First().Key);
    }

    private IEnumerator LoadAsset()
    {
        List<string> AssetsPath = Tools.GetFileList(StaticData.EquipmentPartsPath);
        //Debug.Log(StaticData.EquipmentPartsPath);
        if (AssetsPath == null || AssetsPath.Count == 0)
        {
            Debug.Log("Assets not load!");
            yield break;
        }

        // 所有模型读取到内存中
        foreach (var path in AssetsPath) 
        {
            string name = Path.GetFileNameWithoutExtension(path);
            //Debug.Log("path: " + name);
            AsyncResult result = AssetConsole.Instance.LoadBundle<GameObject>(path, name, true);
            while (!result.isDone)
            {
                yield return null;
            }

            m_ModelPartDic.Add(name, result.LoadAsset<GameObject>(name));
        }
    }
}
