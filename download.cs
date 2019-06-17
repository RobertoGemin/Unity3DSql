using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Dummiesman;

public class download : MonoBehaviour
{
    public Texture myTexture;
   public Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetText");
    }


   public IEnumerator newTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://raw.githubusercontent.com/RobertoGemin/3D-models/master/360_render.png");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
             myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/RobertoGemin/3D-models/master/ModernVilla.obj");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            string dir = "/Resources/";
            string path = "/Resources/test.obj";

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        
            if (!Directory.Exists(path))
            {
                //if it doesn't, create it
                Directory.CreateDirectory(Application.dataPath + dir);
                Debug.Log("save" + Application.dataPath + dir);
            }

                File.WriteAllBytes(Application.dataPath + path, results);

            GameObject loadedObject;
            loadedObject = new OBJLoader().Load(Application.dataPath + path);

            /*
               //string path = "Assets/Resources/Models/mesh1";
                mesh = (Mesh)Resources.Load(Application.dataPath + path, typeof(Mesh));
               this.gameObject.AddComponent<MeshFilter>();
               GetComponent<MeshFilter>().mesh = mesh;
               */


        }
    }

}