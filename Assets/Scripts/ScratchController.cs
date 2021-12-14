using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchController : MonoBehaviour
{
    public GameObject haptic;
    public GameObject avatar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(haptic.GetComponent<HapticPlugin>().touching != null)
        {
            GameObject voxel = haptic.GetComponent<HapticPlugin>().touching;

            if (voxel.name == "Dejar" && haptic.GetComponent<HapticPlugin>().touchingDepth > 3.0f)
            {
                Vector3 heading = avatar.transform.position - voxel.transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                voxel.transform.position -= direction * 10.0f;
            }
            else if(voxel.name == "Quitar" && haptic.GetComponent<HapticPlugin>().touchingDepth > 2.0f)
            {
                Vector3 heading = avatar.transform.position - voxel.transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                voxel.transform.position -= direction * 10.0f;
            }
        }
    }
}
