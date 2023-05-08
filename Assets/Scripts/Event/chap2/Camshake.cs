using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camshake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    IEnumerator ShakeCamera()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = shakeAmplitude;
            noise.m_FrequencyGain = shakeFrequency;
        }

        yield return new WaitForSeconds(shakeDuration);

        if (noise != null)
        {
            noise.m_AmplitudeGain = 0.0f;
            noise.m_FrequencyGain = 0.0f;
        }
    }
}