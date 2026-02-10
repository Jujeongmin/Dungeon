using UnityEngine;

public class Area : MonoBehaviour
{
    /// <summary>
    /// 목적지 트랜스폼
    /// </summary>
    [SerializeField] Transform m_targetPosTrans = null;

    /// <summary>
    /// 이동 시 배경음악 변경용
    /// </summary>
    [SerializeField] AudioClip m_clip = null;

    [Header("[ 0: 삭제, 1: 생성 ]")]
    /// <summary>
    /// 몹 베이스 오브젝트
    /// 0: 삭제, 1: 로딩
    /// </summary>
    [SerializeField] MobBase[] m_mobBaseArr = { null, null };

    /// <summary>
    /// 키 트랜스폼
    /// </summary>
    [SerializeField] Transform m_keyTrans = null;

    /// <summary>
    /// 목적지 트랜스폼
    /// </summary>
    public Transform IsTargetTrans
    {
        get
        {
            if (m_mobBaseArr[0] != null) m_mobBaseArr[0].IsView = false;
            if (m_mobBaseArr[1] != null) m_mobBaseArr[1].IsView = true;

            return m_targetPosTrans;
        }
    }

    /// <summary>
    /// 배경음악 변경용 클립
    /// </summary>
    public AudioClip IsBGMClip { get { return m_clip; } }

    /// <summary>
    /// 이동 가능 체크
    /// </summary>
    public bool IsMoveFlag { get { return m_keyTrans != null && m_keyTrans.childCount > 0 ? false : true; } }
}
