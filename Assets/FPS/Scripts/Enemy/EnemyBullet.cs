using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Range(0f, 100f)]
    public float enemyBulletSpeed;
    [Range(0f,100f)]
    public int damageToGive;

    private Transform player;

    private Vector3 target;

    public GameObject expoEffect;

    private float liveTime = 3;
     void Awake()
    {
        player = GameObject.FindGameObjectWithTag("FPPlayer").GetComponent<Transform>();

        float randomValY = Random.Range(0f, 2f);
        float randomValZ = Random.Range(0f, 2f);

        target = player.position;

        target += new Vector3(0, randomValY, randomValZ);
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target , enemyBulletSpeed * Time.deltaTime);

        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            GameObject currentExpo = Instantiate(expoEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject,0f);
            Destroy(currentExpo, 1f);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FPPlayer")
        {
       MovementScr takeDamage = collision.gameObject.GetComponent<MovementScr>();

            takeDamage.TakeDamage(damageToGive);

            Destroy(this.gameObject);
        }
    }
}
