using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public float WaitBefore;
    public GameObject character;
    public GameObject real_character;
    public GameObject[] character_delete;
    public Material[] materials;
    //public Material dissove_material;
    public GameObject star;
    public GameObject StarRestingPlace;
    public float velocity;
    public bool grandma;

    [Range(0, 1.0f)]
    public float threshold;
    //public float maxStarSize;
    //public float GrowthVelocity;

    private GameObject StarX;
    private Transform characterLastPosition;
    private bool check = false, startEffect = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CharacterKill");
    }

    IEnumerator CharacterKill(){ 

        yield return new WaitForSeconds(WaitBefore);
        //character.material = dissove_material;
        characterLastPosition = character.transform;
        //characterLastPosition.rotation =  Quaternion.Euler (characterLastPosition.rotation.x, characterLastPosition.rotation.y + 180, characterLastPosition.rotation.z) ;
        threshold = 0;
        real_character.transform.position = characterLastPosition.position;
        for(int i = 0; i< character_delete.Length; i++){
            character_delete[i].SetActive(false);
        }
        real_character.SetActive(true);

        startEffect = true;
        yield return new WaitForSeconds(2);
        check = true;
        StarX = Instantiate(star, characterLastPosition);
        if(grandma){
            StarX.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        }else{
            StarX.transform.localScale = new Vector3(0.08351544f,0.08351544f,0.08351544f);
        }
        yield return new WaitForSeconds(2);
        
        
        //Destroy(character);

    }
    // Update is called once per frame
    void Update()
    {
        if(check){
            // var scaleChange =+ (1 * Time.deltaTime + GrowthVelocity) / 10;
            // if(scaleChange >= maxStarSize){
            //     scaleChange = maxStarSize;
            // }
            // Vector3 changeScale = new Vector3 (scaleChange,scaleChange,scaleChange);
            // StarX.transform.localScale = changeScale;
            
            var speed = velocity * Time.deltaTime;
            StarX.transform.position = Vector3.MoveTowards(StarX.transform.position, StarRestingPlace.transform.position, speed);
        }
        if(startEffect && threshold < 1 ){
            threshold += 0.3f * Time.deltaTime; 
            for(int i = 0; i < materials.Length; i++){
                materials[i].SetFloat("_DissolveThreshold", threshold); 
            }
        }
    }
}
