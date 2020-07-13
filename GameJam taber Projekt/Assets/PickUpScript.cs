using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    bool pickedUp = false;
    float value = 100;
    public float shrinkSpeed;
    public AudioSource pickUpSource;
    public Vector3 startScale;
    public ParticleSystem pickUpParticles;
    public BoxCollider coll;
    public GameObject obj;
    public Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        pickUpSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            value -= 3 * shrinkSpeed * Time.deltaTime;
        }
        else
        {
            value -= 1 * shrinkSpeed * Time.deltaTime;
        }
        transform.localScale = Vector3.Lerp(startScale, new Vector3(0.03f, 0.03f, 0.03f), (100-value) / 100);
        if (value <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void PickedUp()
    {
        pickedUp = true;
    }
    public void Dropped()
    {
        pickedUp = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!pickedUp)
        {
            if (collision.transform.tag == "Player")
            {
                Destroy(obj);
                Destroy(coll);
                Destroy(rig);
                pickUpParticles.Play();
                Debug.Log("Particles");
                pickedUp = true;
                pickUpSource.Play();
                GameObject.Find("GameController").GetComponent<GameController>().timeLeft+=value/10;
                Destroy(this.gameObject,0.45f);
            }
        }
    }
}
