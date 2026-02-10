using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "EntityData", menuName = "SNJ/New Entity Data", order = 1)]
public class EntityData : ScriptableObject
{
    /// <summary>
    /// 독립체 타입
    /// </summary>
    [SerializeField] EntityType.TYPE m_entityType = EntityType.TYPE.User;

    /// <summary>
    /// 독립체 인덱스
    /// </summary>
    [SerializeField] int m_entityIndex = 0;

    /// <summary>
    ///  이미지 라이브러리
    /// </summary>
    [SerializeField] SpriteLibraryAsset m_spLibAsset = null;

    /// <summary>
    /// 애니메이션 스케일
    /// 1일경우 1초에 한 번 애니메이션 재생
    /// </summary>
    [SerializeField] float m_aniScale = 0.0f;

    /// <summary>
    /// 이동속도 배열
    /// 0: 걷기, 1: 뛰기
    /// </summary>
    [SerializeField] float[] m_speedArr = { 0.0f, 0.0f };

    /// <summary>
    /// 최대 체력
    /// </summary>
    [SerializeField] float m_maxHp = 0.0f;

    /// <summary>
    /// 최대 스테미나
    /// </summary>
    [SerializeField] float m_maxStamina = 0.0f;

    /// <summary>
    /// 탐색 범위
    /// </summary>
    [SerializeField] float m_searchLength = 0.0f;

    /// <summary>
    /// 공격력
    /// x: 최소, y: 최대
    /// </summary>
    [SerializeField] Vector2Int m_atk = Vector2Int.zero;

    /// <summary>
    /// 이펙트 배열
    /// 0: 히트, 1: 죽음, 2: 공격
    /// </summary>
    [SerializeField] GameObject[] m_effectArr = null;

    /// <summary>
    /// 독립체 타입
    /// </summary>
    public EntityType.TYPE IsEntityType { get { return m_entityType; } }

    /// <summary>
    /// 독립체 인덱스
    /// </summary>
    public int IsEntityIndex { get { return m_entityIndex; } }

    /// <summary>
    /// 스프라이트 라이브러리 에셋
    /// </summary>
    public SpriteLibraryAsset IsSpLibAsset { get { return m_spLibAsset; } }

    /// <summary>
    /// 애니메이션 스케일
    /// </summary>
    public float IsAniScale { get { return m_aniScale; } }

    /// <summary>
    /// 이동속도
    /// 0: 걷기, 1: 뛰기
    /// </summary>
    public float[] IsSpeedArr { get { return m_speedArr; } }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float IsMaxHp { get { return m_maxHp; } }

    /// <summary>
    /// 최대 스테미나
    /// </summary>
    public float IsMaxStamina { get { return m_maxStamina; } }

    /// <summary>
    /// 탐색 범위
    /// </summary>
    public float IsSearchLength { get { return m_searchLength; } }

    /// <summary>
    /// 데미지
    /// </summary>
    public int IsDamage { get { return Random.Range(m_atk.x, m_atk.y + 1); } }
    
    /// <summary>
    /// 이펙트 생성 및 반환
    /// </summary>
    /// <param name="argIndex">이펙트 인덱스</param>
    /// <param name="argTrans">부모 트랜스폼</param>
    /// <returns>생성된 오브젝트</returns>
    public GameObject CreateEffect(int argIndex, Transform argTrans)
    {
        GameObject _obj = Instantiate(m_effectArr[argIndex], argTrans);
        _obj.transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
        _obj.name = $"Effect_{argIndex}";

        return _obj;
    }
}
