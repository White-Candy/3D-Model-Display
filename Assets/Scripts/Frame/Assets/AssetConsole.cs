using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

public class AssetConsole : Singleton<AssetConsole>
{
    //资源列表
    public AssetList m_List = new AssetList();

    public AsyncResult LoadBundle(string path, bool isTemp = true, params AssetInfo[] info)
    {
        AsyncResult result = new AsyncResult();

        //获取缓存资源
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
    /// 加载bundle
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private IEnumerator StartLoadBundle(string path, AssetInfo[] info, AsyncResult result, bool isTemp)
    {
        //www 与 load进度所占比例
        float wwwPart = 0.8f;
        float loadPart = 0.2f;

        //加载WWWTools.WWWPath(path)
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
                // 存结果列表
                result.result[key] = request.asset;

                //缓存
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
    /// 加载bundle
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">bundle路径</param>
    /// <param name="name"><资源名称/param>
    /// <returns></returns>
    public AsyncResult LoadBundle<T>(string path, string name, bool isTemp = true)
    {
       return LoadBundle(path, isTemp, new AssetInfo(typeof(T), name));
    }
}

public class AssetList
{
    // 全局列表
    private AssetCollection m_Collection;

    // 临时列表
    private AssetCollection m_TempCollection;

    // 全局列表
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

    // 临时列表
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
    //类型
    public System.Type type;

    //名称
    public string name;


    /// <summary>
    /// 构造函数
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
    //是否完成
    public bool isDone = false;

    //加载进度
    public float progress = 0;

    //结果集合(类型1) 
    public Dictionary<string, Object> result = new Dictionary<string, Object>();

    //结果集合(类型2) 
    public Object[] resultAll = new Object[] { };


    /// <summary>
    /// 获取资源(类型1) 
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
