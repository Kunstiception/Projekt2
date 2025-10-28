using System;
using System.Collections;
using UnityEngine;

public class Item : Interaction, IInteractable
{
    public Action OnInteractionFinished { get; set; }
    public ItemStats Stats;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Stats.ItemVisual;
    }

    public void StartInteraction()
    {
        Debug.Log($"Interacting with {Stats.Name}!");

        ResetHasFinished();

        StartCoroutine(PickUpItem());
    }

    protected IEnumerator PickUpItem()
    {
        Debug.Log($"Juno has picked up {Stats.Name}!");

        InventoryManager.Instance.AddToInventory(this, 1);

        yield return new WaitForSeconds(2);

        _hasFinished = true;
    }
}
