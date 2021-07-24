using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChanger : MonoBehaviour
{
    public GameObject[] CurrentScene;
    public GameObject[] AfterScene;
    public float time;

    //private bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetAndGo");
    }
    // Update is called once per frame
     private IEnumerator SetAndGo()
    {

        yield return  new WaitForSeconds(time);
        for(int i = 0; i< AfterScene.Length; i++){
            AfterScene[i].SetActive(true);
        }
        for(int i = 0; i< CurrentScene.Length; i++){
            CurrentScene[i].SetActive(false);
        }


    }
    
}
