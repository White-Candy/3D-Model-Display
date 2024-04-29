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
    /// Õ£÷π∏ﬂ¡¡
    /// </summary>
    public void OffHighLight()
    {
        m_Outlinable.enabled = false;
        //m_Outlinable.OutlineParameters.DOKill(true);
    }
}
