using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Range(0f,1000f)]
    public float health = 200f;
    public GameObject particle;
    Animator anim;
    public void TakeDamage(float amount)
    {
        health -= amount;
        //anim.SetTrigger("Hurt");
        if (health <= 0f)
            Die();
    }
    private void Die()
    {
        Debug.Log("dIE");
        Destroy(this.gameObject);
        GameObject particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particleObj, 2f);
    }
}
