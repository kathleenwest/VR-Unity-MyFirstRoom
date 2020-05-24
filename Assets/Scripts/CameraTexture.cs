using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour
{
    private WebCamTexture camTexture = null;
    //
    //
    // Start is called before the first frame update
    void Start()
    {
        camTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = camTexture;
        camTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
