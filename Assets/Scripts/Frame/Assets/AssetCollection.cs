using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetCollection : MonoBehaviour
{

    //列表
    private Dictionary<string, AssetItem> m_List = new Dictionary<string, AssetItem>();


    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    public void SetValue(string key, Object obj, AssetType type)
    {
        AssetItem item = new AssetItem();
        item.type = type;
        item.obj = obj;
        m_List.Add(key, item);
    }

    public Object GetValue(string key)
    {
        if (m_List.ContainsKey(key))
        {
            return m_List[key].obj;
        }
        else
        {
            return null;
        }
    }

    public void OnDestroy()
    {
    
    }

    public enum AssetType
    {
        Local, //本地 Resource下
        Remote //远程 Assetbundle WWW加载
    }

    public class AssetItem
    {
        //类型
        public AssetType type;

        //资源
        public Object obj;

        //资源列表
        public Object[] objs;
    }
}
