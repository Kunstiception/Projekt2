using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDistributor : MonoBehaviour
{
    [SerializeField] private Item[] _itemsDay;
    [SerializeField] private Item[] _itemsNight;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    private bool _wasDay;

    private void Start()
    {
        //SelectItems(ProgressionManager.Instance.IsDay);

        SetItemPositions();
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
        List<SpawnPoint> tempSpawnPoints = _spawnPoints.ToList();
        List<Item> spawnedItems = new List<Item>();

        foreach (SpawnPoint spawnPoint in tempSpawnPoints)
        {
            Item item = spawnPoint.SetItem();
            spawnedItems.Add(item);

            Debug.Log(item.name);
        }
    }
}
