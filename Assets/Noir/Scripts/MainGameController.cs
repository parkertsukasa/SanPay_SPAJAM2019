using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
  public static int totalPrice = 0;
  public static bool isFinish = false;

  public static int getTotalPrice()
  {
    return totalPrice;
  }

  public static void setTotalPrice(int price)
  {
    totalPrice = price;
  }

  public static void setIsFinish(bool flag)
  {
    isFinish = flag;
  }

  public static bool getIsFinish()
  {
    return isFinish;
  }

  void Start()
  {
  }

  void Update()
  {
    
  }

}
