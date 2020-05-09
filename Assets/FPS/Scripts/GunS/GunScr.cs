using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScr : MonoBehaviour
{
    #region Vars
    public TMP_Text tmpText;

  float   nextTimeToFire = 0f;

   public Camera fpsCamera;

    public ParticleSystem muzzelFlash;

    public GameObject impactEffect;
  

    [SerializeField]
  float impactForce = 1, range = 100f, fireRate = 1,useDelay = 0;

    [Range(0f,300f)]
    public float damage = 1;

    [Range(0f,100f)]
    public int currentAmmo;
    [Range(0,150)]
    public int limitAmmo;

    public bool isGun;

    #endregion
    public static GunScr instance;
    public static GunScr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;      
    }
    private void Update()
    {
        if (this.gameObject.transform.parent == null || currentAmmo < 0f)    
            return;

        if (tmpText != null)
        {
            tmpText.text = currentAmmo.ToString();
        }
        else
        {
            tmpText.text = null;
        }
        
     if(currentAmmo >= limitAmmo)
        {
            currentAmmo = limitAmmo;
        }

        if (Input.GetButton("Fire") && Time.time >= nextTimeToFire)
       {
                StartCoroutine(Shoot());

            if(isGun)
            currentAmmo--;
        }
    }
    #region Shoot
    IEnumerator Shoot() {
        nextTimeToFire = Time.time + fireRate;
        yield return new WaitForSeconds(useDelay);

        if (muzzelFlash != null)
        {
            muzzelFlash.Play();
        }



RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            EnemyHealth enemyHp = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHp != null)
            {
                enemyHp.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal* impactForce);
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
Destroy(impact, .5f);
        } 
        yield return new WaitForSeconds(fireRate - useDelay - 0.1f);

    }
    #endregion

}

