using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phomeCamera : MonoBehaviour
{
    private bool camaraHabilitada;

    private WebCamTexture backCam;
    private Texture defaultBackGround;

    public RawImage background;

    public AspectRatioFitter fit;


    // Start is called before the first frame update
    void Start()
    {
        defaultBackGround = background.texture;

        WebCamDevice[] devices=WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("no hay camara detectada");
            camaraHabilitada = false;
            return;

        }
        for (int i = 0; i < devices.Length; i++) 
        {
            if (!devices[i].isFrontFacing) 
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }
        }

        if (backCam == null)
        {
            Debug.Log("no se encuentra la camara");
            return;
        }
        
        backCam.Play();
        background.texture = backCam;
        camaraHabilitada = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!camaraHabilitada) 
        {
            return;
        }
        float ratio=(float) backCam.width/(float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY= backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient=-backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles=new Vector3(0,0,orient);

    }
}
