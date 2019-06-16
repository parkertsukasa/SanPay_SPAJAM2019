using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCameraPrintScreen : MonoBehaviour
{
  private int width = 1080;
  private int height = 1920;
  private int fps = 30; 
  WebCamTexture webcamTexture;

  // Start is called before the first frame update
  void Start()
  {
    WebCamDevice[] devices = WebCamTexture.devices;
    webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
    GetComponent<Renderer> ().material.mainTexture = webcamTexture;
    webcamTexture.Play();
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
