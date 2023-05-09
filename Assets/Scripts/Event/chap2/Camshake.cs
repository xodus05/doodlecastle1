using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camshake : MonoBehaviour
{
    [SerializeField] float shakeMagnitude = 0.1f;
    [SerializeField] float shakeDuration = 0.1f;

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}