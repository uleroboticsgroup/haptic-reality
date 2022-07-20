using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (SceneManager.GetActiveScene().name == "HapticRealityTest")
            {
                SceneManager.LoadScene("BubbleTest", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("HapticRealityTest", LoadSceneMode.Single);
            }
        }
    }
}
