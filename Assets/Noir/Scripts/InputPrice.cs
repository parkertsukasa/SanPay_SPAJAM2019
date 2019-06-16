using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPrice : MonoBehaviour
{
  public Text priceText;
  public int price = 0;

  private InputField addressInput;
  private InputField outGoingInput;
  private InputField inComingInput;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void zero()
  {
    price = price * 10;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void doubleZero()
  {
    price = price * 100;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void one()
  {
    price = price * 10 + 1;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void two()
  {
    price = price * 10 + 2;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void three()
  {
    price = price * 10 + 3;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void four()
  {
    price = price * 10 + 4;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void five()
  {
    price = price * 10 + 5;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void six()
  {
    price = price * 10 + 6;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void seven()
  {
    price = price * 10 + 7;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void eight()
  {
    price = price * 10 + 8;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void nine()
  {
    price = price * 10 + 9;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }

  public void back()
  {
    price = price / 10;
    priceText.text = price.ToString();
    MainGameController.setTotalPrice(price);
  }
}
