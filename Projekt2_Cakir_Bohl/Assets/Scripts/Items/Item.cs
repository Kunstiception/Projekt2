using System;
using System.Collections;
using UnityEngine;

public class Item : Interaction, IInteractable
{
    public Action OnInteractionFinished { get; set; }
    [SerializeField] private ItemStats _stats;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _stats.ItemVisual;
    }

    public void StartInteraction()
    {
        Debug.Log($"Interacting with {_stats.Name}!");

        ResetHasFinished();

        StartCoroutine(PickUpItem());
    }

    protected IEnumerator PickUpItem()
    {
        Debug.Log($"Juno has picked up {_stats.Name}!");

        yield return new WaitForSeconds(2);

        _hasFinished = true;
    }
}
