using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInverseController : MonoBehaviour
{

    public GameObject bubble;
    public GameObject avatar;
    public GameObject workspace;
    public GameObject camera;
    public GameObject scene;

    public GameObject haptic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(workspace.GetComponent<HapticEffect>().enabled)
        {
            scene.transform.position += (bubble.transform.position - avatar.transform.position) * Time.deltaTime * Vector3.Distance(bubble.transform.position, avatar.transform.position) * 6.0f;
            camera.transform.position += (bubble.transform.position - avatar.transform.position) * Time.deltaTime * Vector3.Distance(bubble.transform.position, avatar.transform.position) * 6.0f;

            updateBubbleRotation();
        }
    }

    void updateBubbleRotation()
    {
        haptic.transform.rotation = Camera.main.transform.rotation;
    }
}
