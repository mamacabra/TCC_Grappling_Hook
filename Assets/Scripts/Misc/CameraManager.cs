using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    #region Singleton

    private static CameraManager instance;
    public static CameraManager Instance => instance ? instance : FindObjectOfType<CameraManager>();

    #endregion

    [SerializeField] private GameObject cameraTargetOfficial;
    [SerializeField] private GameObject cameraTargetDeathFeedback;
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroupDeathFeedback;
    [HideInInspector] public List<Transform> players = new List<Transform>(); // Array dos Transforms dos personagens

    [SerializeField] private Slider slider;
    private bool onEndFeedback;

    public bool OnEndFeedback
    {
        get => onEndFeedback;
        set { onEndFeedback = value; }
    }

    private void Start()
    {
        OnEndFeedback = true;
    }

    public void DeathFeedBack(Transform p1, Transform p2)
    {
        if (players.Count > 0)
        {
            cinemachineTargetGroupDeathFeedback.RemoveMember(players[0]);
            cinemachineTargetGroupDeathFeedback.RemoveMember(players[1]);
            players.Clear();
        }
        cameraTargetOfficial.SetActive(false);
        
        players.Add(p1);
        players.Add(p2);
        cinemachineTargetGroupDeathFeedback.AddMember(p1,1,2);
        cinemachineTargetGroupDeathFeedback.AddMember(p2,1,2);
        cameraTargetDeathFeedback.SetActive(true);
        
        slider.maxValue = 1;
        slider.value = 1;

        OnEndFeedback = false;
        slider.DOValue(0.1f, 0.1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            slider.DOValue(1, 1).SetEase(Ease.InOutBounce).SetDelay(1.5f).OnComplete(() =>
            {
                StartCoroutine(WaitToBoolTrue());
                IEnumerator WaitToBoolTrue()
                {
                    yield return new WaitForSeconds(2);
                    OnEndFeedback = true;
                    Time.timeScale = 1;
                }
               
            }).SetUpdate(true);
        }).SetUpdate(true);
    }

    private void Update()
    {
        if (!OnEndFeedback) Time.timeScale = slider.value;
    }
}