using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.PackageManager;
using UnityEngine;

public class ModelAction : BaseAction
{
    private static ModelAction instance;

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

    private IEnumerator InitAsset()
    {
        yield return MonoMgr.Instance.StartCoroutine(LoadAsset());
    }

    private IEnumerator LoadAsset()
    {
        List<string> AssetsPath = Tools.GetFileList(StaticData.EquipmentPartsPath);
        if (AssetsPath == null ||  AssetsPath.Count == 0)
        {
            Debug.Log("Assets not load!");
            yield break;
        }

        // 所有模型读取到内存中
        foreach (var path in AssetsPath) 
        {
            string name = Path.GetFileNameWithoutExtension(path);

            AsyncResult result = AssetConsole.Instance.LoadBundle<GameObject>(path, name, true);
            while (result != null)
            {
                yield return null;
            }

            m_ModelPartDic.Add(name, result.LoadAsset<GameObject>(name));
        }
    }
}
