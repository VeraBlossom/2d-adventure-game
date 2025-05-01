using UnityEngine;
using TMPro;
using System.Collections;

public class QuestBlocker : MonoBehaviour
{
    [Header("Gold Requirement")]
    [SerializeField] private int requiredGold = 50;

    [Header("UI")]
    [SerializeField] private GameObject completeTextObject;
    [SerializeField] private GameObject notEnoughGoldTextObject;

    private bool isWaitingForGold = false;

    private void Start()
    {
        QuestManager.Instance.OnQuestUpdated += CheckQuestStatus;
    }

    private void OnDestroy()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestUpdated -= CheckQuestStatus;
    }

    private void CheckQuestStatus()
    {
        if (IsQuestCompleted())
        {
            Debug.Log("Quest completed");
            TryToPass();
        }
    }

    private void TryToPass()
    {
        if (!IsQuestCompleted())
        {
            // chua hoan thanh nhiem vu thi chua hco qua
            Debug.Log("[QuestBlocker] Quest not completed yet.");
            return;
        }

        if (EconomyManager.Instance.HasEnoughGold(requiredGold))
        {
            EconomyManager.Instance.SpendGold(requiredGold);
            if (completeTextObject != null)
            {
                completeTextObject.SetActive(true);
                StartCoroutine(HideTextAndBlocker());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("[QuestBlocker] Not enough gold, waiting for gold...");
            if (notEnoughGoldTextObject != null)
                notEnoughGoldTextObject.SetActive(true);
            if (!isWaitingForGold)
                StartCoroutine(WaitForGold());
        }
    }

    private IEnumerator WaitForGold()
    {
        isWaitingForGold = true;
        while (!EconomyManager.Instance.HasEnoughGold(requiredGold) || !IsQuestCompleted())
        {
            yield return new WaitForSeconds(1f);
        }

        if (notEnoughGoldTextObject != null)
            notEnoughGoldTextObject.SetActive(false);

        TryToPass(); // du vang va hoan thanh nhiem vu thi goi
    }

    private IEnumerator HideTextAndBlocker()
    {
        yield return new WaitForSeconds(5f);
        if (completeTextObject != null)
            completeTextObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private bool IsQuestCompleted()
    {
        return QuestManager.Instance != null && QuestManager.Instance.CurrentState == QuestState.Completed;
    }
}
