using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    // private float delay = 2f;
    SpriteRenderer ren;

    float count = 0;

    float speed = 1f;

    void Start() {
       ren = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        speed = Random.Range(0.5f, 2.5f);

        count += Time.deltaTime;
        float col = Mathf.Sin (count) * 75 + 125;
        Debug.Log (col + " | " + count + " | " + speed);
        ren.color = new Color (1, 1, 1, col/255);
        
    }

}
