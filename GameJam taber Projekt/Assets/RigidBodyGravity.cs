using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyGravity : MonoBehaviour
{
    public float gravity=-20;
    public Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        if(rig==null)
         rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 grav = transform.position - new Vector3(0, 0, 0);
        rig.AddForce(grav * gravity * Time.deltaTime * Vector3.Distance(transform.position, new Vector3(0, 0, 0)));
        
    }
}
