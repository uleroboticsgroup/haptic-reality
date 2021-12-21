using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    public HapticButtonsController hapticButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hapticButtons.lightSimple)
        {
            hapticButtons.lightSimple = false;

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
