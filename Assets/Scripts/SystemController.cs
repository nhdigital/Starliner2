using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SystemController : MonoBehaviour
{


    public string platform;


    private void Start()
    {
        Platform();
    }


    public void Platform()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            platform = "Unity Editor";
            Debug.Log(platform);
            return;
        }

        if (Application.platform == RuntimePlatform.XboxOne)
        {
            platform = "Xbox";
            Debug.Log(platform);
            return;
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            platform = "Windows";
            Debug.Log(platform);
            return;
        }
    
        if (Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.PS5)
        {
            platform = "Playstation";
            Debug.Log(platform);
            return;
        }
    
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            platform = "WebGL";
            Debug.Log(platform);
            return;
        }
    
        if (Application.platform == RuntimePlatform.Switch)
        {
            platform = "Switch";
            Debug.Log(platform);
            return;
        }

        else
        {
            platform = "Unidentified.";
            Debug.Log("Unidentified Platform");
            return;
        }
    }





}
