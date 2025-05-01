using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private Transform _playerTransform;

    void Awake()
    {
        // Singleton để không bị duplicate
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _playerTransform = transform;  // giả sử script này gắn trên Player
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.Follow = _playerTransform;
            //vcam.LookAt = _playerTransform;
        }
        // Chỉ reset khi vào Scene1
        if (scene.name == "Scene1")
        {
            // Tìm spawn point
            GameObject spawn = GameObject.Find("PlayerSpawn");
            if (spawn != null)
            {
                _playerTransform.position = spawn.transform.position;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy PlayerSpawn trong Scene1!");
            }
        }
    }
}
