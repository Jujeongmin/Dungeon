using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ParentsController : MonoBehaviour
{
    /// <summary>
    /// 독립체 데이터
    /// </summary>
    public EntityData IsData { get; private set; } = null;

    /// <summary>
    /// 스프라이트 라이브러리
    /// </summary>
    public SpriteLibrary IsSpLib { get; private set; } = null;

    /// <summary>
    /// 스프라이트 리졸버
    /// </summary>
    public SpriteResolver IsSpResolver { get; private set; } = null;

    /// <summary>
    /// 입력
    /// </summary>
    public Vector2 IsInput = Vector2.zero;

    /// <summary>
    /// 이동 타입
    /// </summary>
    public MoveType.TYPE IsMoveType { get; set; } = MoveType.TYPE.Idle;

    /// <summary>
    /// 방향 타입
    /// </summary>
    public DirType.TYPE IsDirType { get; set; } = DirType.TYPE.Down;

    /// <summary>
    /// 달리기 인덱스
    /// </summary>
    public int IsRunIndex { get; set; } = 0;

    /// <summary>
    /// 애니메이션 인덱스
    /// </summary>
    public int IsAniIndex { get; set; } = 0;

    /// <summary>
    /// 애니메이션 카테고리
    /// </summary>
    public string IsAniCategory { get; set; } = string.Empty;

    /// <summary>
    /// 현재의 애니메이션 스케일
    /// </summary>
    public float IsNowAniScale { get; set; } = 0.0f;

    /// <summary>
    /// 이동 목적지
    /// </summary>
    public Vector3 IsTargetPos = Vector3.zero;

    /// <summary>
    /// 셋팅 플래그
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;

    /// <summary>
    /// 이동 플래그
    /// </summary>
    public bool IsMoveFlag { get; set; } = false;

    /// <summary>
    /// 무적 시간
    /// </summary>
    public float IsHitTime { get; set; } = 0.0f;

    /// <summary>
    /// 현재의 HP
    /// </summary>
    public float IsNowHp { get; set; } = 0.0f;

    /// <summary>
    /// 현재의 스테미나
    /// </summary>
    public float IsNowStamina { get; set; } = 0.0f;

    /// <summary>
    /// 공격 받는 레이어 마스크
    /// </summary>
    public LayerMask IsHitMask { get; set; }

    /// <summary>
    /// 죽음 플래그
    /// </summary>
    public bool IsDieFlag { get; set; } = false;

    /// <summary>
    /// 에어리어 마스크
    /// </summary>
    LayerMask m_areaMask;

    /// <summary>
    /// 초기 셋팅
    /// </summary>
    /// <param name="argEntityType">독립체 타입</param>
    /// <param name="argEntityIndex">독립체 인덱스</param>
    public virtual void Setting(EntityType.TYPE argEntityType, int argEntityIndex)
    {
        IsSpLib = GetComponent<SpriteLibrary>();
        IsSpResolver = GetComponent<SpriteResolver>();
        IsData = GManager.Instance.IsUnitData.Get(argEntityType, argEntityIndex);
        IsSpLib.spriteLibraryAsset = IsData.IsSpLibAsset;
        IsSpResolver.SetCategoryAndLabel("IdleDown", "0");

        IsNowHp = IsData.IsMaxHp;
        IsNowStamina = IsData.IsMaxStamina;

        m_areaMask = 1 << LayerMask.NameToLayer("Area");
    }

    /// <summary>
    /// 이동
    /// </summary>
    public virtual void Move() { }

    /// <summary>
    /// 공격
    /// </summary>
    public virtual void Attack() { }

    /// <summary>
    /// 공격 받기
    /// </summary>
    public virtual void Hit() { }

    /// <summary>
    /// 방향 및 목적지 체크
    /// </summary>
    /// <param name="argInput"></param>
    public void CheckDirNTargetPos(Vector2 argInput)
    {
        argInput.y = Mathf.Abs(argInput.x) > 0.0f ? 0.0f : argInput.y;
        IsTargetPos = transform.position;

        switch (argInput.normalized)
        {
            case Vector2 v when v.Equals(Vector2.down):
                IsDirType = DirType.TYPE.Down;
                IsTargetPos += Vector3.down;
                break;
            case Vector2 v when v.Equals(Vector2.left):
                IsDirType = DirType.TYPE.Left;
                IsTargetPos += Vector3.left;
                break;
            case Vector2 v when v.Equals(Vector2.right):
                IsDirType = DirType.TYPE.Right;
                IsTargetPos += Vector3.right;
                break;
            case Vector2 v when v.Equals(Vector2.up):
                IsDirType = DirType.TYPE.Up;
                IsTargetPos += Vector3.up;
                break;
        }
    }

    /// <summary>
    /// 한칸 이동
    /// </summary>
    /// <returns></returns>
    public IEnumerator OneStep()
    {
        IsMoveFlag = true;
        IsMoveType = MoveType.TYPE.Walk;

        while (transform.position != IsTargetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, IsTargetPos, IsData.IsSpeedArr[IsRunIndex] * Time.deltaTime);
            yield return null;
        }

        GameObject _collObj = null;
        if (GManager.Instance.CheckColliderBox2D(transform.position, m_areaMask, ref _collObj))
        {
            Area _area = _collObj.GetComponent<Area>();

            if (_area.IsMoveFlag)
            {
                IsTargetPos = transform.position = _area.IsTargetTrans.position;
                if (_area.IsBGMClip != null) GManager.Instance.IsSound.Play(AudioType.TYPE.BGM, _area.IsBGMClip);
            }
        }

        IsMoveFlag = false;
    }

    /// <summary>
    /// 현재의 애니메이션 카테고리 반환
    /// </summary>
    /// <param name="argMoveType">이동 타입</param>
    /// <param name="argDirType">방향 타입</param>
    /// <returns>애니메이션 카테고리</returns>
    public string NowCategory(MoveType.TYPE argMoveType, DirType.TYPE argDirType)
    {
        return $"{argMoveType}{argDirType}";
    }

    /// <summary>
    /// 애니메이션 플레이
    /// </summary>
    public void AniPlay()
    {
        if (!IsSettingFlag || GManager.Instance.IsDieFlag) return;

        switch (NowCategory(IsMoveType, IsDirType))
        {
            case string s when s.Equals(IsAniCategory):
                IsNowAniScale -= Time.deltaTime;
                if (IsNowAniScale <= 0.0f)
                {
                    IsNowAniScale = IsRunIndex == 0 ? IsData.IsAniScale : IsData.IsAniScale * (IsData.IsSpeedArr[0] / IsData.IsSpeedArr[1]);
                    IsAniIndex++;
                    IsAniIndex = IsAniIndex % 4;
                    if (IsMoveType != MoveType.TYPE.Idle && IsData.IsEntityType == EntityType.TYPE.User)
                    {
                        switch (IsAniIndex)
                        {
                            case 1:
                                GManager.Instance.IsSound.FootPlay(0);
                                break;
                            case 3:
                                GManager.Instance.IsSound.FootPlay(1);
                                break;
                        }
                    }
                }
                break;
            default:
                IsNowAniScale = IsRunIndex == 0 ? IsData.IsAniScale : IsData.IsAniScale * (IsData.IsSpeedArr[0] / IsData.IsSpeedArr[1]);
                IsAniIndex = 0;
                IsAniCategory = NowCategory(IsMoveType, IsDirType);
                break;
        }

        IsAniIndex = IsMoveType == MoveType.TYPE.Idle ? 0 : IsAniIndex;
        IsSpResolver.SetCategoryAndLabel(IsAniCategory, IsAniIndex.ToString());
    }
}
