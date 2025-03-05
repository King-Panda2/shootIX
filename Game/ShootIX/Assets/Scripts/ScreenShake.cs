using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private AudioClip sfxFail;

    public void StartShake()
    {
        GameManager.Instance.PlaySound(sfxFail);
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float shake_time = 0f; // Time Counter

        while (shake_time < duration)
        {
            shake_time += Time.unscaledDeltaTime; // Add shake time 
            float strength = curve.Evaluate(shake_time / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength; // Random shake
            yield return null;
        }
        transform.position = startPosition; // Reposition 
    }
}

