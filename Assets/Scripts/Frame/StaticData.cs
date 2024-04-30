using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    private static string assetRootPath;

    public static string AssetRootPath
    {
        get
        {
            assetRootPath = Application.streamingAssetsPath;
            return assetRootPath;
        }

        set
        {
            assetRootPath = value;
        }
    }

    public static string EquipmentPartsPath
    {
        get
        {
            return $"{AssetRootPath}/Data/Modules";
        }
    }

    public static string StructurePath
    {
        get
        {
            return $"{AssetRootPath}/Data/Structures";
        }
    }

    public static string StructureName
    {
        get
        {
            return "10001";
        }
    }
}
