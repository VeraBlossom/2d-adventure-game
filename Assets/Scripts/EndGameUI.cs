using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("HomeScene"); // Doi thanh scene chinh
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Cho chay trong editor
#endif
        
    }
}
