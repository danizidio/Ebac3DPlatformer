using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SaveSetup _saveGame;

    private void Awake()
    {
        SaveSetup _saveGame = new SaveSetup();
    }

    void Save()
    {
        string json = JsonUtility.ToJson(_saveGame);

        SaveFile(json);
    }

    void SaveFile(string json)
    {
        string path = Application.persistentDataPath + "/progress.txt";

        File.WriteAllText(path, json);
    }

}

[Serializable]
public class SaveSetup
{
    [SerializeField] int _lastStage;
    public int lastStage { get { return _lastStage; } set { _lastStage = value; } }

    [SerializeField] int _coinsTaken;
    public int coinsTaken { get { return _coinsTaken; } set { _coinsTaken = value; } }

    [SerializeField] int _medPaks;
    public int medPacks { get { return medPacks; } set { medPacks = value; } }

    [SerializeField] float _score;
    public float score { get { return _score; } set { _score = value; } }
}

