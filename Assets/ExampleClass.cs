     using UnityEngine;
     using System.Collections;
     
     public class ExampleClass : MonoBehaviour {
         public Material mat;
         void OnRenderImage(RenderTexture src, RenderTexture dest) {
            for(int x =0; x< 90; x++)
        {

        }
        Graphics.Blit(src, dest);
         }
     }