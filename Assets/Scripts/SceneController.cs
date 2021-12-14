using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public HapticButtonsController hapticButtons;
    // Start is called before the first frame update
    void Start()
    {
        
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
            }
        }
    }
}
