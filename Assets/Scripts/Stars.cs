using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{


    public int count = 50;
    public float min = -10f;
    public float max = 50f;

    public GameObject prefab;

    // Update is called once per frame
    void Start()
    {
        for (int i = 0; i < count; i++){
            float x = Random.Range(min, max);
            float y = Random.Range(min, max);
            float size = Random.Range (1.25f, 8.5f);
            GameObject child = Instantiate(prefab, new Vector3(x, y, -0.5f), Quaternion.identity);
            child.transform.parent = transform;
            child.transform.localScale = new Vector3(size, size, 1f);
        }
    }

}
