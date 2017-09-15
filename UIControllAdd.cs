using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIControllAdd : MonoBehaviour
{
    /// <summary>
    /// 加算する割合
    /// </summary>
    /// <value>The add parcentage.</value>
    public float addParcentage
    {
        get
        {
            return m_addParcentage;
        }

        set
        {
            m_addParcentage = value;

        }
    }

    /// <summary>
    /// 加算の割合
    /// </summary>
    [SerializeField, Range(0f, 1f)] float m_addParcentage;

    void Awake()
    {
    }

    int count = 0;

    private void Update()
    {
        //count++;
        //float rate01 = (count % 1001) / 1000.0f;

        var widget = GetComponent<UIWidget>();
        widget.material.SetFloat("_AddPercentage", m_addParcentage);
        Debug.Log("" + m_addParcentage);
        widget.
    }

    void SetControllAddCB(Material mat)
    {
        mat.SetFloat("_AddPercentage", m_addParcentage);
    }
}