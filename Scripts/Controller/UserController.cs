using UnityEngine;

public class UserController : ParentsController
{
    /// <summary>
    /// 벽 충돌 마스크
    /// </summary>
    LayerMask m_obsMask;

    /// <summary>
    /// 카메라 베이스 스크립트
    /// </summary>
    CameraBase m_cameraBaseSc = null;

    /// <summary>
    /// 죽음 이펙트 오브젝트
    /// </summary>
    GameObject m_dieEffect = null;

    /// <summary>
    /// 초기 셋팅
    /// </summary>
    /// <param name="argEntityType">독립체 타입</param>
    /// <param name="argEntityIndex">독립체 인덱스</param>
    public override void Setting(EntityType.TYPE argEntityType, int argEntityIndex)
    {
        base.Setting(argEntityType, argEntityIndex);
        GManager.Instance.IsDieFlag = false;
        gameObject.name = argEntityType.ToString();
        GManager.Instance.IsUserTrans = transform;
        GManager.Instance.IsUserSc = this;
        m_obsMask = 1 << LayerMask.NameToLayer("Obstacle");
        IsHitMask = 1 << LayerMask.NameToLayer("MWeapon");
        m_cameraBaseSc = GameObject.Find("CameraBase").GetComponent<CameraBase>();
        m_dieEffect = IsData.CreateEffect(0, transform);
        m_dieEffect.SetActive(false);
        IsSettingFlag = true;
    }

    /// <summary>
    /// 이동
    /// </summary>
    public override void Move()
    {
        if (!IsSettingFlag || IsDieFlag) return;

        IsRunIndex = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;

        if (IsMoveFlag) return;

        IsInput.x = Input.GetAxisRaw("X");
        IsInput.y = Input.GetAxisRaw("Y");

        switch (IsInput)
        {
            case Vector2 v when v.Equals(Vector2.zero):
                IsMoveType = MoveType.TYPE.Idle;
                IsTargetPos = transform.position;
                break;
            default:
                CheckDirNTargetPos(IsInput);

                if (GManager.Instance.CheckColliderBox2D(IsTargetPos, m_obsMask))
                {
                    IsMoveType = MoveType.TYPE.Idle;
                    return;
                }

                StartCoroutine(OneStep());
                break;
        }
    }

    /// <summary>
    /// 공격
    /// </summary>
    public override void Attack()
    {
        if (!IsSettingFlag || IsDieFlag) return;

        if (Input.GetAxis("Fire1") > 0.0f) m_cameraBaseSc.Attack(IsDirType);
    }

    /// <summary>
    /// 공격 받기
    /// </summary>
    public override void Hit()
    {
        if (!IsSettingFlag || IsDieFlag) return;

        if (GManager.Instance.CheckColliderBox2D(transform.position, IsHitMask))
        {
            GManager.Instance.IsDieFlag = true;
            IsMoveType = MoveType.TYPE.Idle;
            m_dieEffect.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = GManager.Instance.IsDieSprite;
        }
    }

    /// <summary>
    /// 데미지
    /// </summary>
    public int IsDamage { get { return IsData.IsDamage; } }
}
