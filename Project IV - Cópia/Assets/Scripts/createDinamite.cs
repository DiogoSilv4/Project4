using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createDinamite : MonoBehaviour
{
    public Object dinamite;
    public Transform position;
    public float waitime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("createExplosion");
    }


    IEnumerator createExplosion(){ 
        
        yield return new WaitForSeconds(waitime);
        Instantiate(dinamite, position.position, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
