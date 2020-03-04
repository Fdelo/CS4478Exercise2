using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float revSpeed = 50.0f;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        rb2D.gravityScale = 0.0f;
    }

    void FixedUpdate()
    {
      //Input.GetAxisRaw("horizontal");
      if (Input.GetKey(KeyCode.A))
      {
        rb2D.MoveRotation(rb2D.rotation - revSpeed * Time.fixedDeltaTime);  
      }
      else if (Input.GetKey(KeyCode.D))
      {
        rb2D.MoveRotation(rb2D.rotation + revSpeed * Time.fixedDeltaTime);  
      }
        
    }
}
