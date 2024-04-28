using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Tools
{
    public static List<string> GetFileList(string path, string filter = "")
    {
        if (!string.IsNullOrEmpty(filter)) filter = filter.ToLower();

        List<string> result = new List<string>();
        if (!Directory.Exists(path)) return result;

        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] fileinfo = di.GetFiles();
        foreach (FileInfo file in fileinfo)
        {
            if (file.Name.EndsWith(".meta"))
            {
                continue;
            }

            if (string.IsNullOrEmpty(filter))
            {
                result.Add(file.FullName);
                //Debug.Log(file.FullName);
            }
            else
            {
                if (file.Extension == filter)
                {
                    result.Add(file.FullName);
                }
            }
        }

        return result;
    }

    public static string WWWPath(string path)
    {
        return "file://" + path;
    }

    // �������
    public static T FindAssetPanel<T>() where T : BasePanel
    {
        T t = UIConsole.Instance.FindPanel<T>();
        if (t == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/" + typeof(T).ToString()));
            t = go.GetComponent<T>();
            go.name = typeof(T).ToString();
            if (t is IGlobalPanel)
            {
                go.transform.SetParent(GlobalCanvas.Instance.transform);
            }
            else
            {
                go.transform.SetParent(GameObject.Find("Canvas/").transform);
            }

            go.transform.localScale = Vector3.one;
            RectTransform rect = go.transform as RectTransform;
            rect.offsetMax = Vector3.zero;
            rect.offsetMin = Vector3.zero;
        }
        return t;
    }
}
