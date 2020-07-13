using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rig;
    public float gravity;
    public float speed;
    public float maximumSpeed;
    public ParticleSystem eksPar;
    public ParticleSystem trailPar;
    public GameObject obj;
    public bool hit = false;

    public float radius = 5.0F;
    public float power = 10.0F;
    public float damage;
    public AudioSource aud;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.AddRelativeForce(new Vector3(1, 0, 0) * -500);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            Vector3 grav = transform.position - new Vector3(0, 0, 0);
            rig.AddForce(grav * gravity * Time.deltaTime * Vector3.Distance(transform.position, new Vector3(0, 0, 0)));
            rig.AddRelativeForce(new Vector3(1, 0, 0) * -speed * Time.deltaTime);
            //transform.LookAt(new Vector3(0, 0, 0), transform.up);

            float curSpeed = Vector3.Magnitude(rig.velocity);  // test current object speed

            if (speed > maximumSpeed)

            {
                float brakeSpeed = curSpeed - maximumSpeed;  // calculate the speed decrease

                Vector3 normalisedVelocity = rig.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                rig.AddForce(-brakeVelocity);  // apply opposing brake force
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Ground")
        {
            Destroy(light, 0.05f);
            hit = true;
            trailPar.Stop();
            eksPar.Play();
            Destroy(obj);
            Destroy(rig);
            aud.Play();
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        float dam = Mathf.Clamp(Vector3.Distance(transform.position, rb.transform.position), 0, radius);
                        dam = dam / radius;
                        dam = 1 - dam;
                        rb.AddExplosionForce(power, explosionPos, radius, 30.0F);
                        if (rb.transform.tag == "Player")
                        {
                            rb.transform.GetComponent<PlayerScript>().TakeDamage(damage*dam);
                        }else if (rb.transform.tag == "Enemy")
                        {
                            rb.transform.root.GetComponent<EnemyScript>().Die();
                        }
                    }
                }
            }
            Destroy(this.gameObject, 1f);
        }
    }
    
}
