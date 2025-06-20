using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [Header("Config")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;

    public NPCInteraction NPCSelected { get; set; }

    private bool hasDialogueStarted;
    private PlayerActions actions;
    private Queue<string> dialogueQueue = new Queue<string>(); // store dialogue of selected NPC in this collection

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialogue();
        actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        hasDialogueStarted = false;
        // reset dialogue queue
        dialogueQueue.Clear();
    }

    private void LoadDialogueFromNPC()
    {
        if (NPCSelected.DialogueToShow.Dialogue.Length <= 0) return;
        foreach (string sentence in NPCSelected.DialogueToShow.Dialogue)
        {
            dialogueQueue.Enqueue(sentence); // load into queue
        }
    }

    private void ShowDialogue()
    {
        if (NPCSelected == null) return;
        if (hasDialogueStarted) return;
        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();
        // set icons and sentence
        npcIcon.sprite = NPCSelected.DialogueToShow.Icon;
        npcNameTMP.text = NPCSelected.DialogueToShow.Name; 
        npcDialogueTMP.text = NPCSelected.DialogueToShow.Greeting;
        hasDialogueStarted = true;
    }

    private void ContinueDialogue()
    {
        if(NPCSelected == null)
        {
            dialogueQueue.Clear();
            return;
        }

        // in case there's no more dialogue
        if(dialogueQueue.Count <= 0)
        {
            CloseDialoguePanel();
            hasDialogueStarted = false;

            // check if NPC has extra interaction
            if(NPCSelected.DialogueToShow.HasInteraction)
            {
                OnExtraInteractionEvent?.Invoke(NPCSelected.DialogueToShow.InteractionType);
            }

            return;
        }

        npcDialogueTMP.text = dialogueQueue.Dequeue(); // get next sentence
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
