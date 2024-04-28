using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConsole : Singleton<UIConsole>
{
    // Panel List
    public Dictionary<string, BasePanel> m_List = new Dictionary<string, BasePanel>();
    
    public void AddPanel(string name, BasePanel panel)
    {
        if (!m_List.ContainsKey(name))
        {
            m_List.Add(name, panel);
        }
    }

    public T FindPanel<T>() where T : class,IBasePanel
    {
        foreach (var item in m_List.Values)
        {
            if (item is T)
            {
                return item as T;
            }
        }
        return null;
    }
}

public interface IBasePanel
{

}
