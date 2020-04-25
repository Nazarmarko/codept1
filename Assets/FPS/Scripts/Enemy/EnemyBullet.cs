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

        target = new Vector3(player.position.x, player.position.y,
            player.position.z);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, enemyBulletSpeed * Time.deltaTime);

        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            DestroyObject(this.gameObject,0f);
        }
      if (transform.position == target)
        {
           GameObject currentExpo = Instantiate(expoEffect, transform.position, Quaternion.identity);
            DestroyObject(this.gameObject,0f);
            DestroyObject(currentExpo, 1f);
        }
    }
    public void DestroyObject(GameObject destroyableObj,float timeForDestroy)
    {
        Destroy(destroyableObj, timeForDestroy);
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
