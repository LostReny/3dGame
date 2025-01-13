using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

   void Start()
   {
        SaveManager.Instance.FileLoaded += OnLoad;
   }

   public void OnLoad(SaveSetup setup)
   {
        uiTextName.text = "Load" + (setup.lastLevel + 1);
        //LoadNextLevel(setup.lastLevel + 1);
   }

   /* private void LoadNextLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }*/

   private void OnDestroy()
   {
        SaveManager.Instance.FileLoaded -= OnLoad;
   }
}
