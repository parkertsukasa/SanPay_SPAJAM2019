using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoenScript : MonoBehaviour
{

  Rigidbody rb;

  void Awake()
  {
    rb = this.gameObject.GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start()
  {
    rb.velocity = new Vector3(0, 7.0f, 2.7f); //速さ
  }

  // Update is called once per frame
  void Update()
  {
    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - 0.158f, rb.velocity.z);
  }

  public void Move()
  {
    rb.velocity = new Vector3(0, 7.0f, 2.7f); //速さ
    Invoke("Delete", 3.0f);
  }

  void Delete()
  {
    Destroy(this.gameObject);
  }
}
