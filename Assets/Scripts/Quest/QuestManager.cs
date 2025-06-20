using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [SerializeField] private QuestCardNPC QuestCardNpcPrefab;
    [SerializeField] private Transform npcPanelContainer;

    private void Start()
    {
        LoadQuestsIntoNPCPanel();
    }

    private void LoadQuestsIntoNPCPanel()
    {
        // load each quest into the NPC panel
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcCard = Instantiate(QuestCardNpcPrefab, npcPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);
        }
    }
}
