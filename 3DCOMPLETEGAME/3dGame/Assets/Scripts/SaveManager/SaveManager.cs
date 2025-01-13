using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LostWordls.Singleton;
public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
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
    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }
    #endregion
    private void SaveFile(string json)
    {
        
        //string path = Application.dataPath + "/save.txt";

        string path = Application.streamingAssetsPath + "/save.txt";
        
        Debug.Log(path);
        File.WriteAllText(path, json);
    }
}
[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;

    public float coins;
    public float lifePack;
}