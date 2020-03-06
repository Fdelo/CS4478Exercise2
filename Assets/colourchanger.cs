using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class colourchanger : MonoBehaviour
{
  private SpriteRenderer sr;



  private void Awake() {
    sr = GetComponent<SpriteRenderer>();
  }

  private void OnCollisionEnter2D(Collision2D other) {
    sr.color = Random.ColorHSV();    
  }
}
