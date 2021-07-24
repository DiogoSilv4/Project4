using UnityEngine; 
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;


public class INTERACTION : MonoBehaviour 
{
    public Material material;

    public GameObject ChoiceMenu;
    public GameObject countDown;
    public GameObject LeftAction, RightAction;
    public WebCamPhotoCamera cammera;

    public float waitingTime;

    public GameObject CurrentScene;
    public GameObject LeftChoiceScene;
    public GameObject RightChoiceScene;
    public bool rigged = false;
    public string Right_Left;

    WebCamTexture text;
    //WebCamTexture webCamTexture;
    float currentTime = 0.0f;
    string result;


    void Start() 
    {
        StartCoroutine("TakePhoto"); 
        //text = cammera.webCamTexture;
    }

    public void XStartCoroutineX(){
        StartCoroutine("TakePhoto");

    }


    IEnumerator TakePhoto(){  // Start this Coroutine on some button click
    

        //webCamTexture = new WebCamTexture();

        GetComponent<Renderer>().material.mainTexture = cammera.webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        cammera.webCamTexture.Play();

        ChoiceMenu.SetActive(true);
        currentTime = waitingTime ;


        yield return new WaitForEndOfFrame(); 

        Texture2D photo = new Texture2D(cammera.webCamTexture.width, cammera.webCamTexture.height);
        photo.SetPixels(cammera.webCamTexture.GetPixels()); 
        photo.Apply();

        material.SetTexture("_MainTex",photo);

        //StorePreviewAsAsset(photo, 1);
        //File.WriteAllBytes(Application.dataPath  + "photo.png", bytes);

        var width = photo.width;
        var height = photo.height;

        Sprite foto1 = TextureToSprite(photo);

        yield return new WaitForSeconds(waitingTime);

        Sprite fotos2;

        Texture2D photo_2 = new Texture2D(cammera.webCamTexture.width, cammera.webCamTexture.height);
        photo_2.SetPixels(cammera.webCamTexture.GetPixels());
        photo_2.Apply();

        material.SetTexture("_MainTex",photo_2);

        //StorePreviewAsAsset(photo_2, 2);

        fotos2 = TextureToSprite(photo_2);
       
        yield return new WaitForSeconds(2);

        Texture2D photoSubtracted = new Texture2D(cammera.webCamTexture.width, cammera.webCamTexture.height);
        photoSubtracted = SubtractImages(foto1, fotos2, width, height);

        material.SetTexture("_MainTex",photoSubtracted);

        //StorePreviewAsAsset(photoSubtracted, 3);

        yield return new WaitForSeconds(5);

        ChoiceMenu.SetActive(false);
        CurrentScene.SetActive(false);

        if(result == "Left"){
            LeftChoiceScene.SetActive(true);
        }else if (result == "Right"){
            RightChoiceScene.SetActive(true);
        }

    }
    



