using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // gán Canvas PauseMenu ở đây
    private bool isPaused = false;


    private void Update()
    {
        // Nếu đang ở scene Home hoặc End thì không cho pause
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "HomeScene" || currentScene == "EndScene")
        {
            return;
        }

        // Bấm ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitToHome()
    {
        Time.timeScale = 1f; // khôi phục trước khi load scene
        SceneManager.LoadScene("HomeScene");
    }
}
