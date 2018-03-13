﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public Player playerController; //Whole of player object
    private float enemyGiveHealth; //Amount of health enemy gives to player after it is destroyed
    public GameObject player; //cameraEye object

    private Vector3 playerDirection;

    public float enemyBulletFireRate; //bullets fires per second
    public float enemyBulletTimer;
    public float enemyBulletSpeed;

    public GameObject enemyBulletPrefab;
    public Transform enemyBulletSpawn;

    public Vector3 targetPosition;

    public Vector3 randomDirection;
    //public GameObject randomDir;

    private Ray ray;

    public float enemyMoveTimer; //time in seconds before enemy switches places

    private Vector3 velocity = Vector3.zero;

    public GameObject explosionPrefab;

    public float enemyAccuracy;

    public Vector3 enemyBulletDirection;

    public GameObject bulletDropExplosion;
    public GameObject bulletDrop;

    //public GameObject bulletPrefab;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyGiveHealth = enemyHealth;

        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f);

        playerDirection = enemyBulletDirection - transform.position;


        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        //Look at player
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2f * Time.deltaTime);

        if (enemyBulletTimer < enemyBulletFireRate)
        {
            enemyBulletTimer += Time.deltaTime;

        }
        else
        {
            enemyBulletTimer = enemyBulletFireRate;
            Fire();
        }

        enemyMoveTimer -= Time.deltaTime;
        if (enemyMoveTimer <= 0)
        {
            RandomPosition();
            enemyMoveTimer = Random.Range(5, 10);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            var damage = other.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //for (int i = 0; i < enemyGiveHealth; i++)
        {
           // Instantiate(bulletDrop, transform.position, transform.rotation);
        }

        //Instantiate(bulletDropExplosion, transform.position, transform.rotation);

        //playerController.playerHealth += enemyGiveHealth;

    }

    void Fire()
    {
        //Ray ray = new Ray(enemyBulletSpawn.transform.position, player.transform.position); //draws a ray before shooting from the bullet spawner to the player
        //float distanceToPlayer = Vector3.Distance(enemyBulletSpawn.transform.position, player.transform.position);
        //Vector3 gunDownSights = ray.GetPoint(distanceToPlayer); 

        Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
        enemyBulletDirection = player.transform.position + randomFire;
        //print(enemyBulletDirection);

        if (enemyBulletTimer == enemyBulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = enemyBulletSpawn.transform.forward * enemyBulletSpeed;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);

            enemyBulletTimer = 0;
        }
    }

    void RandomPosition()
    {
        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            randomDirection = new Vector3(Random.Range(-1f, -.5f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        } else
        {
            randomDirection = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        }
        ray = new Ray(playerController.transform.position, randomDirection);
        //randomDir.transform.position = randomDirection;
        targetPosition = ray.GetPoint(Random.Range(7f, 10f));
    }
}
