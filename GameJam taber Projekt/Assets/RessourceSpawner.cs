using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceSpawner : MonoBehaviour
{

    public CollisionCheck coll;
    public GameObject Prefab;
    public int spawnCount;
    bool opened = false;
    public GameObject met;
    public SphereCollider spherecoll;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!opened && coll.hit)
        {
            opened = true;
            StartCoroutine(Spawn());
            Destroy(spherecoll);
            Destroy(met);
        }
    }

    IEnumerator Spawn()
    {
        
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(Prefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
