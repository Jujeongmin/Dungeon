using UnityEngine;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// 애니메이터
    /// </summary>
    [SerializeField] Animator m_animator = null;

    /// <summary>
    /// 윈도우 뷰 플래그
    /// </summary>
    bool m_viewFlag = false;

    /// <summary>
    /// 버튼 클릭시 처리
    /// </summary>
    public void ButtonClick(bool argFlag)
    {
        m_viewFlag = argFlag;
        m_animator.Play(m_viewFlag == true ? "Open" : "Close");
    }
}
