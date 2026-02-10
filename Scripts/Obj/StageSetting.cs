using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class StageSetting : MonoBehaviour
{
    /// <summary>
    /// 유저 라이트
    /// </summary>
    [SerializeField] Light2D m_uLight = null;

    /// <summary>
    /// 글로벌 라이트
    /// </summary>
    [SerializeField] Light2D m_gLight = null;

    /// <summary>
    /// 글로벌 라이트 밝기
    /// </summary>
    [SerializeField] float m_gLightIntensity = 0.0f;

    /// <summary>
    /// 충돌 타일맵
    /// </summary>
    [SerializeField] Tilemap m_obsMap = null;

    /// <summary>
    /// 충돌 타일맵 칼라
    /// </summary>
    [SerializeField] Color m_color = Color.white;

    /// <summary>
    /// 시작 시 재생할 BGM 클립
    /// </summary>
    [SerializeField] AudioClip m_bgmClip = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_obsMap != null) m_obsMap.color = m_color;
        if(m_gLight != null) m_gLight.intensity = m_gLightIntensity;
        if(m_uLight != null) m_uLight.pointLightOuterRadius = GManager.Instance.IsUnitData.Get(EntityType.TYPE.User, 0).IsSearchLength;
        if(m_bgmClip != null) GManager.Instance.IsSound.Play(AudioType.TYPE.BGM, m_bgmClip);
    }
}
