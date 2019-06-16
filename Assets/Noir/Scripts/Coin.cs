using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour{

  Rigidbody rb;

  void Awake(){
    rb = this.gameObject.GetComponent<Rigidbody>();
  }

  // Use this for initialization
  void Start(){
    //初期位置
    this.gameObject.transform.position = new Vector3(0, -8, 20);
    Debug.Log("b");

    Invoke("Delete", 3.0f);
  }


  // Update is called once per frame
  void Update(){
    rb.velocity = new Vector3(Random.Range(-8.0f, 8.0f), rb.velocity.y, 0.0f);
  }

  public void Move(){
    rb.velocity = new Vector3(0, 20, 0); //速さ
    rb.velocity = new Vector3(Random.Range(-8.0f, 8.0f), rb.velocity.y, 0.0f);
    Invoke("Delete", 3.0f);
  }

  void Delete(){
    Destroy(this.gameObject);
  }

}
