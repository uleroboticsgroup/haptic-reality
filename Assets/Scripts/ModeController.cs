using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class ModeController : MonoBehaviour
{
    public GameObject hapticDevice;
    public GameObject workspace;
    public GameObject avatar;

    //public GameObject textCanvas;
    //public Text modeText;

    public string mode;

    private bool modeSwitcher;

    private Color originalWorkspaceColor;

    private int textShowedCounter;

    private int switchToInteractionCounter, switchToRecolocationCounter, triesToSwitchToInteractionCounter, triesToSwitchToRecolocationCounter;
    private string statsPath;

    private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<SceneController>();

        Ray ray = new Ray(Camera.main.transform.position, hapticDevice.transform.position - Camera.main.transform.position);
        Vector3 startingHapticPosition = ray.origin + ray.direction.normalized * 2.0f;

        //hapticDevice.transform.localPosition = startingHapticPosition;

        Quaternion allAxisQuaternion = Quaternion.LookRotation(hapticDevice.transform.position - Camera.main.transform.position);

        Quaternion yAxisOnlyQuaternion = Quaternion.Euler(hapticDevice.transform.rotation.eulerAngles.x, allAxisQuaternion.eulerAngles.y, hapticDevice.transform.rotation.eulerAngles.z);

        hapticDevice.transform.rotation = yAxisOnlyQuaternion;

        mode = "interaccion";
        originalWorkspaceColor = workspace.GetComponentInChildren<Renderer>().material.color;
        modeSwitcher = false; // Interaccion

        textShowedCounter = 0;

        switchToInteractionCounter = 0;
        switchToRecolocationCounter = 0;
        triesToSwitchToInteractionCounter = 0;
        triesToSwitchToRecolocationCounter = 0;

        statsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\stats.csv";
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);

        //textCanvas.transform.position = ray.origin + ray.direction.normalized * 2.0f;
        //textCanvas.transform.position -= new Vector3(0.0f, 0.5f, 0.0f);

        //textCanvas.transform.rotation = Quaternion.LookRotation(textCanvas.transform.position - Camera.main.transform.position);

        if (GetComponentInParent<HapticButtonsController>().darkSimple)
        {
            modeSwitcher = !modeSwitcher;
            GetComponentInParent<HapticButtonsController>().darkSimple = false;
        }

        if (!modeSwitcher && this.mode != "interaccion")
        {
            if (canSwitchToInteractionMode())
            {
                scene.register("Recolocacion - interaccion");

                Renderer[] renderers = workspace.GetComponentsInChildren<Renderer>();

                foreach (Renderer r in renderers)
                {
                    if (r.gameObject.name != "ws_info")
                    {
                        r.material.color = originalWorkspaceColor;
                    }
                }

                switchToInteractionCounter++;
                //GameObject.Find("ChronoButton").GetComponent<ChronoController>().saveSwitchToInteraction();

                this.mode = "interaccion";
                hapticDevice.GetComponent<HapticPlugin>().shapesEnabled = true;
                //modeText.text = "Modo actual: interacción";
            }
            else
            {
                scene.register("Se ha podido cambiar al modo interaccion");
                modeSwitcher = !modeSwitcher;
            }
        }

        if (modeSwitcher && this.mode != "recolocacion")
        {
            scene.register("Interaccion - recolocacion");

            this.mode = "recolocacion";
            hapticDevice.GetComponent<HapticPlugin>().shapesEnabled = false;
            //modeText.text = "Modo actual: recolocación";

            Renderer[] renderers = workspace.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                if(r.gameObject.name != "ws_info")
                {
                    r.material.color = Color.green;
                }
            }
        }
    }

    private bool canSwitchToInteractionMode()
    {
        triesToSwitchToInteractionCounter++;
        //GameObject.Find("ChronoButton").GetComponent<ChronoController>().saveTrySwitchToInteraction();

        //return avatar.GetComponent<AvatarController>().safePlace;
        return true;
    }
}
