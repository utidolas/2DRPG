using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/New Quest")]
public class Quest : ScriptableObject
{
    [Header("Quest Information")]
    public string Name;
    public string ID;
    public int QuestGoal;

    [Header("Description")]
    [TextArea] public string Description;

    [Header("Reward")]
    public int GoldReward;
    public float ExpReward;
    public QuestItemReward ItemReward;

    [HideInInspector] public int CurrentStatus;
    [HideInInspector] public bool IsQuestCompleted;
    [HideInInspector] public bool QuestAccepted;

    public void AddProgress(int amount)
    {
        CurrentStatus += amount;
        if(CurrentStatus >= QuestGoal)
        {
            CurrentStatus = QuestGoal; // Ensure it doesn't exceed the goal
            QuestIsCompleted();
        }
    }

    public void QuestIsCompleted()
    {
        if (IsQuestCompleted)
        {
            return;
        }

        IsQuestCompleted = true;
    }

    public void ResetQuest()
    {
        QuestAccepted = false;
        IsQuestCompleted = false;
        CurrentStatus = 0;
    }
}

[Serializable]
public class QuestItemReward
{
    public InventoryItem Item;
    public int Quantity;
}
