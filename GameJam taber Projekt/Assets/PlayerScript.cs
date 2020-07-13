using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public OrbitScript OrbitCamera;

    public ParticleSystem death;
    public bool Alive=true;
    public int PlayerNr;
    private float Vertical;
    private float Horizontal;
    public Rigidbody rig;
    public float gravity;
    public float moveSpeed;
    public float rotSpeed;
    public float recoil;
    public GameObject Bullet;
    public Transform bulletSpawn;
    public AudioSource aud;
    public AudioClip[] audClip;
    public float health;
    public float timeSinceShot;
    public float timeBetweenShots;
    public Text helthText;
    public Text deathText;
    public AudioClip playerDeathSound;
    // Start is called before the first frame update
    void Start()
    {
        rig= GetComponent<Rigidbody>();
        helthText.text = "" + health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 grav = transform.position - new Vector3(0, 0, 0);
        rig.AddForce(grav * gravity * Time.deltaTime * Vector3.Distance(transform.position, new Vector3(0, 0, 0)));

        if (Alive)
        {

            timeSinceShot += Time.deltaTime;
            Vertical = Input.GetAxis("VerticalPlayer" + PlayerNr);
            Horizontal = Input.GetAxis("HorizontalPlayer" + PlayerNr);


            if (Vertical != 0)
            {
                rig.AddRelativeForce(new Vector3(Vertical, 0,-0.8f) * moveSpeed*30*Time.deltaTime);
            }

            transform.LookAt(new Vector3(0, 0, 0), transform.up);
            transform.Rotate(0, 0, -Horizontal * rotSpeed * Time.deltaTime);
            if (timeSinceShot >= timeBetweenShots)
            {
                if (Input.GetButtonDown("Fire" + PlayerNr))
                {
                    Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation);
                    rig.AddRelativeForce(new Vector3(-1, 0, 0) * recoil, ForceMode.Impulse);
                    aud.pitch = Random.Range(0.6f, 1.4f);
                    aud.PlayOneShot(audClip[0]);
                    timeSinceShot = 0;
                }
            }

        }
    }
    public void TakeDamage(float dam)
    {
        if (Alive)
        {
            health -= dam;
            if (health < 0)
            {
                health = 0;
            }
            helthText.text = "" + Mathf.Floor(health);
            if (health <= 0)
            {
                Alive = false;
                death.Play();

                aud.PlayOneShot(playerDeathSound);

                //OrbitCamera.orbiting = true;
                deathText.enabled = true;
                //OrbitCamera.transform.parent = null;


                //Destroy(this.gameObject);
            }
        }
    }
}
