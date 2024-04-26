using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    private static string assetRootPath;
    public static string currentModuleCode = "Module";

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
            return $"{AssetRootPath}/Data/{currentModuleCode}/EquipmentParts";
        }
    }
}
