using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScr : MonoBehaviour
{
    private float expoDelay = 1.5f,radius = 10f,force = 700f,grenadeDamage = 500;

    bool hasExplode = false;

    public GameObject expoEffect;

   void Update()
    {
        if (hasExplode)
            return;

        expoDelay -= Time.deltaTime;

        if(expoDelay <= 0f)
        {
            Explode();
        }
    }
    void Explode()
    {
      GameObject particle =  Instantiate(expoEffect, transform.position, transform.rotation);
        #region AddExpoForce
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in collidersToMove) {
        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force , transform.position,radius);
            }
        }
        #endregion
        #region DestroyNearObj
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToDestroy)
        {
                EnemyHealth enemyHP = nearbyObject.gameObject.GetComponent<EnemyHealth>();
                if (enemyHP != null)
                    enemyHP.TakeDamage(grenadeDamage);
        }
        #endregion
        hasExplode = true;
        Destroy(this.gameObject);
        Destroy(particle, 2f);
    }
}
