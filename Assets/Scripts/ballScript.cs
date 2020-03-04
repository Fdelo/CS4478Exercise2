using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 velocity = new Vector2(0.0f, -0.5f);

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D.gravityScale = 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D other) {
      
      velocity.y = velocity.y * -1;
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
      
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
