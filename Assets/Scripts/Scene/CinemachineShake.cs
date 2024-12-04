using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private float shakeTimer;
    [SerializeField] private float shakeTimerTotal;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineAmplitudeGain;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineAmplitudeGain = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineAmplitudeGain.m_AmplitudeGain = 0f;
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (cinemachineVirtualCamera is null) return;

        shakeTimer = time;
        shakeTimerTotal = time;
        shakeIntensity = intensity;
    }

    private void Update()
    {
        if (shakeTimer <= 0) return;

        shakeTimer -= Time.deltaTime;
        cinemachineAmplitudeGain.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
    }
}
