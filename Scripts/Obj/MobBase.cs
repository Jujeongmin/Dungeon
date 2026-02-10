using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    /// <summary>
    /// 스프라이트 랜더러 리스트
    /// </summary>
    [SerializeField] List<SpriteRenderer> m_spRendererList = null;

    private void Start()
    {
        IsView = false;
    }

    /// <summary>
    /// 보여짐 설정
    /// </summary>
    public bool IsView
    {
        set
        {
            for (int i = 0; i < m_spRendererList.Count; i++)
            {
                if (m_spRendererList[i] != null) m_spRendererList[i].enabled = value;
                else m_spRendererList.RemoveAt(i);
            }
        }
    }
}
