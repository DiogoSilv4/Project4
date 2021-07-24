using UnityEngine; 
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;


public class WebCamPhotoCamera : MonoBehaviour 
{
    public WebCamTexture webCamTexture;
    public Material material;
    public GameObject SubtractionResult;


    void Start() 
    {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();

        //StartCoroutine("TakePhoto"); 
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {


        yield return new WaitForEndOfFrame(); 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Texture2D[] photos= new Texture2D[];

        material.SetTexture("_MainTex",photo);

        var width = photo.width;
        var height = photo.height;
        //byte[] foto1 = photo.EncodeToPNG();

        Sprite foto1 = TextureToSprite(photo);


        //Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(Application.dataPath  + "photo.png", bytes);

        yield return new WaitForSeconds(2);

        Sprite fotos2;

        Texture2D photo_2 = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo_2.SetPixels(webCamTexture.GetPixels());
        photo_2.Apply();

        material.SetTexture("_MainTex",photo_2);


        fotos2 = TextureToSprite(photo_2);
       

        yield return new WaitForSeconds(2);

        Texture2D photoSubtracted = new Texture2D(webCamTexture.width, webCamTexture.height);
        photoSubtracted = SubtractImages(foto1, fotos2, width, height);


        // Display the result.
        material.SetTexture("_MainTex",photoSubtracted);

    }
    
    public void startCorroutine(){
        StartCoroutine("TakePhoto");
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

        SubtractionResult.GetComponent<Renderer>().material.mainTexture = subtractionTex;

        if(maxCount == 0){
            maxCount= 1;
        }
        float percentage = (count/maxCount)*100;

        Debug.Log("End the subtraction");
        Debug.Log("there were " + count + " from " + maxCount + ". Or "+ percentage + "%");
        Debug.Log("LeftSide is " + countLeftSide + ". And RightSide is  " + countRigthSide + ".");
        Debug.Log("Left has " + countLEFT + ". And Right has  " + countRIGHT + ".");


        leftORrigth(countLEFT,countRIGHT);

        return subtractionTex;

    }

    public Sprite TextureToSprite(Texture2D SpriteTexture) {
        
     float PixelsPerUnit = 100.0f;
     Sprite NewSprite;
     NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);
 
     return NewSprite;
   }
 
    private void leftORrigth (int left, int right){
        
        if(left > right){
            Debug.Log("Left Wins");
        }
        else{
            Debug.Log("Right Wins");

        }
    }

   
}


