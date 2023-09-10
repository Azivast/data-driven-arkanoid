using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour {
    public AnimationCurve curve;
    public static bool shaking = false;
    
    private static float duration = 1f;
    private static float intensity = 1f;

    private void Update() {
        if (shaking) {
            shaking = false;
            StartCoroutine(DoShake(duration, intensity));
        }
    }
    IEnumerator DoShake(float duration, float intensity = 1) {
        Vector3 initialPos = transform.position;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float modifier = curve.Evaluate(elapsedTime / duration) * intensity;
            transform.position = initialPos + (Vector3)Random.insideUnitCircle * modifier;
            yield return null;
        }

        transform.position = initialPos;
    }

    public static void Shake(float duration, float intensity = 1) {
        shaking = true;
        CameraShake.duration = duration;
        CameraShake.intensity = intensity;
    }
}
