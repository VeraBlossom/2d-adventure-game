using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialUI : MonoBehaviour
{
    public GameObject instructionText;  // Text hướng dẫn, gán từ Inspector
    public static bool inputBlocked = true;
    private bool waitingForInput = false;

    void Start()
    {
        instructionText.SetActive(false);  // Ẩn chữ ngay từ đầu
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        inputBlocked = true;

        // Chờ 2 giây
        yield return new WaitForSeconds(2f);

        // Hiện dòng chữ hướng dẫn
        instructionText.SetActive(true);

        // Chờ thêm 1 giây
        yield return new WaitForSeconds(1f);

        // Bắt đầu lắng nghe phím bấm
        waitingForInput = true;
    }

    void Update()
    {
        if (waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                UnlockInput();
            }
        }
    }

    void UnlockInput()
    {
        inputBlocked = false;
        waitingForInput = false;
        instructionText.SetActive(false);
    }

    // Hàm này ví dụ cho việc kiểm soát input ở nơi khác trong game
    public bool IsInputBlocked()
    {
        return inputBlocked;
    }
}
