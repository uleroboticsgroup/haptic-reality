using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RecolocationModeController : MonoBehaviour
{
    public GameObject hapticDevice;
    public GameObject workspace;
    public GameObject avatar;

    //public Text lookAtWorkspaceText;

    private GameObject textCanvas;

    private bool showingLookAtWorkspaceText;

    private bool movingWorkspace;
    private Vector3 gazeWorkspaceDiff;
    private Quaternion rotationDiff;

    private float depth;

    // METRICS
    private float recolocationTimer, beforeStartRecolocationTimer;
    private int lastState, currentState;
    private int increaseDepthCounter, decreaseDepthCounter;

    private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<SceneController>();

        //textCanvas = lookAtWorkspaceText.transform.parent.gameObject;
        showingLookAtWorkspaceText = false;
        movingWorkspace = false;

        depth = 0.0f;

        recolocationTimer = 0.0f;
        beforeStartRecolocationTimer = 0.0f;

        lastState = 0;
        currentState = 0;
        increaseDepthCounter = 0;
        decreaseDepthCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Change if Gaze
        if (GetComponentInParent<ModeController>().mode == "recolocacion")
        //if (GetComponentInParent<ModeControllerGaze>().mode == "recolocacion")
        {

            /*
            if (GameObject.Find("TasksCanvas").GetComponent<GameController>().gameStarted)
                recolocationTimer += Time.deltaTime;
            else
                beforeStartRecolocationTimer += Time.deltaTime;
            */

            if(depth == 0.0f)
            {
                depth = Vector3.Distance(Camera.main.transform.position, hapticDevice.transform.position);
            }

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);

            /* De esto se va a encargar el ModeController
            if (Vector3.Angle(camera.transform.forward, workspace.transform.position - camera.transform.position) >= 5 && !showingLookAtWorkspaceText)
            {
                showingLookAtWorkspaceText = true;
                lookAtWorkspaceText.text = "Mira hacia el área de trabajo para desplazarla";

                /*
                textCanvas.transform.position = ray.origin + ray.direction.normalized * 2.0f;
                textCanvas.transform.position -= new Vector3(0.0f, 0.5f, 0.0f);

                textCanvas.transform.rotation = Quaternion.LookRotation(textCanvas.transform.position - camera.transform.position);
                */

            /*
                StartCoroutine(hideTextAfterSeconds(3));
            }
        
            else if (Vector3.Angle(camera.transform.forward, workspace.transform.position - camera.transform.position) < 5)
            {
                lookAtWorkspaceText.text = "";
                showingLookAtWorkspaceText = false;

                allowRecolocationMode = true;
            }
            */

            // Aqui solo me voy a encargar del modo recolocacion, no de activarlo o ver si puedo o no hacerlo

            if (GetComponentInParent<HapticButtonsController>().darkContinuous)
            {
                currentState = 1;
                depth += 0.001f;
            }
            else if (GetComponentInParent<HapticButtonsController>().lightContinuous)
            {
                currentState = -1;
                depth -= 0.001f;
            }
            else
            {
                currentState = 0;
                lastState = 0;
            }

            Vector3 gazePoint = ray.origin + ray.direction.normalized * depth;

            if (!movingWorkspace)
            {
                gazeWorkspaceDiff = gazePoint - hapticDevice.transform.position;
            }

            hapticDevice.transform.localPosition = gazePoint;
            //workspace.transform.localPosition = gazePoint - gazeWorkspaceDiff;

            Quaternion allAxisQuaternion = Quaternion.LookRotation(hapticDevice.transform.position - Camera.main.transform.position);

            Quaternion yAxisOnlyQuaternion = Quaternion.Euler(hapticDevice.transform.rotation.eulerAngles.x, allAxisQuaternion.eulerAngles.y, hapticDevice.transform.rotation.eulerAngles.z);

            hapticDevice.transform.rotation = yAxisOnlyQuaternion;
            //workspace.transform.rotation = Quaternion.LookRotation(workspace.transform.position - camera.transform.position);

            float verticalWorkspaceDisplacement = (hapticDevice.transform.localScale.x * 0.5f) / 0.01f;
            float depthWorkspaceDisplacement = (hapticDevice.transform.localScale.x * 0.44f) / 0.01f;

            //workspace.transform.localPosition += new Vector3(0.0f, verticalWorkspaceDisplacement, depthWorkspaceDisplacement);

            movingWorkspace = true;

            /* Pongo en rojo el workspace si no puedo salir del modo */

            /*

            Renderer[] renderers = workspace.GetComponentsInChildren<Renderer>();

            if (avatar.GetComponent<AvatarController>().safePlace)
            {
                foreach (Renderer r in renderers)
                {
                    r.material.color = Color.green;
                }
            }
            else
            {
                foreach (Renderer r in renderers)
                {
                    r.material.color = Color.red;
                }
            }
            */

            if (currentState != lastState && currentState != 0)
            {
                if (currentState == 1)
                {
                    scene.register("Se aleja el workspace");
                }

                if (currentState == -1)
                {
                    scene.register("Se acerca el workspace");
                }

                lastState = currentState;
            }
        }
        else
        {
            movingWorkspace = false;
        }
    }

    IEnumerator hideTextAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        //lookAtWorkspaceText.text = "";
        showingLookAtWorkspaceText = false;
    }
}
