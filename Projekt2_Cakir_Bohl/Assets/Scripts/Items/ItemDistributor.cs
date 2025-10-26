using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDistributor : MonoBehaviour
{
    [SerializeField] private Item[] _itemsDay;
    [SerializeField] private Item[] _itemsNight;
    [SerializeField] private Transform[] _positions;
    private bool _wasDay;
    private Item[] _tempItems;

    private void Start()
    {
        SelectItems(ProgressionManager.Instance.IsDay);
    }
    
    private void SelectItems(bool isDay)
    {
        if (_wasDay == isDay)
        {
            return;
        }
        
        Item[] _tempItems = ProgressionManager.Instance.IsDay ? _itemsDay : _itemsNight;
    }


    private void SetItemPositions()
    {
        List<Transform> tempPositions = _positions.ToList();

        foreach (Item item in _tempItems)
        {
            Item instance = item;
            Instantiate(instance);
            Transform instancePosition = tempPositions[UnityEngine.Random.Range(0, tempPositions.Count)];
            instance.transform.position = instancePosition.position;

            tempPositions.Remove(instancePosition);
        }
    }
    
    // Object/Struct/Listen anlegen, die Item-Konfiguration speichern


}
