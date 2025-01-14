using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Itens;
using LostWordls.StateMachine;

public class EndGame : MonoBehaviour
{
    public string tagToCheck = "Player";

    public List<GameObject> endGameObjects;

    public int _currentLevel = 1;

    [Header("Text")]
    public TMP_Text EndText;

    private bool _endGame = false;


    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheck && !_endGame)
        {
            EndText.text = "End Game";
            ShowEndGame();
        }
        StartCoroutine(HideEndGameMessage(5f));
    }

    IEnumerator HideEndGameMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndText.text = "";
    }

    private void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }
            
            SaveManager.Instance.SaveLastLevel(_currentLevel);
            //SaveManager.Instance.CreateNewSave();
            //ResetCheckpoints();
            //ResetAllItens();

    }

}
