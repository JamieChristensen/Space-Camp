using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        if (vec == Vector3.zero)
        {
            vec = new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vec);
    }
}
