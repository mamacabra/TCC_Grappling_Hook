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

    //[SerializeField] private GameObject cameraTargetOfficial;
    //[SerializeField] private GameObject cameraTargetDeathFeedback;
   // [SerializeField] private CinemachineTargetGroup cinemachineTargetGroupDeathFeedback;
    //[HideInInspector] public List<Transform> players = new List<Transform>(); // Array dos Transforms dos personagens

    [SerializeField] private Slider slider;
    private bool onEndFeedback;


    public event Action CallWinnerDance;
    public bool OnEndFeedback
    {
        get => onEndFeedback;
        set { onEndFeedback = value; }
    }

    private void Start()
    {
        slider.maxValue = 1;
        slider.value = 1;
        OnEndFeedback = true;
    }

    public void DeathFeedBack()
    {
       /* if (players.Count > 0)
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
        cameraTargetDeathFeedback.SetActive(true);*/
        
       

        OnEndFeedback = false;
        slider.DOValue(0.4f, 0.05f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            slider.DOValue(1, 0.25f).SetEase(Ease.InOutBounce).SetDelay(0.5f).OnComplete(() =>
            {
                StartCoroutine(WaitToBoolTrue());
                IEnumerator WaitToBoolTrue()
                {
                    yield return new WaitForSeconds(1.5f);
                    Time.timeScale = 1;
                    
                    CallWinnerDance?.Invoke();
                    
                    //muda a variavel de baixo para deixar a dancinha por mais tempo
                    yield return new WaitForSeconds(1.5f);
                    OnEndFeedback = true;
                }
               
            }).SetUpdate(true);
        }).SetUpdate(true);
    }

    private void Update()
    {
        if (!OnEndFeedback) Time.timeScale = slider.value;
    }
}