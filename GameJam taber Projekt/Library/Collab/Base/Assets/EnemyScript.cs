using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    bool alive = true;
    public Rigidbody[] ragdollParts;
    public CapsuleCollider[] ragdollColl;
    public Animator ani;
    public BoxCollider boxColl;
    public float Vertical;
    public float Horizontal;
    public Rigidbody rig;
    public float moveSpeed;

    public AudioClip[] alienDeathSounds;
    public AudioClip[] alienSounds;
    AudioSource AlienAudioSource;

    public PlayerScript[] players;
    public float maxLookDistance;

    private float timer;
    private float timerLimit = 2;
    public float explosionForce;
    public Transform skeleton;

    private float randomV, randomH;

    public Transform positionOfCarriedStuff;

    public bool isCarryingSomething = false;
    public GameObject carriedObj;

    public float lifeTime;
    private float maxLifetime = 10;
    public Vector3 maxScale;

    private float notAliveTime;
    private float maxNotAliveTime = 40;
    private Vector3 maxAliveScaleAttained;
    void Start()
    {
        lifeTime = 0;
        transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        ani = GetComponent<Animator>();
        foreach (Rigidbody rag in ragdollParts)
        {
            rag.isKinematic = true;
        }
        foreach (CapsuleCollider rag in ragdollColl)
        {
            rag.enabled = false;
        }
        randomV = -Random.Range(0f, 1f);
        randomH = -Random.Range(0f, 1f);

        rig.AddExplosionForce(explosionForce, transform.position, 20, 10, ForceMode.Impulse);

        AlienAudioSource = GetComponent<AudioSource>();

        AlienAudioSource.PlayOneShot(alienSounds[3]);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            lifeTime = lifeTime < maxLifetime ? lifeTime += Time.deltaTime : maxLifetime;
            transform.LookAt(Vector3.zero, transform.up);
            rig.AddRelativeForce(new Vector3(Vertical + randomV, Horizontal + randomH, 0) * moveSpeed);

            //Debug.Log(timer);
            if (timer >= timerLimit)
            {
                Horizontal *= -1;
                //Debug.Log("Timer over on enemy");
                rig.AddExplosionForce(explosionForce, transform.position, 20, 10, ForceMode.Impulse);
            }
            timer = timer < timerLimit ? timer += Time.deltaTime : 0 + Random.Range(0f, 1f);
            transform.localScale = Vector3.Lerp(new Vector3(0.03f, 0.03f, 0.03f), maxScale, lifeTime / maxLifetime);
            maxAliveScaleAttained = transform.localScale;
        }

        if (!alive)
        {
            notAliveTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxAliveScaleAttained, new Vector3(0.03f, 0.03f, 0.03f), notAliveTime / maxNotAliveTime);
            if (notAliveTime / maxNotAliveTime >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collided");
        if (other.transform.CompareTag("Carryable") && lifeTime == maxLifetime)
        {
            if (isCarryingSomething)
            {
                return;
            }
            //Debug.Log("CARRYING SOMETHING");
            other.transform.SetParent(transform);
            other.transform.position = positionOfCarriedStuff.position;
            other.transform.GetComponent<RigidBodyGravity>().enabled = false;
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.GetComponent<Collider>().enabled = false;
            other.transform.GetComponent<PickUpScript>().PickedUp();

            isCarryingSomething = true;
            carriedObj = other.gameObject;

        }
    }

    public void Die()
    {
        AlienAudioSource.PlayOneShot(alienDeathSounds[Random.Range(0,2)]);

        Destroy(ani);
        Destroy(boxColl);
        alive = false;
        foreach (Rigidbody rag in ragdollParts)
        {
            rag.isKinematic = false;
        }
        foreach (CapsuleCollider rag in ragdollColl)
        {
            rag.enabled = true;
        }

        if (carriedObj == null)
        {
            return;
        }
        carriedObj.transform.SetParent(null);
        carriedObj.GetComponent<Rigidbody>().isKinematic = false;
        carriedObj.GetComponent<Collider>().enabled = true;
        carriedObj.GetComponent<RigidBodyGravity>().enabled = true;
        carriedObj.GetComponent<PickUpScript>().Dropped();
    }
}
