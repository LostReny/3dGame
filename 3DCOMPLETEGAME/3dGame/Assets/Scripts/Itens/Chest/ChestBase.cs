using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ChestBase : MonoBehaviour
{
    public Animator animator;

    [Header("Key")]
    public KeyCode _keycode = KeyCode.Z;

    [Header("Trigger")]
    public string triggerOpen = "Open";
    public Collider _chestCollider;

    [Header("Camera")]
    public GameObject chestCamera;

    [Header("Notification")]
    public GameObject _notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Header("Text")]
    public TMP_Text ChestText;
    public bool usedItemOnce = false;

    [Space]
    public ChestItemBase chestItemBase;

    
    private float startScale;

    private bool _chestOpened = false;
    private bool _playerNear = false;


    public void Start()
    {
        _chestCollider.enabled = true;
        _notification.SetActive(false);
        startScale = _notification.transform.localScale.x;
        usedItemOnce = false;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;
        animator.SetTrigger(triggerOpen);
        _chestCollider.enabled = false;
        _chestOpened = true;
        TurnCameraOn();
        Invoke(nameof(ShowItemInChest), .5f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShowNotification(0.5f));
            ChestText.text = "Aperte Z para interagir";
            usedItemOnce = true;
            _playerNear = true;
            InputChest();
            StartCoroutine(HideChestMessage(3f));
        }
    }

    private IEnumerator HideChestMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChestText.text = "";
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _playerNear = false;
        }
    }

    private IEnumerator ShowNotification(float duration)
    {
        yield return new WaitForSeconds(duration);
        _notification.SetActive(true);
        _notification.transform.localScale = Vector3.zero;
        _notification.transform.DOScale(startScale,tweenDuration);
        StartCoroutine(HideNotification(3f));
    }

    private IEnumerator HideNotification(float delay)
    {
        yield return new WaitForSeconds(delay);
        _notification.SetActive(false);
    }

    private void Update()
    {
        InputChest();
    }

    private void InputChest()
    {
        if (Input.GetKeyDown(_keycode) && _playerNear && !_chestOpened)
        {
            OpenChest();
        }
    }

    private void ShowItemInChest()
    {
        chestItemBase.ShowItem();
        Invoke(nameof(CollecItemInChest), 0.8f);
    }

    private void CollecItemInChest()
    {
        chestItemBase.Collect();
        TurnCameraOff();
    }

     private void TurnCameraOn()
    {
        chestCamera.SetActive(true);
    }

    public void TurnCameraOff()
    {
        chestCamera.SetActive(false);
    }

}
