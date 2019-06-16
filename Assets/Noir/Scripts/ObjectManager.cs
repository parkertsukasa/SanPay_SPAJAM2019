using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{

  public GameObject sprite;
  public GameObject nextButton;
  public GameObject priceText;
  public GameObject sentence;
  public GameObject guideAnimation;
  int price;

  // 追記
  //! OSC
  private OSCController oscController;

  bool isPitched = false;

  // Start is called before the first frame update
  void Start()
  {
    MainGameController.setIsFinish(false);

    nextButton.SetActive(false);
    priceText.SetActive(false);
    sentence.SetActive(false);
    price = MainGameController.getTotalPrice();
    Invoke("Appear", 0.40f);
    Invoke("Delete", 4.10f);

    oscController = GameObject.Find("OSCManager").GetComponent<OSCController>();

  }

  // Update is called once per frame
  void Update(){
    if (MainGameController.getIsFinish()){
    // スワイプオブジェクトの削除
      sprite.SetActive(false);
      Delete();

      //MoneyManager manager = GameObject.Find("Manager").GetComponent<MoneyManager>();
      Invoke("Active", 1.6f);

      if (isPitched)
      oscController.sendStartEffect();

    }
  }

  void Active()
  {
    nextButton.SetActive(true);
    priceText.SetActive(true);
    sentence.SetActive(true);
    priceText.GetComponent<Text>().text = price.ToString() + "円";
  
    isPitched = true;
    // ここで送信
    oscController.sendStartEffect();
  }

  public void moveScene()
  {
    // OSC通信にて"1"を送信
    SceneManager.LoadScene("SanPai");

    //int flag = 1;
    //var sampleVals = new List<int>() { 1, 2, 3 };
    //OSCHandler.Instance.SendMessageToClient("iPhoneTouchOSCApp", "/hoge/a", flag);
  }

  void Delete()
  {
    guideAnimation.SetActive(false);
  }

  void Appear()
  {
    guideAnimation.SetActive(true);
  }
}
