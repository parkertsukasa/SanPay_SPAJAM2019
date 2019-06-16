using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
  [SerializeField]
  string nextSceneName;

  private GameObject debugCanvas; 
  private bool debugMode = false;

  private OSCController osc;

  private InputField addressInput;
  private InputField outGoingInput;
  private InputField inComingInput;

  // Start is called before the first frame update
  void Start()
  {
    
    addressInput = GameObject.Find("IPAdressInputField").GetComponent<InputField>();
    outGoingInput = GameObject.Find("OutGoingPortInputField").GetComponent<InputField>();
    inComingInput = GameObject.Find("InComingPortInputField").GetComponent<InputField>();

    debugCanvas = GameObject.Find("DebugCanvas");
    //debugCanvas.SetActive(debugMode);

    osc = GameObject.Find("OSCManager").GetComponent<OSCController>();

    GameObject tempG0 = GameObject.Find("IPAdressInputField");



  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      
      if (Input.mousePosition.x < 50 && Input.mousePosition.y > 1300)
      {
        Debug.Log("DEBUG MODE!");
        debugMode = !debugMode;
        debugCanvas.SetActive(debugMode);
      }
    }

  }

// -----------------------------------------------------------------------------
//  次のシーンに移動する
// -----------------------------------------------------------------------------
  public void goNextSceme()
  {
    SceneManager.LoadScene(nextSceneName);
  }

// -----------------------------------------------------------------------------
//  アドレスを設定する
// -----------------------------------------------------------------------------
  public void setAddress()
  {
    osc.TargetAddr = addressInput.text;
  }

// -----------------------------------------------------------------------------
//  outポートを設定する
// -----------------------------------------------------------------------------
  public void setOutGoing()
  {
    osc.OutGoingPort = Convert.ToInt32(outGoingInput.text);
  }

// -----------------------------------------------------------------------------
//  inポートを設定する
// -----------------------------------------------------------------------------
  public void setInComing()
  {
    osc.InComingPort = Convert.ToInt32(inComingInput.text);
  }

}
