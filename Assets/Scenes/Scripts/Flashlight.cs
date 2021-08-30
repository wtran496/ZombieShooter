using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;

    Light myLight;
    // Start is called before the first frame update
    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    private void DecreaseLightAngle() {
        if (myLight.spotAngle <= minimumAngle)
            return;
        else
            myLight.spotAngle -= angleDecay * Time.deltaTime;
    }

    private void DecreaseLightIntensity() {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }

    public void RestoreLightAngle(float restoreAngle) {
        myLight.spotAngle = restoreAngle;
    }

    public void AddLightIntensity(float intensityAmount) {
        myLight.intensity += intensityAmount; 
    }
}
