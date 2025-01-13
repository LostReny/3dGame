using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LostWordls.Singleton;
public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;

    public int lastLevel;

    public Action<SaveSetup> FileLoaded = delegate { };

    public SaveSetup Setup
    {
        get {return _saveSetup;}
    }

    private string _path = Application.streamingAssetsPath + "/save.txt";


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        //colocando em Json
        string setupToJson = JsonUtility.ToJson(_saveSetup);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Itens.ItemCollectManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Itens.ItemCollectManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        SaveCurrentLife();
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveCheckpoint(int checkpoint)
    {
        _saveSetup.checkPoint = CheckPointManager.Instance.SaveCheckPoint(checkpoint);
        SaveItems();
        Save();
    }

    public void SaveCurrentLife()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            HealthBase playerHealthBase = player.GetComponent<HealthBase>();
            if (playerHealthBase != null)
            {
                _saveSetup.currentLife = playerHealthBase.currentLife;
                Save(); // Salva a vida no arquivo
            }
        }
    }


        public void LoadCurrentLife()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            HealthBase playerHealthBase = player.GetComponent<HealthBase>();
            if (playerHealthBase != null && _saveSetup.currentLife > 0)
            {
                playerHealthBase.currentLife = _saveSetup.currentLife;
                playerHealthBase.UpdateUi(); // Atualiza a UI do jogador após carregar a vida
            }
        }
    }


    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if(File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }  

         FileLoaded?.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public float coins;
    public float lifePack;
    public int checkPoint;

    public float currentLife;
}