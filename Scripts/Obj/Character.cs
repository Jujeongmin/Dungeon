using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
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
    /// 컨트롤러
    /// </summary>
    ParentsController m_controller = null;

    void Start()
    {
        Setting();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    void Setting()
    {
        System.Type _type = System.Type.GetType($"{m_entityType}Controller");
        m_controller = gameObject.AddComponent(_type) as ParentsController;
        m_controller.Setting(m_entityType, m_entityIndex);
    }

    void Update()
    {
        if (m_controller == null || GManager.Instance.IsDieFlag) return;
        m_controller.Attack();
        m_controller.Hit();
        m_controller.Move();
        m_controller.AniPlay();
    }

#if UNITY_EDITOR
    /// <summary>
    /// 캐릭터의 위치 표시
    /// </summary>
    private void OnDrawGizmos()
    {
        string _viewStr = m_entityType == EntityType.TYPE.User ? $"◀ {m_entityType}" : $"◀ {m_entityType}_{m_entityIndex}";
        Handles.Label(transform.position, $"{_viewStr}");
    }
#endif
}
