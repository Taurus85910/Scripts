using UnityEngine;

public class SaveService : MonoBehaviour
{
    public static SaveService Instance { get; private set; }
    public SaveData SaveData { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }
    public void Save() => SaveManager.SaveGame(SaveData);
    public void Load() => SaveData = SaveManager.LoadGame();
   
}