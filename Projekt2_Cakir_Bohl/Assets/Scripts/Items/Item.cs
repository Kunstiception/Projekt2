using System;
using System.Collections;
using UnityEngine;

public class Item : Interaction, IInteractable
{
    public Action OnInteractionFinished { get; set; }

    public void StartInteraction()
    {
        Debug.Log("Interacting with item!");

        ResetHasFinished();

        StartCoroutine(PickUpItem());
    }

    protected IEnumerator PickUpItem()
    {
        Debug.Log($"Juno has picked up {this.name}!");

        yield return new WaitForSeconds(2);

        _hasFinished = true;
    }
}
