using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Tools
{
    public static List<string> GetFileList(string path, string filter = "")
    {
        if (string.IsNullOrEmpty(filter)) filter = filter.ToLower();

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
}
