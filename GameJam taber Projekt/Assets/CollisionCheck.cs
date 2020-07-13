using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  <><       {}
public class CollisionCheck : MonoBehaviour
{
    public Rigidbody rig;

    Vector3 hitPosition;
    public float moveDistance;
    public ParticleSystem impact;
    public ParticleSystem trail;
    public ParticleSystem smoke;

    public bool hit = false;

    public float radius = 5.0F;
    public float power = 10.0F;
    
    public float maximumSpeed;

    public AudioSource impactAud;
    public float damage = 100;
    public GameObject light;

    public CameraShaker cameraShaker1;
    public CameraShaker cameraShaker2;

    void Start()
    {
        
        rig = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        float curSpeed = Vector3.Magnitude(rig.velocity);  // test current object speed

        if (curSpeed > maximumSpeed)

        {
            float brakeSpeed = curSpeed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rig.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rig.AddForce(-brakeVelocity);  // apply opposing brake force
        }

        if (hit)
        {
            

            if (Vector3.Distance(hitPosition, transform.position) >= moveDistance)
            {
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                foreach (Collider hit in colliders)
                {
                    if (hit.GetComponent<Rigidbody>())
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        Debug.Log(rb.gameObject.name);
                        if (rb != null)
                        {
                            float dam = Mathf.Clamp(Vector3.Distance(transform.position, rb.transform.position), 0, radius);
                            dam = dam / radius;
                            dam = 1 - dam;
                            rb.AddExplosionForce(power, explosionPos, radius, 30.0F);
                            if (rb.transform.tag == "Player")
                            {
                                Debug.Log(GetComponent<PlayerScript>());
                                rb.transform.GetComponent<PlayerScript>().TakeDamage(damage * dam);
                            }
                            else if (rb.transform.tag == "Enemy")
                            {
                                rb.transform.root.GetComponent<EnemyScript>().Die();
                            }
                        }
                    }
                }
                GetComponent<Rigidbody>().isKinematic = true;
                impactAud.Play();
                Destroy(this);
            }

            
        }
    

    transform.LookAt(new Vector3(0, 0, 0), transform.up);
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
            if (other.transform.tag == "Ground")
            {
                //If the GameObject's name matches the one you suggest, output this message in the console
                Debug.Log("Do something´, when landing on ground");

                StartCoroutine(cameraShaker1.Shake(.5f, .4f));
                StartCoroutine(cameraShaker2.Shake(.5f, .4f));

                hitPosition = transform.position;
                hit = true;
                trail.Stop();
                impact.Play();
                smoke.Play();
                Destroy(light);
                GetComponent<SphereCollider>().isTrigger = false;
            }
            if (other.transform.tag == "Player")
            {
                Debug.Log("Do something´, when hitting player");
            }

        }
    }
}
