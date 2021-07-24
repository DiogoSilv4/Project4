using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkyboxChanged : MonoBehaviour
{
    public Material Skyboxe;
    // Start is called before the first frame update
    void Start()
    {
        ChangeSkybox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSkybox()
    {
        RenderSettings.skybox = Skyboxe;
    }
}
