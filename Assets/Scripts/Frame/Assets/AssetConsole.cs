using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

public class AssetConsole : Singleton<AssetConsole>
{
    //��Դ�б�
    public AssetList m_List = new AssetList();

    public AsyncResult LoadBundle(string path, bool isTemp = true, params AssetInfo[] info)
    {
        AsyncResult result = new AsyncResult();

        //��ȡ������Դ
        GetBundleCacheAsset(path, info, result);

        if (!result.isDone)
            StartCoroutine(StartLoadBundle(path, info, result, isTemp));
        return result;
    }

    private void GetBundleCacheAsset(string path, AssetInfo[] info, AsyncResult result)
    {
        bool isDone = true;

        foreach (var item in info)
        {
            string resultKey = item.type.ToString() + "_" + item.name;
            string key = path + "_" + resultKey;
            Object obj = m_List.GetValue(key);
            if (obj != null)
            {
                result.result[resultKey] = obj;
            }
            else
            {
                isDone = false;
            }
        }

        result.isDone = isDone;
        result.progress =  isDone ? 1 : 0;
    }

    /// <summary>
    /// ����bundle
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private IEnumerator StartLoadBundle(string path, AssetInfo[] info, AsyncResult result, bool isTemp)
    {
        //www �� load������ռ����
        float wwwPart = 0.8f;
        float loadPart = 0.2f;

        //����WWWTools.WWWPath(path)
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(Tools.WWWPath(path), 0))
        {
            yield return www.SendWebRequest();

            while (!www.isDone)
            {
                result.progress = wwwPart * www.downloadProgress;
                yield return 0;
            }
            result.progress = wwwPart;

            float assetProgress = 0;
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
            float step = loadPart / info.Length;
            foreach (var ai in info)
            {
                string key = ai.type.ToString() + "_" + ai.name;
                
                if (result.result.ContainsKey(key))
                {
                    assetProgress += step;
                    result.progress = wwwPart + assetProgress;
                    continue;
                }

                AssetBundleRequest request = ab.LoadAssetAsync(ai.name, ai.type);
                while (!request.isDone)
                {
                    result.progress = wwwPart + assetProgress + step * request.progress;
                    yield return 0;
                }
                // �����б�
                result.result[key] = request.asset;

                //����
                m_List.SetRemoteValue(path + "_" + key, request.asset, isTemp);

                assetProgress += step;
                result.progress = wwwPart + assetProgress;
            }
            ab.Unload(false);

            result.progress = 1;
            result.isDone = true;
        }
    }

    /// <summary>
    /// ����bundle
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="path">bundle·��</param>
    /// <param name="name"><��Դ����/param>
    /// <returns></returns>
    public AsyncResult LoadBundle<T>(string path, string name, bool isTemp = true)
    {
       return LoadBundle(path, isTemp, new AssetInfo(typeof(T), name));
    }
}

public class AssetList
{
    // ȫ���б�
    private AssetCollection m_Collection;

    // ��ʱ�б�
    private AssetCollection m_TempCollection;

    // ȫ���б�
    public AssetCollection CollectionList
    {
        get
        {
            if (m_Collection == null)
            {
                GameObject go = new GameObject("GlobalAssetList");
                m_Collection = go.AddComponent<AssetCollection>();
                GameObject.DontDestroyOnLoad(go);
            }
            return m_Collection;
        }
    }

    // ��ʱ�б�
    public AssetCollection TempCollectionList
    {
        get
        {
            if (m_TempCollection == null)
            {
                GameObject go = new GameObject("TempAssetList");
                m_TempCollection = go.AddComponent<AssetCollection>();
            }
            return m_TempCollection;
        }
    }

    public Object GetValue(string val)
    {
        Object obj = null;
        if (obj == null) obj = CollectionList.GetValue(val);
        if (obj == null) obj = TempCollectionList.GetValue(val);
        return obj;
    }

    public void SetRemoteValue(string key, Object value, bool isTemp = true)
    {
        if (isTemp)
        {
            TempCollectionList.SetValue(key, value, AssetCollection.AssetType.Remote);
        }
        else
        {
            CollectionList.SetValue(key, value, AssetCollection.AssetType.Remote);
        }
    }

}

public class AssetInfo
{
    //����
    public System.Type type;

    //����
    public string name;


    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="t"></param>
    /// <param name="n"></param>
    public AssetInfo(System.Type t, string n)
    {
        type = t;
        name = n;
    }
}

public class AsyncResult
{
    //�Ƿ����
    public bool isDone = false;

    //���ؽ���
    public float progress = 0;

    //�������(����1) 
    public Dictionary<string, Object> result = new Dictionary<string, Object>();

    //�������(����2) 
    public Object[] resultAll = new Object[] { };


    /// <summary>
    /// ��ȡ��Դ(����1) 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    public T LoadAsset<T>(string name) where T : Object
    {
        string key = typeof(T).ToString() + "_" + name;
        if (result.ContainsKey(key))
            return result[key] as T;
        else
            return null;
    }
}
