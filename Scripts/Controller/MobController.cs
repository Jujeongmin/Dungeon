using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class MobController : ParentsController
{
    /// <summary>
    /// 페스 리스트
    /// </summary>
    List<Vector3> m_pathList = null;

    /// <summary>
    /// 이펙트 오브젝트 배열
    /// 0: 히트, 1: 죽음, 2: 공격
    /// </summary>
    GameObject[] m_effectArr = { null, null, null };

    /// <summary>
    /// 탐색 플래그
    /// </summary>
    bool m_searchFlag = false;

    /// <summary>
    /// 초기 셋팅
    /// </summary>
    /// <param name="argEntityType">독립체 타입</param>
    /// <param name="argEntityIndex">독립체 인덱스</param>
    public override void Setting(EntityType.TYPE argEntityType, int argEntityIndex)
    {
        base.Setting(argEntityType, argEntityIndex);
        gameObject.name = $"{argEntityType}_{argEntityIndex}";
        IsHitMask = 1 << LayerMask.NameToLayer("PWeapon");
        for (int i = 0; i < m_effectArr.Length; i++)
        {
            m_effectArr[i] = IsData.CreateEffect(i, transform);
            m_effectArr[i].SetActive(false);
        }
        IsSettingFlag = true;
    }

    /// <summary>
    /// 이동
    /// </summary>
    public override void Move()
    {
        if (!IsSettingFlag || IsDieFlag) return;

        if (!m_searchFlag)
        {
            m_searchFlag = GManager.Instance.CheckSearchLength(transform, IsData.IsSearchLength);
            return;
        }

        //IsRunIndex = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;

        if (IsMoveFlag) return;

        m_pathList = AStar.FindPath(GManager.Instance.IsObsMap.IsMap, transform.position, GManager.Instance.IsUserTrans.position);

        if (m_pathList == null || m_pathList.Count <= 1)
        {
            IsMoveType = MoveType.TYPE.Idle;
            IsTargetPos = transform.position;
            return;
        }

        IsInput = m_pathList[1] - m_pathList[0];

        CheckDirNTargetPos(IsInput);
        StartCoroutine(OneStep());
    }

    /// <summary>
    /// 공격
    /// </summary>
    public override void Attack()
    {
        if (!IsSettingFlag || IsDieFlag || m_effectArr[2].activeInHierarchy) return;

        if (GManager.Instance.CheckSearchLength(transform, GManager.Instance.IsAtkLength))
        {
            m_effectArr[2].SetActive(true);
        }
    }

    /// <summary>
    /// 공격 받기
    /// </summary>
    public override void Hit()
    {
        if (!IsSettingFlag || IsDieFlag) return;
        IsHitTime = IsHitTime <= 0.0f ? 0.0f : IsHitTime - Time.deltaTime;
        if (IsHitTime > 0.0f) return;

        if (GManager.Instance.CheckColliderBox2D(transform.position, IsHitMask))
        {
            IsNowHp -= GManager.Instance.IsUserSc.IsDamage;
            IsNowHp = IsNowHp <= 0.0f ? 0.0f : IsNowHp;
            switch (IsNowHp)
            {
                case 0.0f:
                    IsDieFlag = true;
                    IsMoveType = MoveType.TYPE.Idle;
                    m_effectArr[1].SetActive(true);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 1.0f);
                    break;
                default:
                    m_effectArr[0].SetActive(true);
                    break;
            }
            IsHitTime = 0.5f;
        }
    }
}
