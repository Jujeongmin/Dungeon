using UnityEngine;

public class CameraBase : MonoBehaviour
{
    /// <summary>
    /// 유저 무기 애니메이터
    /// </summary>
    [SerializeField] Animator m_weaponAnimator = null;

    /// <summary>
    /// 애니메이션 플래그
    /// </summary>
    public bool IsAniFlag { get; private set; } = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GManager.Instance.IsUserTrans == null) return;

        transform.position = GManager.Instance.IsUserTrans.position;
    }

    /// <summary>
    /// 공격
    /// </summary>
    /// <param name="argDirType">방향 타입</param>
    public void Attack(DirType.TYPE argDirType)
    {
        if (IsAniFlag) return;

        IsAniFlag = true;
        m_weaponAnimator.SetFloat("Dir", (float)argDirType);
        m_weaponAnimator.SetBool("AtkFlag", IsAniFlag);
    }

    /// <summary>
    /// 애니메이션 종료
    /// </summary>
    public void EndAni()
    {
        IsAniFlag = false;
        m_weaponAnimator.SetBool("AtkFlag", IsAniFlag);
    }
}
