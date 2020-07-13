using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnScript : MonoBehaviour
{
    public CollisionCheck coll;
    public GameObject alienPrefab;
    public Animator ani;
    public int spawnCount;
    bool opened = false;
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
        }
    }

    IEnumerator Spawn()
    {
        ani.Play("Open");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(alienPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
