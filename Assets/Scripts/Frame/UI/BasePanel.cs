using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour, IBasePanel
{
    // Panel Name
    [HideInInspector]
    public string m_PanelName;

    // Panel Instance
    public GameObject m_Content;

    // Panel it is Active?
    protected bool m_IsActive = false;

    // Panel it is Active?
    public bool IsActive 
    {
        get
        {
            return m_IsActive;
        }
    }

    public virtual void Awake()
    {
        m_PanelName = this.GetType().ToString();
        UIConsole.Instance.AddPanel(m_PanelName, this);
        m_IsActive = m_Content == null ? false : m_Content.activeSelf;
    }

    public virtual void Active(bool active)
    {
        m_IsActive = active;
        if (m_Content != null)
        {
            m_Content.SetActive(active);
        }
    }
}

public interface IGlobalPanel
{

}