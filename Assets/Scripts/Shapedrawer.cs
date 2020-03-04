using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapedrawer : MonoBehaviour
{
  

    int NumEdges = 24;
    float RadiusOne = 1f;
    float RadiusTwo = 0.9f;

    Vector2[] vertices;
    ushort[] triangles;
    void Start()
     {
         //Vector2[] vertices = new Vector2[] { new Vector2(1, 1), new Vector2(.05f, 1.3f), new Vector2(1, 2), new Vector2(1.95f, 1.3f), new Vector2(1.58f, 0.2f), new Vector2(.4f, .2f) };
         //ushort[] triangles = new ushort[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 1 };
         DefinePoints();
         DrawPolygon2D(vertices, triangles, Color.red);
     }
 

    void DefinePoints()
    {
      vertices = new Vector2[NumEdges+1];

      for (int i = 0; i < NumEdges; i++)
      {
        float angle = 2 * Mathf.PI * i / NumEdges;
        float x_one = RadiusOne * Mathf.Cos(angle);
        float y_one = RadiusOne * Mathf.Sin(angle);
        // float x_two = RadiusTwo * Mathf.Cos(angle);
        // float y_two = RadiusTwo * Mathf.Sin(angle);


        vertices[i] = new Vector2(x_one, y_one);
        // vertices[i+1] = new Vector2(x_two, y_two);
      }
      //points[NumEdges] = points[0];
      
      int j = 0;
      triangles = new ushort[NumEdges*6];
      for (int i = 0; i < NumEdges; i += 2){
        triangles[j] = (ushort)i;
        triangles[j+1] = (ushort)(i + 2); 
        triangles[j+2] = (ushort)(i + 1);
        triangles[j+3] = (ushort)(i + 1);
        triangles[j+4] = (ushort)(i + 2);
        triangles[j+5] = (ushort)(i + 3);
        j += 6;
      }
         
     }
    
    void DrawPolygon2D(Vector2[] vertices, ushort[] triangles, Color color)
    {
        GameObject polygon = new GameObject(); //create a new game object
        SpriteRenderer sr = polygon.AddComponent<SpriteRenderer>(); // add a sprite renderer
        Texture2D texture = new Texture2D(1025, 1025); // create a texture larger than your maximum polygon size

        // create an array and fill the texture with your color
        List<Color> cols = new List<Color>(); 
        for (int i = 0; i < (texture.width * texture.height); i++)
          cols.Add(color);
        texture.SetPixels(cols.ToArray());
        texture.Apply();

        sr.color = color; //you can also add that color to the sprite renderer

        sr.sprite = Sprite.Create(texture, new Rect(-512, -512, 1024, 1024), Vector2.zero, 1); //create a sprite with the texture we just created and colored in

        // //convert coordinates to local space
        // float lx = Mathf.Infinity, ly = Mathf.Infinity;
        // foreach (Vector2 vi in vertices)
        // {
        //     if (vi.x < lx)
        //         lx = vi.x;
        //     if (vi.y < ly)
        //         ly = vi.y;
        // }
        // Vector2[] localv = new Vector2[vertices.Length];
        // for (int i = 0; i < vertices.Length; i++)
        // {
        //     localv[i] = vertices[i] - new Vector2(lx, ly);
        // }

        sr.sprite.OverrideGeometry(vertices, triangles); // set the vertices and triangles

        // polygon.transform.position = (Vector2)transform.InverseTransformPoint(transform.position); // return to world space
    }

}
