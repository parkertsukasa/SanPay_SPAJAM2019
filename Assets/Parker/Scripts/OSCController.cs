using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class OSCController : MonoBehaviour {
    #region Network Settings
    public string TargetAddr;
    public int OutGoingPort;
    public int InComingPort;
    public int playerID;
    #endregion
    private Dictionary<string, ServerLog> servers;
      private InputField addressInput;
  private InputField outGoingInput;
  private InputField inComingInput;

  public Text portText;

    // Script initialization
    void Start() {  

        DontDestroyOnLoad(this);

    addressInput = GameObject.Find("IPAdressInputField").GetComponent<InputField>();
    outGoingInput = GameObject.Find("OutGoingPortInputField").GetComponent<InputField>();
    inComingInput = GameObject.Find("InComingPortInputField").GetComponent<InputField>();
    GameObject.Find("DebugCanvas").SetActive(false);

        OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);
        servers = new Dictionary<string, ServerLog>();
        portText.text = "Version : " + OutGoingPort.ToString();
    }

   // TODO 値が変わったら，もう一度initする．
    public void Restart()
    {
        //OSCHandler.ReInstance().Init(TargetAddr, OutGoingPort, InComingPort);
        OSCHandler.ReInstance().Reset();
        OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);

        servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;
    }


    // NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
    void Update() {
        // must be called before you try to read value from osc server
        OSCHandler.Instance.UpdateLogs();

        // データ受信部
        servers = OSCHandler.Instance.Servers;
        foreach( KeyValuePair<string, ServerLog> item in servers )
        {
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            if(item.Value.log.Count > 0) 
            {
                int lastPacketIndex = item.Value.packets.Count - 1;

                UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}", 
                item.Key, // Server name
                item.Value.packets[lastPacketIndex].Address, // OSC address
                item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
            }
        } 

        // データ送信部
        /*
        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("SendMessage");
            var sampleVals = new List<int>(){1, 2, 3};
            OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/a", sampleVals);
        }
        */
    }


    public void sendStartEffect()
    {
      int sampleVals = 0;
      OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/b", sampleVals);
      
    }
    // 
    public void sendFinishSanpai()
    {
      int sampleVals = 1;
      OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/b", sampleVals);
      
    }

    public void sendReturnTitle()
    {
      int sampleValsA = 0;
      OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/b", sampleValsA);      
      int sampleValsB = 0;
      OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/b", sampleValsB);

  
    }

    
// -----------------------------------------------------------------------------
//  アドレスを設定する
// -----------------------------------------------------------------------------
  public void setAddress()
  {
    TargetAddr = addressInput.text;
        //OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);
  }

// -----------------------------------------------------------------------------
//  outポートを設定する
// -----------------------------------------------------------------------------
  public void setOutGoing()
  {
    OutGoingPort = Convert.ToInt32(outGoingInput.text);
        //OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);
  }

// -----------------------------------------------------------------------------
//  OSCを更新
// -----------------------------------------------------------------------------
  public void setUpdateData()
  {
    Restart();
  }

// -----------------------------------------------------------------------------
//  inポートを設定する
// -----------------------------------------------------------------------------
  public void setInComing()
  {
    InComingPort = Convert.ToInt32(inComingInput.text);
       //OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);
  }

    public void set1P()
    {
      playerID = 1;
    }

    public void set2P()
    {
      playerID = 2;
    }

    public void set3P()
    {
      playerID = 1;
    }

}
