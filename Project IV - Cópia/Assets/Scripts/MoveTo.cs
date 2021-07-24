using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public GameObject Camera;
    public GameObject[] Pontos;
    public float[] velocity;
    public string[] direction;
    public float waitBefore;
    public bool FollowObject;
    public GameObject Object;
    public string[] Minus_or_Plus;

    bool camerastart = false;
    float Camera_P,Point_P;
    bool[] getThere;
    // int cont = 0;

    private Transform objects;

    void Start()
    {
        objects = Object.GetComponent<Transform>();
        StartCoroutine("StartMovementx");
        getThere = new bool[Pontos.Length];
        for(int i = 0; i < Pontos.Length; i++){
             getThere[i] = false;
        }
    }
 
    private IEnumerator StartMovementx(){

        

        yield return new WaitForSeconds(waitBefore);
        camerastart = true;
        

        print("Coroutine ended: " + Time.time + " seconds");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(FollowObject){
                Camera.transform.LookAt(objects);
        }

        if(camerastart){

            var number = -1;
            foreach (bool ponto in getThere)
            {
                number += 1;
                if(ponto == false){
                    break;
                }
            }

            


            var speed = velocity[number] * Time.deltaTime;
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Pontos[number].transform.position, speed);


            switch (direction[number])
            {
                case "x" : Camera_P = Camera.transform.position.x;
                Point_P = Pontos[number].transform.position.x;
                break;
                case "y" : Camera_P = Camera.transform.position.y;
                Point_P = Pontos[number].transform.position.y;
                break;
                case "z" : Camera_P = Camera.transform.position.z;
                Point_P = Pontos[number].transform.position.z;
                break;

            }
            if(Minus_or_Plus[number] == "-"){
                if(Camera_P <= Point_P  ){ //&& cont == 0
                //cont += 1;
                getThere[number] = true;
                }
            }else if(Minus_or_Plus[number] == "+"){
                if(Camera_P >= Point_P  ){ // && cont == 0
                //cont += 1;
                getThere[number] = true;

                }
            }
            
            
        }
        
        
    }
}
