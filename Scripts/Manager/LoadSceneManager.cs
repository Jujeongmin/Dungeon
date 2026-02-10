using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    /// <summary>
    /// 대기 플래그
    /// </summary>
    public bool IsWaitFlag { get; set; } = false;

    /// <summary>
    /// 칼라 배열
    /// 0: in, 1: out
    /// </summary>
    [SerializeField] Color[] m_colorArr = null;

    /// <summary>
    /// fade 이미지
    /// </summary>
    [SerializeField] Image m_fadeImg = null;

    /// <summary>
    /// fade 시간
    /// (1.0 이라면 1초만에 Fade In/Out 실행됨)
    /// </summary>
    [SerializeField] float m_fadeTimeScale = 1.0f;

    /// <summary>
    /// 현재의 칼라
    /// </summary>
    Color m_nowColor = Color.black;

    /// <summary>
    /// 씬 이동 플래그
    /// </summary>
    bool m_changeSceneFlag = false;

    /// <summary>
    /// 씬 이동
    /// </summary>
    /// <param name="argSceneName">이동할 씬 이름</param>
    public void Load(string argSceneName)
    {
        if (m_changeSceneFlag) return;

        StartCoroutine(ChangeScene(argSceneName));
    }

    /// <summary>
    /// 씬 바꾸기
    /// </summary>
    /// <param name="argSceneName">씬 이름</param>
    /// <returns></returns>
    IEnumerator ChangeScene(string argSceneName)
    {
        m_changeSceneFlag = true;

        m_fadeImg.raycastTarget = true;
        m_nowColor = m_colorArr[0];

        while (m_nowColor.a != m_colorArr[1].a)
        {
            m_nowColor.a += (Time.deltaTime / m_fadeTimeScale);
            m_nowColor.a = m_nowColor.a > 1.0f ? 1.0f : m_nowColor.a;
            m_fadeImg.color = m_nowColor;
            yield return null;
        }

        AsyncOperation _asyncO = SceneManager.LoadSceneAsync(argSceneName);

        while (!_asyncO.isDone) yield return null;

        while (IsWaitFlag) yield return null;

        m_nowColor = m_colorArr[1];

        while (m_nowColor.a != m_colorArr[0].a)
        {
            m_nowColor.a -= (Time.deltaTime / m_fadeTimeScale);
            m_nowColor.a = m_nowColor.a < 0.0f ? 0.0f : m_nowColor.a;
            m_fadeImg.color = m_nowColor;
            yield return null;
        }

        m_fadeImg.raycastTarget = false;
        m_changeSceneFlag = false;
    }
}
