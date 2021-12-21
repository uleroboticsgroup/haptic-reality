using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchController : MonoBehaviour
{
    public GameObject haptic;
    public GameObject avatar;

    private int lastTouchedRedVoxelId = 0;
    private int lastRemovedVoxelId = 0;

    private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(haptic.GetComponent<HapticPlugin>().touching != null)
        {
            GameObject voxel = haptic.GetComponent<HapticPlugin>().touching;
            int voxelId = voxel.GetInstanceID();

            if (voxel.name == "Dejar")
            {
                if(voxelId != lastTouchedRedVoxelId)
                {
                    scene.register("Tocando voxel rojo");
                }

                lastTouchedRedVoxelId = voxelId;

                if(haptic.GetComponent<HapticPlugin>().touchingDepth > 3.5f && voxelId != lastRemovedVoxelId)
                {
                    Vector3 heading = avatar.transform.position - voxel.transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance;

                    voxel.transform.position -= direction * 10.0f;

                    scene.register("Eliminado voxel rojo");
                    scene.incrementRemoved("Dejar");

                    lastRemovedVoxelId = voxelId;
                }
            }
            else if(voxel.name == "Quitar" && haptic.GetComponent<HapticPlugin>().touchingDepth > 1.75f && voxelId != lastRemovedVoxelId)
            {
                Vector3 heading = avatar.transform.position - voxel.transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                voxel.transform.position -= direction * 10.0f;

                scene.register("Eliminado voxel verde");
                scene.incrementRemoved("Quitar");

                lastRemovedVoxelId = voxelId;
            }
            else
            {
                lastTouchedRedVoxelId = 0;
            }
        }
        else
        {
            lastTouchedRedVoxelId = 0;
        }
    }
}
