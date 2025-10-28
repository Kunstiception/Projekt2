using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    public static SaveFileManager Instance;

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

    [System.Serializable]
    class SaveData
    {

    }

    // Create a savefile
    public void SaveAll()
    {
        SaveData data = new SaveData();



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



            Debug.LogError(json);
        }
    }
    
    // Clears the savefile
    // Just for editor use, makes it easier to debug things
    public void ClearSaveData()
    {
        SaveData data = new SaveData();



        SaveAll();
    }
}
