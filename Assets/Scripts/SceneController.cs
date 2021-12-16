using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SceneController : MonoBehaviour
{
    public HapticButtonsController hapticButtons;

    private Dictionary<DateTime, String> registry = new Dictionary<DateTime, String>();
    private String registryFileName;

    // Start is called before the first frame update
    void Start()
    {
        registryFileName = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".csv";
        registry.Add(DateTime.Now, "Cargada la escena " + SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if(hapticButtons.lightSimple) {
            switch( SceneManager.GetActiveScene().name) {
                case "EasyLevel":
                    SceneManager.LoadScene("MediumLevel", LoadSceneMode.Single);
                    break;
                case "MediumLevel":
                    SceneManager.LoadScene("HardLevel", LoadSceneMode.Single);
                    break;
                case "HardLevel":
                    SceneManager.LoadScene("ExtraHardLevel", LoadSceneMode.Single);
                    break;
                case "ExtraHardLevel":
                    Application.Quit();
                    break;
                case "BubbleEasyLevel":
                    SceneManager.LoadScene("BubbleMediumLevel", LoadSceneMode.Single);
                    break;
                case "BubbleMediumLevel":
                    SceneManager.LoadScene("BubbleHardLevel", LoadSceneMode.Single);
                    break;
                case "BubbleHardLevel":
                    SceneManager.LoadScene("BubbleExtraHardLevel", LoadSceneMode.Single);
                    break;
                case "BubbleExtraHardLevel":
                    Application.Quit();
                    break;
                default:
                    Application.Quit();
                    break;
            }
        }
    }

    public void register(String e)
    {
        registry.Add(DateTime.Now, e);
    }

    private void OnDestroy()
    {
        registry.Add(DateTime.Now, "Terminando nivel");

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