    private Texture2D SubtractImages(Sprite original, Sprite toCompare, int width, int height) {

        Debug.Log("Start the subtraction");
        Color[] pixOriginal = original.texture.GetPixels(
                    Mathf.FloorToInt(original.textureRect.x),
                    Mathf.FloorToInt(original.textureRect.y),
                    Mathf.FloorToInt(original.rect.width),
                    Mathf.FloorToInt(original.rect.height));

        Color[] pixToCompare = toCompare.texture.GetPixels(
                    Mathf.FloorToInt(toCompare.textureRect.x),
                    Mathf.FloorToInt(toCompare.textureRect.y),
                    Mathf.FloorToInt(toCompare.rect.width),
                    Mathf.FloorToInt(toCompare.rect.height));

        Color[] diffPix = new Color[pixOriginal.Length];
        Debug.Log(pixOriginal.Length);
        var maxCount= 0;
        var count = 0; var countLeftSide = 0; var countRigthSide = 0;var countLEFT = 0; var countRIGHT = 0;
         
        if (pixOriginal.Length == pixToCompare.Length)
        {

            for (int i = 0; i < diffPix.Length; i++)
            {
                diffPix[i] = new Color(
                    Mathf.Abs(pixOriginal[i].r - pixToCompare[i].r),
                    Mathf.Abs(pixOriginal[i].g - pixToCompare[i].g),
                    Mathf.Abs(pixOriginal[i].b - pixToCompare[i].b));

                maxCount += 1;
                // Verificar quantos pixeis estão diferentes de 0
                // != new Color(0,0,0,1)
                // == Color.white
                if(  diffPix[i] != new Color(0,0,0,1)  ){
                    count += 1;
                }
                for(int j = 0 ; j < height; j++){

                    if(i < ((width/2)+ j * width) && i > j * width ){
                        countLeftSide += 1;
                        if(  diffPix[i] != new Color(0,0,0,1)  ){
                            countLEFT += 1;
                        }
                    }
                    if(i <  width + ( j * width) && i > width/2  + j * width )  {
                        countRigthSide += 1;
                        if(  diffPix[i] != new Color(0,0,0,1)  ){
                            countRIGHT += 1;
                        }
                    }
                }
                


            }
        }

        Texture2D subtractionTex = new Texture2D(original.texture.width, original.texture.height);
        subtractionTex.SetPixels(diffPix);
        subtractionTex.Apply();


        if(maxCount == 0){
            maxCount= 1;
        }
        float percentage = (count/maxCount)*100;

        Debug.Log("End the subtraction");
        Debug.Log("there were " + count + " from " + maxCount + ". Or "+ percentage + "%");
        Debug.Log("LeftSide is " + countLeftSide + ". And RightSide is  " + countRigthSide + ".");
        Debug.Log("Left has " + countLEFT + ". And Right has  " + countRIGHT + ".");


        result = leftORrigth(countLEFT,countRIGHT);

        return subtractionTex;

    }

    public Sprite TextureToSprite(Texture2D SpriteTexture) {
        
     float PixelsPerUnit = 100.0f;
     Sprite NewSprite;
     NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);
 
     return NewSprite;
   }
    int randomNumber;
    private string leftORrigth(int left, int right)
    {
        if (rigged)
        {
            return Right_Left;
        }
        randomNumber = Random.Range(0,1);
        if (left > right){
            Debug.Log("Left Wins");
            //LeftAction.GetComponent<Image>().color = Color.green;
            return "Left";

        }else if(left < right){
            Debug.Log("Right Wins");
            //RightAction.GetComponent<Image>().color = Color.green;
            return "Right";
        }
        else
        {
            return "Right";
        }
        

    }
    void Update(){

        currentTime -= 1 * Time.deltaTime;
        
        countDown.GetComponent<Text>().text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }


        
    }
    public static readonly string PreviewFileName = "Photo";
    public static readonly string PreviewFileFolder = "InteractionImages";

    //Texture2D StorePreviewAsAsset(Object asset, int index)
    //{
    //   var folder = "Assets/" + PreviewFileFolder;
    //   var fileNamePrefix = PreviewFileName + index;
    //   var fileName = fileNamePrefix + ".asset";
    //   var completePath = folder + '/' + fileName;
    //   // get preview texture from cache
    //   var textureOrig = AssetPreview.GetAssetPreview(asset);
    //   // create new texture that is copy of original
    //   var texture = new Texture2D(textureOrig.width, textureOrig.height, textureOrig.format, false);
    //   texture.SetPixels32(textureOrig.GetPixels32());
    //   texture.name = folder + '/' + fileNamePrefix;
    //   // create folders
    //   if (!Directory.Exists(folder))
    //   {
    //       Directory.CreateDirectory(folder);
    //   }
    //   // delete any existing version of it (if any)
    //   AssetDatabase.DeleteAsset(completePath);
    //   // add to AssetDatabase
    //   AssetDatabase.CreateAsset(texture, completePath);
    //   // load and from here on only reference the texture from the AssetDatabase
    //   var previewAsset = AssetDatabase.LoadAssetAtPath(completePath, typeof(Texture2D));
    //   return (Texture2D)EditorUtility.InstanceIDToObject(previewAsset.GetInstanceID());
    //}

}


