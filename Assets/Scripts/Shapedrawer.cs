using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapedrawer : MonoBehaviour
{
  
  
  SpriteRenderer sr;
  int NumEdges = 24;
  float RadiusOne = 1f;
  float RadiusTwo = 0.9f;

  Vector2[] vertices;
  ushort[] triangles;
  private void Awake() {
    sr = GetComponent<SpriteRenderer>();
  
  }
  void Start()
  {     
    DefinePoints();
    DrawPolygon2D(vertices, triangles, Color.red);      
  }


  void DefinePoints()
  {
    vertices = new Vector2[NumEdges+2];

    int i = 0;      
    do
    {
      float angle = (2 * Mathf.PI * i / NumEdges) + Mathf.PI;
      float x_one = (RadiusOne * Mathf.Cos(angle)) + 1;
      float y_one = (RadiusOne * Mathf.Sin(angle)) + 1;
      float x_two = (RadiusTwo * Mathf.Cos(angle)) + 1;
      float y_two = (RadiusTwo * Mathf.Sin(angle)) + 1;


      vertices[i*2] = new Vector2(x_one, y_one);
      vertices[i*2 + 1] = new Vector2(x_two, y_two);
      i++;
      
    } while ((2 * Mathf.PI * i / NumEdges) + Mathf.PI <= 2 *  Mathf.PI && (2 * Mathf.PI * i / NumEdges) + Mathf.PI >= Mathf.PI);

    //points[NumEdges] = points[0];
    
    int j = 0;
    triangles = new ushort[NumEdges*3];
    for (i = 0; i < NumEdges; i += 2){
      triangles[j] = (ushort)i;
      triangles[j+1] = (ushort)(i + 1); 
      triangles[j+2] = (ushort)(i + 2);
      triangles[j+3] = (ushort)(i + 1);
      triangles[j+4] = (ushort)(i + 3);
      triangles[j+5] = (ushort)(i + 2);
      j += 6;
    }
        
    }
  
  void DrawPolygon2D(Vector2[] vertices, ushort[] triangles, Color color)
  {
    if(sr == null)
      return;

      // GameObject polygon = new GameObject(); //create a new game object
      
      Texture2D texture = new Texture2D(3, 3); // create a texture larger than your maximum polygon size

      // create an array and fill the texture with your color
      // List<Color> cols = new List<Color>(); 
      // for (int i = 0; i < (texture.width * texture.height); i++)
      //   cols.Add(color);
      // texture.SetPixels(cols.ToArray());
      // texture.Apply();

      // sr.color = color; //you can also add that color to the sprite renderer

      sr.sprite = Sprite.Create(texture, new Rect(0,0, 2, 2), Vector2.zero, 1); //create a sprite with the texture we just created and colored in

      // convert coordinates to local space
      float lx = Mathf.Infinity, ly = Mathf.Infinity;
      foreach (Vector2 vi in vertices)
      {
          if (vi.x < lx)
              lx = vi.x;
          if (vi.y < ly)
              ly = vi.y;
      }
      Vector2[] localv = new Vector2[vertices.Length];
      for (int i = 0; i < vertices.Length; i++)
      {
          localv[i] = vertices[i] - new Vector2(lx, ly);
      }

      sr.sprite.OverrideGeometry(vertices, triangles); // set the vertices and triangles

      // polygon.transform.position = (Vector2)transform.InverseTransformPoint(transform.position); // return to world space
  }

}
