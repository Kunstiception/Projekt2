using System;
using UnityEngine;

public class Item : Interaction, IInteractable
{
    public Action OnInteractionFinished { get; set; }

    public void StartInteraction()
    {
        Debug.Log("Interacting with item!");
    }
}
