using UnityEngine;

public class SceneMoveBtn : MonoBehaviour
{
    /// <summary>
    /// 이동할 씬 이름
    /// (단 Exit 일 경우 게임 종료)
    /// </summary>
    [SerializeField] string m_sceneName = string.Empty;

    public void ButtonClick()
    {
        switch (m_sceneName.ToUpper())
        {
            case "EXIT":
                Application.Quit();
                break;
            default:
                GManager.Instance.IsScene.Load(m_sceneName);
                break;
        }
    }
}
