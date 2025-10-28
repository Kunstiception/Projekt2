using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    public static SaveFileManager Instance;

    public List<string> Inventory;
    public List<int> Amounts;

    //Fortschritt
    //Tag/Tageszeit
    //Inventar
    //Itemverteilung

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

    private void Start()
    {
        LoadAll();
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }

    [System.Serializable]
    class SaveData
    {
        public List<string> Inventory;
        public List<int> Amounts;
    }

    // Create a savefile
    public void SaveAll()
    {
        SaveData data = new SaveData();

        Inventory.Clear();
        Amounts.Clear();

        for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            Inventory.Add(InventoryManager.Instance.Inventory[i]);
            Amounts.Add(InventoryManager.Instance.Amounts[i]);
        }

        data.Inventory = Inventory;
        data.Amounts = Amounts;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerstats.json", json);

        Debug.LogError(json);
    }

    // Load the savefile (if there is one)
    public void LoadAll()
    {
        string path = Application.persistentDataPath + "/playerstats.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if(InventoryManager.Instance != null)
            {
                for(int i = 0; i < data.Inventory.Count; i++)
                {
                    InventoryManager.Instance.Inventory.Add(data.Inventory[i]);
                    InventoryManager.Instance.Amounts.Add(data.Amounts[i]);
                }
            }

            Debug.LogError(json);
        }
    }
    
    // Clears the savefile
    // Just for editor use, makes it easier to debug things
    public void ClearSaveData()
    {
        SaveData data = new SaveData();

        InventoryManager.Instance.ClearInventory();

        Inventory.Clear();
        Amounts.Clear();

        data.Inventory = Inventory;
        data.Amounts = Amounts;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerstats.json", json);

        Debug.LogError(json);
    }
}
