using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private float shakeTimer;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (virtualCamera is null) return;
        var cinemachineAmplitudeGain = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (cinemachineAmplitudeGain) cinemachineAmplitudeGain.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
            shakeTimer -= Time.deltaTime;
        else
        {
            if (virtualCamera is null) return;
            var cinemachineAmplitudeGain = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (cinemachineAmplitudeGain) cinemachineAmplitudeGain.m_AmplitudeGain = 0;
        }
    }
}
