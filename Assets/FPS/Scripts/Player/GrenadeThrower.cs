using TMPro;
using UnityEngine;
using Utilites;
using System.Collections;
public class GrenadeThrower : Singleton<GrenadeThrower>
{
    float throwForce = 15f,nextTimeTogrenade = 0 , fireRate = 100f;

    public GameObject grenadePref;

    private  Animator grenadeAnimator;

    private KeyCode throwGrenade = KeyCode.E;
  new  void Awake()
    {
        grenadeAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(throwGrenade) && Time.time >= nextTimeTogrenade)
        {
            nextTimeTogrenade = Time.time + fireRate;
            grenadeAnimator.SetBool("isThrow", true);
        }
    }
  public  IEnumerator TrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePref, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(fireRate - .1f);
        grenadeAnimator.SetBool("isThrow", false);
    }
}
