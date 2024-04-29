using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelMgr : MonoBehaviour
{
    private Dictionary<string, GameObject> m_ModelDic = new Dictionary<string, GameObject>();

    private List<ModelHighLightCol> m_ModelHightLightList = new List<ModelHighLightCol>();
    private List<ModelMatCol> m_ModelMatList = new List<ModelMatCol>();

    public List<string> GetStructureNameList()
    {
        foreach (Transform transf in transform.Find("Parts"))
        {
            m_ModelDic.Add(transf.name, transf.gameObject);
            ModelHighLightCol col = transf.gameObject.AddComponent<ModelHighLightCol>();
            m_ModelHightLightList.Add(col);
        }

        AddStructMatCol(this.transform);

        return m_ModelDic.Keys.ToList();
    }

    private void AddStructMatCol(Transform transf)
    {
        foreach (Transform item in transf)
        {
            if (item.GetComponent<MeshRenderer>() != null)
            {
                ModelMatCol col = item.gameObject.AddComponent<ModelMatCol>();
                m_ModelMatList.Add(col) ;
            }
            else
            {
                AddStructMatCol(item);
            }
        }
    }

    public void ClickedItem(string name)
    {

        // 所有材质设为透明
        foreach (var item in m_ModelMatList)
        {
            item.Transparent();
        }

        // 关闭所有高光
        foreach (var item in m_ModelHightLightList)
        {
            item.OffHighLight();
        }
    }
}
