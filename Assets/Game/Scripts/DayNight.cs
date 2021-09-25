using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;

    public Light directionalLight;

    public bool toggleDay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            toggleDay = !toggleDay;
            RenderSettings.skybox = toggleDay ? dayMat : nightMat;
            directionalLight.color = toggleDay ? Color.white : Color.black;
        }
    }
}
