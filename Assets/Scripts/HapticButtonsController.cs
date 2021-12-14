using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticButtonsController : MonoBehaviour
{
    public GameObject hapticDevice;

    public bool darkContinuous, lightContinuous, darkDouble, lightDouble, darkSimple, lightSimple;

    private bool darkLastPressed, lightLastPressed;

    private float darkTimer, lightTimer;
    private float darkReleasedTimer, lightReleasedTimer;
    private bool darkStatus, lightStatus;

    // Start is called before the first frame update
    void Start()
    {
        darkContinuous = lightContinuous = darkDouble = lightDouble = false;

        darkReleasedTimer = lightReleasedTimer = 0.0f;
        darkStatus = lightStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        checker();


        /* Dark button */

        if(darkStatus)
        {
            if(!darkLastPressed)
            {
                darkTimer = 0.0f;
            }
            else
            {
                darkTimer += Time.deltaTime;

                if(darkTimer >= 0.5f)
                {
                    darkContinuous = true;
                }
            }
        }
        else
        {
            if(darkLastPressed)
            {
                if(darkTimer < 0.5f)
                {
                    darkSimple = true;
                }
            }

            darkTimer = 0.0f;
            darkContinuous = false;
        }

        darkLastPressed = darkStatus;


        /* Light button */

        if (lightStatus)
        {
            if (!lightLastPressed)
            {
                lightTimer = 0.0f;
            }
            else
            {
                lightTimer += Time.deltaTime;

                if (lightTimer >= 0.5f)
                {
                    lightContinuous = true;
                }
            }
        }
        else
        {
            if (lightLastPressed)
            {
                if (lightTimer < 0.5f)
                {
                    lightSimple = true;
                }
            }

            lightTimer = 0.0f;
            lightContinuous = false;
        }

        lightLastPressed = lightStatus;
    }

    private void checker()
    {
        /* Checks for dark button */

        if (!darkStatus)
        {
            if (hapticDevice.GetComponent<HapticPlugin>().Buttons[0] == 1)
            {
                darkStatus = true;
            }
        }
        else
        {
            if (hapticDevice.GetComponent<HapticPlugin>().Buttons[0] == 0)
            {
                darkReleasedTimer += Time.deltaTime;
            }
            else
            {
                darkReleasedTimer = 0.0f;
            }

            if (darkReleasedTimer > 0.05f)
            {
                darkStatus = false;
                darkReleasedTimer = 0.0f;
            }
        }


        /* Checks for light button */

        if (!lightStatus)
        {
            if (hapticDevice.GetComponent<HapticPlugin>().Buttons[1] == 1)
            {
                lightStatus = true;
            }
        }
        else
        {
            if (hapticDevice.GetComponent<HapticPlugin>().Buttons[1] == 0)
            {
                lightReleasedTimer += Time.deltaTime;
            }
            else
            {
                lightReleasedTimer = 0.0f;
            }

            if (lightReleasedTimer > 0.05f)
            {
                lightStatus = false;
                lightReleasedTimer = 0.0f;
            }
        }
    }
}
