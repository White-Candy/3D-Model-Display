using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetCollection : MonoBehaviour
{

    //�б�
    private Dictionary<string, AssetItem> m_List = new Dictionary<string, AssetItem>();


    /// <summary>
    /// ����ֵ
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
        Local, //���� Resource��
        Remote //Զ�� Assetbundle WWW����
    }

    public class AssetItem
    {
        //����
        public AssetType type;

        //��Դ
        public Object obj;

        //��Դ�б�
        public Object[] objs;
    }
}
