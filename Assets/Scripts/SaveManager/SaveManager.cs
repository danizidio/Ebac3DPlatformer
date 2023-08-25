using CommonMethodsLibrary;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] SaveSetup _saveGame;
    public SaveSetup saveGame { get {  return _saveGame; } }

    string _path = Application.streamingAssetsPath + "/progress.txt";

    protected override void Awake()
    {
        base.Awake();
        SaveSetup _saveGame = new SaveSetup();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Load();
    }

   public void CreateNewSave()
    {
        _saveGame.lastStage = 0;
        _saveGame.score = 0;
        _saveGame.medPacks = 0;
        _saveGame.coinsTaken = 0;
        _saveGame.checkpoints = Checkpoints.POINT_A;

        Save();
    }

    public void SaveInventory(CollectibleTypes type, int quantity)
    {
        switch (type)
        {
            case CollectibleTypes.COLLECTIBLE_COIN:
                {
                    _saveGame.coinsTaken = quantity;
                    break;
                }
            case CollectibleTypes.COLLECTIBLE_HEALTHPACK:
                {
                    _saveGame.medPacks = quantity;
                    break;
                }
        }

        Save();
    }

    void Save()
    {
        string json = JsonUtility.ToJson(_saveGame);

        SaveFile(json);
    }

    void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    public void StageCleared()
    {
        _saveGame.lastStage++;

        Save();
    }

    public void SetCheckPoint(Checkpoints checkpoint)
    {
        _saveGame.checkpoints = checkpoint;

        Save();
    }

    void Load()
    {
        string fileLoad = "";

        if(File.Exists(_path))
        {
            fileLoad = File.ReadAllText(_path);
            _saveGame = JsonUtility.FromJson<SaveSetup>(fileLoad);
        }
        else
        {
            CreateNewSave();
            Save();
        }
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
    public int medPacks { get { return _medPaks; } set { _medPaks = value; } }

    [SerializeField] float _score;
    public float score { get { return _score; } set { _score = value; } }

    [SerializeField] Checkpoints _checkpoints;
    public Checkpoints checkpoints { get { return _checkpoints; } set { _checkpoints = value; } }
    
}

