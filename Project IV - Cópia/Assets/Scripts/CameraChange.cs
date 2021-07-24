using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;

    public float[] Times;
   
   void Start()
    {
        StartCoroutine("CamerasChange");
    }

    private IEnumerator CamerasChange(){

        var count = 0;
        foreach (var timez in Times)
        {
            count += 1;
            yield return new WaitForSeconds(timez);
            if (count != Times.Length){
                
            
            if(count % 2 == 0 ){

                Camera1.SetActive(true);
                Camera2.SetActive(false);

            }else{
                Camera2.SetActive(true);
                Camera1.SetActive(false);

            }
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
