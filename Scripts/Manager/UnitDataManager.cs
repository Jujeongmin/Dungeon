using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    /// <summary>
    /// 독립체 데이터 리스트
    /// </summary>
    [SerializeField] List<EntityData> m_entityDataList = null;

    /// <summary>
    /// 데이터취득
    /// </summary>
    /// <param name="argEntityType">독립체 타입</param>
    /// <param name="argEntityIndex">독립체 인덱스</param>
    /// <returns></returns>
    public EntityData Get(EntityType.TYPE argEntityType, int argEntityIndex)
    {
        return m_entityDataList.Find(_data => _data.IsEntityType == argEntityType && _data.IsEntityIndex == argEntityIndex);
    }
}
