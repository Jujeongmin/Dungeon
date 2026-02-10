using UnityEngine;

public class GameMenu : MonoBehaviour
{
    /// <summary>
    /// 애니메이터
    /// </summary>
    [SerializeField] Animator m_animator = null;

    /// <summary>
    /// 보여짐 플래그
    /// </summary>
    bool m_viewFlag = false;

    // Update is called once per frame
    void Update()
    {
        if (!GManager.Instance.IsDieFlag || m_viewFlag) return;

        m_viewFlag = true;
        m_animator.Play("Open");
    }
}
