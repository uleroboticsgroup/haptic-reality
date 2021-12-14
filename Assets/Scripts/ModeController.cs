using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ModeController : MonoBehaviour
{
    private Dictionary<DateTime, String> registry = new Dictionary<DateTime, String>();
    private String registryFileName;

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

    // Start is called before the first frame update
    void Start()
    {
        registryFileName = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".csv";
        registry.Add(DateTime.Now, "Comienzo de la simulacion");

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
                registry.Add(DateTime.Now, "Recolocacion - interaccion");

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
                registry.Add(DateTime.Now, "Se ha podido cambiar al modo interaccion");
                modeSwitcher = !modeSwitcher;
            }
        }

        if (modeSwitcher && this.mode != "recolocacion")
        {
            if (canSwitchToRecolocationMode())
            {
                registry.Add(DateTime.Now, "Interaccion - recolocacion");

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
            else
            {
                registry.Add(DateTime.Now, "No se ha podido cambiar al modo recolocacion");

                modeSwitcher = !modeSwitcher;

                GameObject.Find("CenterIndicator").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("CenterIndicatorText").GetComponent<Text>().color = Color.black;

                StartCoroutine(centerIndicatorFadeOut());
                StartCoroutine(centerIndicatorTextFadeOut());
            }

        }
    }

    public void register(String e) {
        registry.Add(DateTime.Now, e);
    }

    private bool canSwitchToRecolocationMode()
    {
        triesToSwitchToRecolocationCounter++;

        /*
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Mathf.Infinity);

        for(int i=0; i<hits.Length; i++)
        {
            if(hits[i].transform.gameObject.name == "CenterIndicator")
            {
                return true;
            }
        }
        */

        return true;
    }

    private bool canSwitchToInteractionMode()
    {
        triesToSwitchToInteractionCounter++;
        //GameObject.Find("ChronoButton").GetComponent<ChronoController>().saveTrySwitchToInteraction();

        //return avatar.GetComponent<AvatarController>().safePlace;
        return true;
    }

    IEnumerator centerIndicatorFadeOut()
    {
        Color color = GameObject.Find("CenterIndicator").GetComponent<MeshRenderer>().material.color;

        for (float i = 3; i >= 0; i -= Time.deltaTime)
        {
            color.a = i * 0.2f / 3.0f;
            GameObject.Find("CenterIndicator").GetComponent<MeshRenderer>().material.color = color;

            yield return null;
        }

        GameObject.Find("CenterIndicator").GetComponent<MeshRenderer>().enabled = false;
        color.a = 0.2f;
        GameObject.Find("CenterIndicator").GetComponent<MeshRenderer>().material.color = color;
    }

    IEnumerator centerIndicatorTextFadeOut()
    {
        Text text = GameObject.Find("CenterIndicatorText").GetComponent<Text>();
        Color originalColor = text.color;

        for (float t = 0.01f; t < 3.0f; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 3.0f));
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        String path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/stats/" + registryFileName;

        using FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fs);

        writer.WriteLine("Marca de tiempo; Evento");

        foreach (KeyValuePair<DateTime, String> evento in registry)
        {
            writer.WriteLine(evento.Key.ToString() + ";" + evento.Value);
        }

        writer.Flush();
    }
}
