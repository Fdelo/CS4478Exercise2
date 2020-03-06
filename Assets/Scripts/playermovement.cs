using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private EdgeCollider2D ec;
    private SpriteRenderer sr;
    private float revSpeed = 50.0f;

    void Awake()
    {
      rb2D = GetComponent<Rigidbody2D>();
      sr = GetComponentInChildren<SpriteRenderer>();
      ec = GetComponent<EdgeCollider2D>();
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

    private void OnCollisionEnter2D(Collision2D other) {

      int newVertArrLen = (sr.sprite.vertices.Length-8);
      int newNumEdges = (sr.sprite.triangles.Length-24);

      IEnumerable<Vector2> updateVert = sr.sprite.vertices.Skip(4).Take(newVertArrLen);

      Vector2[] updateCol =  new Vector2[newVertArrLen/2];
      for(int i = 1; i < newVertArrLen; i+=2)
      {
      Vector2 vector2 = new Vector2(updateVert.ElementAt(i).x - 1, updateVert.ElementAt(i).y - 1);
      updateCol[i / 2] = vector2;
      }
      ushort[] updateTri = new ushort[newNumEdges];

      int j = 0;
      for (int i = 0; i < newVertArrLen-2; i += 2){
        updateTri[j] = (ushort)i;
        updateTri[j+1] = (ushort)(i + 1); 
        updateTri[j+2] = (ushort)(i + 2);
        updateTri[j+3] = (ushort)(i + 1);
        updateTri[j+4] = (ushort)(i + 3);
        updateTri[j+5] = (ushort)(i + 2);
        j += 6;
      }
      
      sr.sprite.OverrideGeometry(updateVert.ToArray(),updateTri);
      ec.points = updateCol;
    }
}
