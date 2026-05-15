using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlackoutLightTimer : MonoBehaviour
{
    [SerializeField] private float blackoutDuration = 2.5f;
    [SerializeField] private float blackoutInterval = 5f;
    [SerializeField] private Light2D blackoutLight;
    [Header("Color Settings")]
    [SerializeField] private float blackoutColorIntensity = 0f;
    [SerializeField] private float lightsOnColorIntensity = 0.3f;

    private float currentTimer;

    void Start()
    {
        StartCoroutine(BlackoutRoutine());
    }

    IEnumerator BlackoutRoutine()
    {
        // Wait for the interval before starting the blackout
        yield return new WaitForSeconds(blackoutInterval);

        // Start the blackout
        currentTimer = 0f;
        while (currentTimer < blackoutDuration)
        {
            currentTimer += Time.deltaTime;
            float t = currentTimer / blackoutDuration;

            // Smoothly transition the light intensity
            blackoutLight.intensity = Mathf.Lerp(lightsOnColorIntensity, blackoutColorIntensity, t);

            yield return null; // Wait for the next frame
        }

        // Ensure the light is fully off at the end of the blackout
        blackoutLight.intensity = blackoutColorIntensity;
        StartCoroutine(LightsOnRoutine()); // Start the routine to turn the lights back on
    }

    IEnumerator LightsOnRoutine()
    {
        // Wait for the interval before turning the lights back on
        yield return new WaitForSeconds(blackoutInterval + blackoutDuration);

        // Start turning the lights back on
        currentTimer = 0f;
        while (currentTimer < blackoutDuration)
        {
            currentTimer += Time.deltaTime;
            float t = currentTimer / blackoutDuration;

            // Smoothly transition the light intensity back to normal
            blackoutLight.intensity = Mathf.Lerp(blackoutColorIntensity, lightsOnColorIntensity, t);

            yield return null; // Wait for the next frame
        }

        // Ensure the light is fully on at the end of the routine
        blackoutLight.intensity = lightsOnColorIntensity;
        StartCoroutine(BlackoutRoutine()); // Restart the blackout routine
    }
}
