using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [Header("NPC Quest Panel")]
    [SerializeField] private QuestCardNPC QuestCardNpcPrefab;
    [SerializeField] private Transform npcQuestPanelContainer;

    [Header("Player Quest Panel")]
    [SerializeField] private QuestCardPlayer QuestCardPlayerPrefab;
    [SerializeField] private Transform playerQuestPanelContainer;

    private void Start()
    {
        LoadQuestsIntoNPCPanel();
    }

    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer cardPlayer = Instantiate(QuestCardPlayerPrefab, playerQuestPanelContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    public void AddProgress(string questID, int amount)
    {
        Quest questToUpdate = QuestExistis(questID);
        if (questToUpdate == null) return;

        if (questToUpdate.QuestAccepted)
        {
            questToUpdate.AddProgress(amount);
        }
    }

    // check if quest exists in the quest list and return it if it does
    private Quest QuestExistis(string questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
            {
                return quest;
            }
        }

        return null;
    }

    private void LoadQuestsIntoNPCPanel()
    {
        // load each quest into the NPC panel
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcCard = Instantiate(QuestCardNpcPrefab, npcQuestPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }
}
