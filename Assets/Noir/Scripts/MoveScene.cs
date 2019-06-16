using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
    
    
      int sampleVals = 0;
      OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/b", sampleVals);
  }

  public void moveScene()
  {
    // OSC通信にて"0"を送信
    SceneManager.LoadScene("SendMoney");

    int flag = 0;
    //var sampleVals = new List<int>() { 1, 2, 3 };
    OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/a", flag);
  }
}
