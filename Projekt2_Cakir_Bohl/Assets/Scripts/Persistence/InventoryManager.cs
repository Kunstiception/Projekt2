using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> Inventory = new List<string>();
    public List<int> Amounts = new List<int>();

    [SerializeField] private GameObject[] _allItems;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Add Item to Inventory
    public void AddToInventory(Item item, int amount)
    {
        string itemName = item.Stats.Name;

        if (Inventory.Contains(itemName))
        {
            int index = Inventory.IndexOf(itemName);

            Amounts[index] += amount;

            return;
        }
        else
        {
            Inventory.Add(itemName);
            Amounts.Add(amount);
        }

        for (int i = 0; i < Inventory.Count; i++)
        {
            Debug.Log($"{Inventory[i]}, {Amounts[i]}");
        }
    }

    public void RemoveFromInventory(Item item)
    {
        string itemName = item.Stats.Name;

        if (!Inventory.Contains(itemName))
        {
            Debug.LogError("Item not found in Inventory");
            return;
        }

        int index = Inventory.IndexOf(itemName);

        Inventory.RemoveAt(index);
        Amounts.RemoveAt(index);


        for (int i = 0; i < Inventory.Count; i++)
        {
            Debug.Log($"{Inventory[i]}, {Amounts[i]}");
        }
    }

    public void ClearInventory()
    {
        Inventory.Clear();
        Amounts.Clear();
    }
}
