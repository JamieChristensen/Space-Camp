using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public float speed;
    public bool orbiting;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (orbiting)
        {
            transform.RotateAround(Vector3.zero, transform.forward, speed * Time.deltaTime);
        }
    }
}
