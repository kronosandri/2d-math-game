using UnityEngine;

public class NPC : Interactable
{
    public NPCData data; 

    public override void Interact(PlayerInteract player)
    {
        bool alreadyMet = ProgressManager.Instance.HasMetNPC(data.npcName);
        
        if (!alreadyMet && data.givesFormula)
        {
            ProgressManager.Instance.UnlockFormula(data.formula.formulaTitle);
            NotificationManager.Instance?.PopNotification("Unlocked: " + data.formula.formulaTitle, null, NotificationManager.Instance.formulaColor);
            ProgressManager.Instance.MarkNPCMet(data.npcName);
        }

        DialogueManager.Instance?.StartDialogue(player, alreadyMet ? data.repeatingLines : data.firstMeetingLines, data.npcName);
    }

}