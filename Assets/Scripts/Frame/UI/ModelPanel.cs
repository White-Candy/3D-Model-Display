using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelPanel : BasePanel
{
    public Action<string> OnBtnCilcked = (str) => { };

    private ModelAction m_ModelAction;

    public GameObject m_ModelItem;
    public Transform m_ModelParent;
    //public TextMeshProUGUI m_DescriptionText;

    public void InitPanel(ModelAction modelAction)
    {
        m_ModelAction = modelAction;
        SpawnItem(m_ModelAction.GetModelsName());
    }

    public void SpawnItem(List<string> items)
    {
        foreach (string item in items)
        {
            GameObject obj = GameObject.Instantiate(m_ModelItem, m_ModelParent);
            obj.SetActive(true);

            Button btn = obj.GetComponent<Button>();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = item;
            Sprite spr = Resources.Load<Sprite>($"Model/Sprites/{item}");
            if (spr == null )
            {
                spr = Resources.Load<Sprite>("NotFoundTips/NotFoundImage");
            }
            btn.GetComponent<Image>().sprite = spr;
            btn.onClick.AddListener(() => { OnBtnCilcked?.Invoke(item); });
        }
    }

    public override void Active(bool active)
    {
        base.Active(active);
        m_ModelAction.OnMoudelItemCilcked(m_ModelAction.m_ModelPartDic.First().Key);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
