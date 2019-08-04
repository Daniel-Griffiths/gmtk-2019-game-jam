using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    public static CameraShake instance;

    void Awake()
    {
        originalPos = transform.localPosition;

        instance = this;
    }

    public static void Shake (float duration, float amount) {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.cShake(duration, amount));
    }

    public IEnumerator cShake (float duration, float amount) {
        float endTime = Time.time + duration;

        while (Time.time < endTime) {
            transform.localPosition = originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}