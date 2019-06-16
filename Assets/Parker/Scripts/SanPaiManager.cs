using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanPaiManager : MonoBehaviour
{
  //! 参拝ルーチン
  enum sanpaiRoutine {
    PRE_SANPAI,
    FIRST_REI,
    SECOND_REI,
    FIRST_CLAP,
    SECOND_CLAP,
    FIN_SANPAI,
  }
  //! 参拝ステート
  private sanpaiRoutine sanpaiState = sanpaiRoutine.PRE_SANPAI;

  //! 連続して判定が取られないようにする
  private bool coolTimeRei = false;
  private bool coolTimeClap = true;

  //! 音源リソース
  [SerializeField]
  private AudioClip[] succeseActionSE = new AudioClip[5];
  //! 音源ごとの音量
  [SerializeField]
  private float[] volumes = new float[6];

  //! ルーチンごとの文字テクスチャ
  [SerializeField]
  private Sprite[] stateStringTexture = new Sprite[5];

  //! タイトルへ戻るボタン
  private GameObject returnButton;

  //! 状態表示Image
  private Image stateImage;

  //! OSC
  private OSCController oscController;


  //! ----- 以下リリース時削除 -----

  //! デバッグ用
  [SerializeField]
  private Text inputXText;
  [SerializeField]
  private Text inputZText;
  [SerializeField]
  private Text stateText;


  // Start is called before the first frame update
  void Start()
  {
    returnButton = GameObject.Find("ReturnButton");
    returnButton.SetActive(false);
    stateImage = GameObject.Find("StringImage").GetComponent<Image>();
    stateImage.sprite = stateStringTexture[0];
    oscController = GameObject.Find("OSCManager").GetComponent<OSCController>();
  }

  // Update is called once per frame
  void Update()
  {
    sanpaiStateCheck();

    //! デバッグ用
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if ((int)sanpaiState < 5)
        sanpaiState++;
      else
      {
        returnButton.SetActive(true);
        oscController.sendFinishSanpai();
      }
    }
  }

  // -----------------------------------------------------------------------------
  //  参拝の状態を確認してステートを先に進める
  // -----------------------------------------------------------------------------
  void sanpaiStateCheck()
  {
    Vector3 inputAccel = Input.acceleration;

    float thresholdRei = 1.0f;


    float xAccel = inputAccel.x * -1;
    float zAccel = inputAccel.z;

    stateText.text = sanpaiState.ToString();
    inputXText.text = "X軸" + xAccel.ToString();
    inputZText.text = "Z軸" + zAccel.ToString();

    switch (sanpaiState)
    {
      // 最初 
      case sanpaiRoutine.PRE_SANPAI:

      oscController.sendStartEffect();

      stateImage.sprite = stateStringTexture[0];

      if (xAccel > thresholdRei)
      {
        sanpaiState = sanpaiRoutine.FIRST_REI;
        coolTimeRei = true;
        playSE(0);
      }

      break;

      // 1度礼をしたあと
      case sanpaiRoutine.FIRST_REI:

      oscController.sendStartEffect();

      stateImage.sprite = stateStringTexture[1];
      
      // 一回戻るまではクールタイム
      if (coolTimeRei)
      {
        if (xAccel < 0.2f)
          coolTimeRei = false;
      }
      else
      {
        if (xAccel > thresholdRei)
        {
          sanpaiState = sanpaiRoutine.SECOND_REI;
          coolTimeRei = true;
          playSE(1);
        }
      }
      break;

      // 2度礼をした後
      case sanpaiRoutine.SECOND_REI:
      
      oscController.sendStartEffect();

      stateImage.sprite = stateStringTexture[2];
      
      // 一回戻るまではクールタイム
      if (coolTimeRei || coolTimeClap)
      {
        // 体を起こしてリセット
        if (xAccel < 0.2f)
          coolTimeRei = false;

        //  手を開いてリセット
        if (zAccel < -0.7f)
          coolTimeClap = false;
      }
      else
      {
        // 手を立てて拍手判定
        if (zAccel > 0.1f)
        {
          coolTimeClap = true;
          coolTimeRei = true;
          sanpaiState = sanpaiRoutine.FIRST_CLAP;
          playSE(2);
        }
      }
      break;

      // 1度手を叩いた後
      case sanpaiRoutine.FIRST_CLAP:
      
      oscController.sendStartEffect();

      stateImage.sprite = stateStringTexture[3];
      
      // 一回戻るまではクールタイム
      if (coolTimeClap)
      {
        //  手を開いてリセット
        if (zAccel < -0.7f)
          coolTimeClap = false;
      }
      else
      {
        // 手を立てて拍手判定
        if (zAccel > 0.1f)
        {
          coolTimeClap = true;
          coolTimeRei = true;
          sanpaiState = sanpaiRoutine.SECOND_CLAP;
          playSE(3);
        }
      }
      break;

      // 2度手を叩いた後
      case sanpaiRoutine.SECOND_CLAP:
      
      oscController.sendStartEffect();

      stateImage.sprite = stateStringTexture[4];
      
      if (coolTimeRei)
      {
        // 体を起こしてリセット
        if (xAccel < 0.2f)
          coolTimeRei = false;
      }
      else
      {
        // 一礼判定
        if (xAccel > thresholdRei)
        {
          coolTimeClap = true;
          coolTimeRei = true;
          sanpaiState = sanpaiRoutine.FIN_SANPAI;
          playSE(4);
          returnButton.SetActive(true);

          // OSC通信で2礼2拍手1礼が終わったことを通知
          oscController.sendFinishSanpai();

        }
      }

      break;

      case sanpaiRoutine.FIN_SANPAI:


      // OSC通信で2礼2拍手1礼が終わったことを通知
      oscController.sendFinishSanpai();
      stateImage.sprite = stateStringTexture[5];
      
      break;

      default :

      break;
    }
  }

  // -----------------------------------------------------------------------------
  //  index, 音量を指定してSEを鳴らす
  //  playSE(index);
  // -----------------------------------------------------------------------------
  void playSE(int index)
  {
    AudioSource audio = GetComponent<AudioSource>();
    audio.clip = succeseActionSE[index];
    audio.volume = volumes[index];
    audio.Play();
  }


  // -----------------------------------------------------------------------------
  //  最初に戻るボタンを押した時の処理
  // -----------------------------------------------------------------------------
  public void returnTitleButton()
  {
    // OSC通信でタイトルへ戻ることを通知
    oscController.sendReturnTitle();
    SceneManager.LoadScene("InputPrice");
  }

}
