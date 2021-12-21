using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InteractionModeController : MonoBehaviour
{
    public GameObject hapticDevice;
    public GameObject avatar;
    //public GameObject workspace;

    private HapticButtonsController hapticButtonsController;

    private float scale;

    private float interactionTimer, beforeStartInteractionTimer;
    private int lastState, currentState;
    private int zoomInCounter, zoomOutCounter;

    private bool updateFlag;

    private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<SceneController>();

        scale = hapticDevice.transform.localScale.x;

        interactionTimer = 0.0f;
        beforeStartInteractionTimer = 0.0f;

        lastState = 0;
        currentState = 0;
        zoomOutCounter = 0;
        zoomInCounter = 0;

        updateFlag = true;

        hapticButtonsController = GetComponentInParent<HapticButtonsController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change if Gaze
        if (GetComponentInParent<ModeController>().mode == "recolocacion")
        //if (GetComponentInParent<ModeControllerGaze>().mode == "recolocacion")
        {
            return;
        }

        /*
        if (GameObject.Find("TasksCanvas").GetComponent<GameController>().gameStarted)
            interactionTimer += Time.deltaTime;
        else
            beforeStartInteractionTimer += Time.deltaTime;
        */

        if (hapticButtonsController.darkContinuous)
        {
            if (scale - 0.00001f < 0.0001f)
                return;

            currentState = 1;

            scale -= 0.00001f;

            Vector3 A = hapticDevice.transform.localPosition;

            Vector3 B = avatar.transform.localPosition;

            Vector3 C = A - B; // diff from object pivot to desired pivot/origin

            Vector3 newScale = new Vector3(scale, scale, scale);

            float RS = scale / hapticDevice.transform.localScale.x; // relataive scale factor

            // calc final position post-scale
            Vector3 FP = B + C * RS;

            // finally, actually perform the scale/translation
            hapticDevice.transform.localScale = newScale;
            hapticDevice.transform.localPosition = FP;

            Vector3 newWorkspaceScale = newScale / 0.01f;
            Vector3 newWorkspacePosition = FP;

            float verticalWorkspaceDisplacement = (scale * 0.5f) / 0.01f;
            float depthWorkspaceDisplacement = (scale * 0.44f) / 0.01f;

            newWorkspacePosition += new Vector3(0.0f, verticalWorkspaceDisplacement, depthWorkspaceDisplacement);

            //workspace.transform.localScale = newWorkspaceScale;
            //workspace.transform.localPosition = newWorkspacePosition;
        }
        else if (hapticButtonsController.lightContinuous)
        {
            if(updateFlag)
            {
                updateFlag = false;
            }
            else
            {
                updateFlag = true;
                return;
            }

            if (scale + 0.00001f > 0.1f)
                return;

            currentState = -1;

            scale += 0.00001f;

            /*
            float size = scale / 0.00075f;
            sizeText.text = "WS SIZE: " + size;
            sizeText.color = Color.white;
            */

            Vector3 A = hapticDevice.transform.localPosition;

            Vector3 B = avatar.transform.localPosition;

            Vector3 C = A - B; // diff from object pivot to desired pivot/origin

            Vector3 newScale = new Vector3(scale, scale, scale);

            float RS = scale / hapticDevice.transform.localScale.x; // relataive scale factor

            // calc final position post-scale
            Vector3 FP = B + C * RS;

            // finally, actually perform the scale/translation

            hapticDevice.transform.localScale = newScale;
            hapticDevice.transform.localPosition = FP;

            Vector3 newWorkspaceScale = newScale / 0.01f;
            Vector3 newWorkspacePosition = FP;

            float verticalWorkspaceDisplacement = (scale * 0.5f) / 0.01f;
            float depthWorkspaceDisplacement = (scale * 0.44f) / 0.01f;

            newWorkspacePosition += new Vector3(0.0f, verticalWorkspaceDisplacement, depthWorkspaceDisplacement);

            //workspace.transform.localScale = newWorkspaceScale;
            //workspace.transform.localPosition = newWorkspacePosition;
        }
        else
        {
            currentState = 0;
            lastState = 0;
        }

        if (currentState != lastState && currentState != 0)
        {
            if (currentState == 1)
            {
                scene.register("Workspace mas pequeno");
            }

            if (currentState == -1)
            {
                scene.register("Workspace mas grande");
            }

            lastState = currentState;
        }
    }
}
