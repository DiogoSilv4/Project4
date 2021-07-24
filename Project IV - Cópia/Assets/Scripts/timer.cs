using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
   public float waitingBefore;
    public GameObject Camera, ponto2_camera, ponto3;
    public float velocity;
    bool camerastart = false;
    bool turnON = false;
    public GameObject timeWarp;
    public GameObject Before,After;
    public GameObject panel;
    public bool InitialWarp;
    public ParticleSystem WarpDrive;

    private Image yup;
    private GameObject WarpDrives;
    
    void Start()
    {
        StartCoroutine("WaitAndPrint");
        turnON = false;
    }
 
    private IEnumerator WaitAndPrint()
    {

        yield return  new WaitForSeconds(waitingBefore);

        WarpDrives = GameObject.FindWithTag("warp");
        
        if(WarpDrives!=null){
            if(InitialWarp){
                StartCoroutine("InitialW");
            }else{
                WarpDrives.SetActive(false);
            }
            
        }
        camerastart = true;

        print("Coroutine ended: " + Time.time + " seconds");
    }
    private IEnumerator InitialW()
    {
        WarpDrives.SetActive(true);
        //WarpDrive.Simulate(3.0f, true,true,true);
        yield return  new WaitForSeconds(0.5f);
        WarpDrives.SetActive(false);
        
        print("Coroutine ended: " + Time.time + " seconds");
    }
    private IEnumerator EndofScene()
    {
        // yup =  panel.GetComponent<Image>().Color;
        // for(int i = 0; i< 255 ; i++){
        //     yup.Color = new Color(color.r, color.g,color.b, i);
        // }

        //yield return  new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(0.5f);

        After.SetActive(true);
        Before.SetActive(false);
        print("Coroutine ended: " + Time.time + " seconds");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var speed = velocity * Time.deltaTime;
        if(camerastart && !turnON){

            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, ponto2_camera.transform.position, speed);
             //Debug.Log(Camera.transform.position);

            if(Camera.transform.position.y <= ponto2_camera.transform.position.y + 1 ){
                timeWarp.SetActive(true);
                //Debug.Log("ok");
                turnON = true;
            }
        }
        if(turnON){
            //Debug.Log("ok");
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, ponto3.transform.position, speed);
            velocity = velocity +5;

            if(Camera.transform.position.y <= ponto3.transform.position.y + 1 ){
                //timeWarp.SetActive(false);
                StartCoroutine("EndofScene");


            }

        }

        
    }
}
