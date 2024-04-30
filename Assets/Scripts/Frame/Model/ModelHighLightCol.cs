using System;
using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Outlinable))]
public class ModelHighLightCol : MonoBehaviour
{
    private Outlinable m_Outlinable;
    Color m_OutlineableInitColor;

    private void Awake()
    {
        m_Outlinable = GetComponent<Outlinable>();
        m_Outlinable.enabled = false;

        m_OutlineableInitColor = m_Outlinable.OutlineParameters.Color;
    }

    /// <summary>
    /// 打开高亮
    /// </summary>
    public void OnHighLight()
    {
        m_Outlinable.enabled = false;
        m_Outlinable.OutlineParameters.DOColor(Color.red, .5f).SetLoops(3, LoopType.Yoyo).onComplete += () =>
        {
            m_Outlinable.enabled = false;
            m_Outlinable.OutlineParameters.Color = m_OutlineableInitColor;
        };
    }

    /// <summary>
    /// 停止高亮
    /// </summary>
    public void OffHighLight()
    {
        m_Outlinable.enabled = false;
        m_Outlinable.OutlineParameters.DOKill(true);
        m_Outlinable.OutlineParameters.Color = m_OutlineableInitColor;
    }
}
