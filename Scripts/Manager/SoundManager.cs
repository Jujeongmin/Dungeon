using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 오디오 배열
    /// </summary>
    [SerializeField] AudioSource[] m_audioArr = null;

    /// <summary>
    /// 발자국 소리
    /// </summary>
    [SerializeField] AudioClip[] m_footClipArr = null;

    /// <summary>
    /// 배경음 사용 플래그
    /// </summary>
    bool m_useBGMFlag = true;

    /// <summary>
    /// SE 사용 플래그
    /// </summary>
    bool m_useSEFlag = true;

    /// <summary>
    /// 오디오 플레이
    /// </summary>
    /// <param name="argAudioType">오디오 타입</param>
    /// <param name="argAudioClip">오디오 클립</param>
    public void Play(AudioType.TYPE argAudioType, AudioClip argAudioClip)
    {
        int _index = (int)argAudioType;
        switch (argAudioType)
        {
            case AudioType.TYPE.BGM:
                if (!m_useBGMFlag) return;
                m_audioArr[_index].Stop();
                m_audioArr[_index].clip = argAudioClip;
                m_audioArr[_index].loop = true;
                m_audioArr[_index].Play();
                break;
            case AudioType.TYPE.SE_0:
                if (!m_useSEFlag) return;
                m_audioArr[_index].PlayOneShot(argAudioClip);
                break;
        }
    }

    /// <summary>
    /// 발자국 소리 SE 플레이
    /// </summary>
    /// <param name="argIndex">발자국 인덱스</param>
    public void FootPlay(int argIndex)
    {
        Play(AudioType.TYPE.SE_0, m_footClipArr[argIndex]);
    }

    /// <summary>
    /// 배경음 설정
    /// </summary>
    /// <param name="argUseFlag">사용 플래그</param>
    public void SetBGM(bool argUseFlag)
    {
        m_useBGMFlag = argUseFlag;
        int _index = (int)AudioType.TYPE.BGM;

        switch (m_useBGMFlag)
        {
            case false:
                m_audioArr[_index].Stop();
                break;
            case true:
                m_audioArr[_index].Stop();
                m_audioArr[_index].loop = true;
                m_audioArr[_index].Play();
                break;
        }
    }

    /// <summary>
    /// 효과음 설정
    /// </summary>
    /// <param name="argUseFlag">사용 플래그</param>
    public void SetSE(bool argUseFlag)
    {
        m_useSEFlag = argUseFlag;
    }
}
