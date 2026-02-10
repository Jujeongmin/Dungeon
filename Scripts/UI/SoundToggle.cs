using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    /// <summary>
    /// 오디오 타입
    /// </summary>
    [SerializeField] AudioType.TYPE m_audioType = AudioType.TYPE.BGM;

    /// <summary>
    /// 토글
    /// </summary>
    [SerializeField] Toggle m_toggle = null;

    /// <summary>
    /// 버튼 클릭시 처리
    /// </summary>
    public void ButtonClick()
    {
        switch(m_audioType)
        {
            case AudioType.TYPE.BGM:
                GManager.Instance.IsSound.SetBGM(m_toggle.isOn);
                break;
            case AudioType.TYPE.SE_0:
                GManager.Instance.IsSound.SetSE(m_toggle.isOn);
                break;
        }
    }
}
