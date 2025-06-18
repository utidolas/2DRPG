using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private NPCDialogue dialogueToShow;
    [SerializeField] private GameObject interactionBox; // range that interaction button will appear to player 

    // prop to get 'dialogueToShow'
    public NPCDialogue DialogueToShow => dialogueToShow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player enter collider show interaction box
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSelected = this;
            interactionBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if player leaves collider hide interaction box
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSelected = this;
            DialogueManager.Instance.CloseDialoguePanel();
            interactionBox.SetActive(false);
        }
    }
}
