using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : MonoBehaviour
{
    public GameObject Laser;
    public float waitTime;
    public float time;
    public float laserVelocity;
    public float maxWidth;

    private bool check = false;
    private bool endCheck = false;

    private Vector3 starPos, endPos;
    private float starY, endY;

    //private VolumetricLineBehavior volumetricLineBehavior;

    // Start is called before the first frame update
    void Start()
    {
        starPos = new Vector3(0 , 0, 0);
        endPos = new Vector3(0 , 0, 0);
        starY = 0;
        endY = 0;

        //starPos = gameObject.GetComponent<VolumetricLineBehavior>().StartPos ;
        //script.StarPos = starPos; 
        //script.EndPos = endPos; 

        //StartCoroutine("Shoot");

    }

    private IEnumerator Shoot()
    {

        yield return  new WaitForSeconds(waitTime);
        check = true;
        yield return  new WaitForSeconds(time);
        endCheck= true;
        


    }
    // Update is called once per frame
    void Update()
    {
        if(check && starY < maxWidth ){
            starY += laserVelocity * Time.deltaTime;
            starPos = new Vector3(0 , starY, 0);
            endPos = new Vector3(0 , 0, 0);
            
            
        }
        if(endCheck && endY < maxWidth){
            endY += laserVelocity * Time.deltaTime;
            starPos = new Vector3(0 , 0, 0);
            endPos = new Vector3(0 , endY, 0);


        }
    }
    
		
}
