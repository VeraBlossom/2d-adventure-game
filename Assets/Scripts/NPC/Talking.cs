using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Talking : MonoBehaviour
{
    [Header("UI References")]
    //[SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private TMP_Text dialogueText;

    [Header("Dialogue Settings")]
    [SerializeField] private List<string> firstTimeDialogues;
    [SerializeField] private string repeatDialogue = "Complete your mission";

    [Header("Quest Settings")]
    [SerializeField] private string questTitle;           // ← tieu de nhiem vu
    [SerializeField][TextArea] private string questDesc; // ← mo ta nhiem vu
    [SerializeField] private int questTarget = 1;         // ← tien trinh nhiem vu

    private List<string> currentDialogues;
    private int currentLineIndex = 0;
    private bool isTalking = false;
    private bool hasTalkedBefore = false;

    private void Update()
    {
        if (isTalking && Input.GetMouseButtonDown(0))
            ShowNextLine();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTalking)
            StartDialogue();
    }

    private void StartDialogue()
    {
        isTalking = true;
        UIFade.Instance.dialoguePanel.SetActive(true);
        Time.timeScale = 0f;

        currentDialogues = !hasTalkedBefore
            ? new List<string>(firstTimeDialogues)
            : new List<string> { repeatDialogue };

        currentLineIndex = 0;
        UIFade.Instance.dialogueText.text = currentDialogues[currentLineIndex];
    }

    private void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentDialogues.Count)
        {
            UIFade.Instance.dialogueText.text = currentDialogues[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        UIFade.Instance.dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        if (!hasTalkedBefore)
        {
            // Dung gia tri tu Inspector
            QuestManager.Instance.StartQuest(questTitle, questDesc, questTarget);
            hasTalkedBefore = true;
        }
    }
}
