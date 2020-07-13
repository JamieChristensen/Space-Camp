using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] Meteor;

    public float distance;
    public float max;
    public float min;

    public CameraShaker camShake1;
    public CameraShaker camShake2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForSpawn()
    {
        float wait = Random.Range(min, max);
        yield return new WaitForSeconds(wait);
        Vector3 pos = Random.onUnitSphere * distance;
        CollisionCheck coll = Instantiate(Meteor[Random.Range(0, Meteor.Length)], pos, Quaternion.identity).GetComponent<CollisionCheck>();
        coll.cameraShaker1 = camShake1;
        coll.cameraShaker2 = camShake2;
        StartCoroutine(WaitForSpawn());
    }

   
}
