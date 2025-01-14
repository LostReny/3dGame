using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

     public void ResetLevel(int level)
    {
        PlayerPrefs.DeleteAll(); 
        SaveManager.Instance.CreateNewSave();
        SceneManager.LoadScene(level);
    }
}
