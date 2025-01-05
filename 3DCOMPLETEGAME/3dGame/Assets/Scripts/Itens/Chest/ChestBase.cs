using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator animator;

    [Header("Trigger")]
    public string triggerOpen = "Open";
    public Collider _chestCollider;

    [Header("Notification")]
    public GameObject _notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;
    private float startScale;

    public void Start()
    {
        _chestCollider.enabled = true;
        _notification.SetActive(false);
        startScale = _notification.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);
        _chestCollider.enabled = false;
        StartCoroutine(ShowNotification(0.5f));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenChest();
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
}
