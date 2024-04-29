using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureAction : BaseAction
{
    private static ModelAction instance;

    private ModelPanel m_Panel;
    private ModelMgr m_Mgr;
    public List<string> m_StructureNameList = new List<string>();

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
        MonoMgr.Instance.StartCoroutine(LoadAsset());
        m_Panel = Tools.FindAssetPanel<ModelPanel>();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateData()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator LoadAsset()
    {
        AsyncResult result = AssetConsole.Instance.LoadBundleGameObject(StaticData.StructurePath + "/" + StaticData.StructureName, StaticData.StructureName, true);

        while (!result.isDone)
        {
            yield return null;
        }

        GameObject go = result.LoadAsset<GameObject>(StaticData.StructureName);
        if (go != null)
        {
            ModelMgr Manager = go.GetComponent<ModelMgr>();
            m_Mgr = Manager;
            m_StructureNameList = Manager.GetStructureNameList();
            m_Panel.InitPanel(this);
            m_Panel.OnBtnCilcked = PanelItemCilcked;
        }
    }

    private void PanelItemCilcked(string name)
    {
        m_Mgr.ClickedItem(name);
    }
}