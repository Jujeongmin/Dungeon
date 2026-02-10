using UnityEngine;

public class GManager : MonoBehaviour
{
    /// <summary>
    /// 유저의 트랜스폼
    /// </summary>
    public Transform IsUserTrans { get; set; } = null;

    /// <summary>
    /// 유저 컨트롤러 스크립트
    /// </summary>
    public UserController IsUserSc { get; set; } = null;

    /// <summary>
    /// 충돌 타일맵 스크립트
    /// </summary>
    public ObsMap IsObsMap { get; set; } = null;

    /// <summary>
    /// 죽음 플래그
    /// </summary>
    public bool IsDieFlag { get; set; } = false;

    /// <summary>
    /// 충돌 박스 사이즈
    /// </summary>
    [SerializeField] Vector2 m_boxSize = Vector2.zero;

    /// <summary>
    /// 유닛 데이터 매니저
    /// </summary>
    [SerializeField] UnitDataManager m_unitDataManager = null;

    /// <summary>
    /// 사운드 매니저
    /// </summary>
    [SerializeField] SoundManager m_soundManager = null;

    /// <summary>
    /// 씬이동 매니저
    /// </summary>
    [SerializeField] LoadSceneManager m_loadSceneManager = null;

    /// <summary>
    /// 죽음 스프라이트
    /// </summary>
    [SerializeField] Sprite m_dieSprite = null;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    public static GManager Instance { get; private set; } = null;

    private void Awake()
    {
        switch (GManager.Instance)
        {
            case null:
                Instance = this;
                DontDestroyOnLoad(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    /// <summary>
    /// 유닛 데이터 매니저
    /// </summary>
    public UnitDataManager IsUnitData { get { return m_unitDataManager; } }

    /// <summary>
    /// 사운드 매니저
    /// </summary>
    public SoundManager IsSound { get { return m_soundManager; } }

    /// <summary>
    /// 씬 이동 매니저
    /// </summary>
    public LoadSceneManager IsScene { get { return m_loadSceneManager; } }

    /// <summary>
    /// 2D 박스 충돌 체크
    /// </summary>
    /// <param name="argPos">체크할 위치</param>
    /// <param name="argLayerMask">체크할 레이어</param>
    /// <returns>true: 충돌, false: 미충돌</returns>
    public bool CheckColliderBox2D(Vector3 argPos, LayerMask argLayerMask)
    {
        return Physics2D.OverlapBox(argPos, m_boxSize, 0.0f, argLayerMask) == null ? false : true;
    }

    /// <summary>
    /// 2D 박스 충돌 체크
    /// </summary>
    /// <param name="argPos">체크할 위치</param>
    /// <param name="argLayerMask">체크할 레이어</param>
    /// <param name="argObj">충돌발생시 반환할 오브젝트</param>
    /// <returns>true: 충돌, false: 미충돌</returns>
    public bool CheckColliderBox2D(Vector3 argPos, LayerMask argLayerMask, ref GameObject argObj)
    {
        Collider2D _coll = Physics2D.OverlapBox(argPos, m_boxSize, 0.0f, argLayerMask);
        argObj = _coll == null ? null : _coll.gameObject;

        return _coll == null ? false : true;
    }

    /// <summary>
    /// 유저와 몹과의 거리 체크
    /// </summary>
    /// <param name="argTrans">몹의 트랜스폼</param>
    /// <param name="argLength">측정 범위</param>
    /// <returns>true: 측정 범위 안, false: 측정 범위 밖</returns>
    public bool CheckSearchLength(Transform argTrans, float argLength)
    {
        return Vector2.Distance(IsUserTrans.position, argTrans.position) <= argLength ? true : false;
    }

    /// <summary>
    /// 공격 가능 거리
    /// </summary>
    public float IsAtkLength { get { return m_boxSize.x; } }

    /// <summary>
    /// 죽음 스프라이트
    /// </summary>
    public Sprite IsDieSprite { get { return m_dieSprite; } }
}
