using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffectController : MonoBehaviour
{
    private SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            GetComponentInParent<HapticEffect>().enabled = false;
            scene.register("La burbuja deja de moverse");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            GetComponentInParent<HapticEffect>().enabled = true;
            scene.register("La burbuja se mueve");
        }
    }
}
