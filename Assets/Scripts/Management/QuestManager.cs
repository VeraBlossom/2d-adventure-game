using UnityEngine;
using System;

public enum QuestState { NotStarted, InProgress, Completed }

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public QuestState CurrentState { get; private set; } = QuestState.NotStarted;
    public string QuestTitle { get; private set; }
    public string QuestDescription { get; private set; }
    public int CurrentProgress { get; private set; }
    public int TargetProgress { get; private set; }

    public event Action OnQuestUpdated;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    // Goi khi nhan nhiem vu
    public void StartQuest(string title, string description, int target)
    {
        
        QuestTitle = title;
        QuestDescription = description;
        TargetProgress = target;
        CurrentProgress = 0;
        CurrentState = QuestState.InProgress;
        Debug.Log("[QuestManager] StartQuest invoked");
        OnQuestUpdated?.Invoke();
        Debug.Log("[QuestManager] OnQuestUpdated fired");
    }

    // Goi khi cap nhat tien do
    public void UpdateProgress(int amount = 1)
    {
        if (CurrentState != QuestState.InProgress) return;
        CurrentProgress = Mathf.Clamp(CurrentProgress + amount, 0, TargetProgress);
        if (CurrentProgress >= TargetProgress)
            CompleteQuest();
        OnQuestUpdated?.Invoke();
    }

    private void CompleteQuest()
    {
        CurrentState = QuestState.Completed;
        OnQuestUpdated?.Invoke();
    }
}

