using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ModelMatCol : MonoBehaviour
{
    private Material[] m_DefultMat;

    private Material m_TransparentMat;

    private MeshRenderer m_MeshRender;

    private void Awake()
    {
        m_MeshRender = GetComponent<MeshRenderer>();
        m_DefultMat = m_MeshRender.materials;
        m_TransparentMat = Resources.Load<Material>("Structure/Material/TransparentMat");
    }

    public void Transparent()
    {
        Material[] mats = new Material[m_MeshRender.materials.Length];
        for (int i = 0; i < m_MeshRender.materials.Length; ++i)
        {
            mats[i] = m_TransparentMat;
        }
        m_MeshRender.materials = mats;
    }

    /// <summary>
    /// 还原模型材质
    /// </summary>
    public void Revert()
    {
        m_MeshRender.materials = m_DefultMat;
    }
}
