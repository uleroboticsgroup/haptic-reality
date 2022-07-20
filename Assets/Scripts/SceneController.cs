using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class SceneController : MonoBehaviour
{
    private List<DateTime> registryTimestamps = new List<DateTime>();
    private List<String> registryEvents = new List<String>();
    private String registryFileName;

    private int removedQuitar = 0;
    private int removedDejar = 0;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        registryFileName = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".csv";

        this.register("Cargada la escena " + SceneManager.GetActiveScene().name);

        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Mouse.current.leftButton.wasReleasedThisFrame) {

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
        */
    }

    public void register(String e)
    {
        registryTimestamps.Add(DateTime.Now);
        registryEvents.Add(e);
    }

    public void setVoxelCount(String voxelType, int count)
    {
        if(voxelType == "Dejar")
        {
            this.register("Numero de voxels a dejar " + count);
        }
        else
        {
            this.register("Numero de voxels a quitar " + count);
        }
    }

    public void incrementRemoved(String voxelType)
    {
        if(voxelType == "Dejar")
        {
            removedDejar++;
        }
        else
        {
            removedQuitar++;
        }
    }

    private void OnDestroy()
    {
        this.register("Terminando nivel");

        this.register("Numero de voxels a quitar eliminados " + removedQuitar);
        this.register("Numero de voxels a dejar eliminados " + removedDejar);

        String path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/stats/" + registryFileName;

        using FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fs);

        writer.WriteLine("Marca de tiempo; Evento");

        for(int i=0; i<registryTimestamps.Count; i++)
        {
            writer.WriteLine(registryTimestamps[i].ToString("yyyy-MM-dd HH:mm:ss:fff") + ";" + registryEvents[i]);
        }

        writer.Flush();
    }
}
