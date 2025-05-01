using UnityEngine;
using TMPro;

public class QuestPanelController : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMP_Text questTitleText;
    [SerializeField] private TMP_Text questDescText;
    [SerializeField] private TMP_Text questProgressText;

    private void Start()
    {
        QuestManager.Instance.OnQuestUpdated += RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        QuestManager.Instance.OnQuestUpdated -= RefreshUI;
    }

    private void RefreshUI()
    {
        Debug.Log($"[QuestPanelController] RefreshUI called; state={QuestManager.Instance.CurrentState}");
        var qm = QuestManager.Instance;
        if (qm.CurrentState == QuestState.InProgress)
        {
            questPanel.SetActive(true);
            questTitleText.text = qm.QuestTitle;
            questDescText.text = qm.QuestDescription;
            questProgressText.text = $"{qm.CurrentProgress}/{qm.TargetProgress}";
        }
        else
        {
            questPanel.SetActive(false);
        }
    }
}

