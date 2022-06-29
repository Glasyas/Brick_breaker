using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Static = if multiple instances of MainManager, If any of those MainManagers changed the value in it, it would also be changed for the others.
    public static MainManager Instance;
    public string Name; // Get in menu

    public string BestName = "Name"; // Get from last session
    public int BestScore = 0; // Get from last session


    private void Awake()
    {
        // Singleton = ensure that only a single instance of the MainManager can ever exist
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // Serializable needed to use JSON on the data
    [System.Serializable]
    class SavedData
    {
        public string Username;
        public int Score;
    }

    public void SaveData(int points)
    {
        SavedData data = new SavedData();
        data.Username = Name;
        data.Score = points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);

            BestName = data.Username;
            BestScore = data.Score;

        }
    }
    public void ResetData()
    {
        SavedData data = new SavedData();
        data.Username = "Name";
        data.Score = 0;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}
