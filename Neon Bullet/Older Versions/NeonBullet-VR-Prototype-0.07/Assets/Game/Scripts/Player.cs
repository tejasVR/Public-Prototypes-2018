﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameManager gameManager;

    // Player Basic Variables
    public float playerBullets;
    public string bulletString;

    public float playerHealth;
    public float playerHealthMax;

    public float playerRedemptionHealth;


    public float enemyGiveAdditionalBullets;
    public float enemyNegativeHealthMultiplier;

    //public float playerRedemptionHealth

    // Player Health Multipliers
    public float playerHealthMaxMultiplier = 1;
    public float playerHealthRegenMultiplier = 0;

    // Player Attack Multipliers
    public float bulletFireRateMultiplier = 1;
    public float bulletDamageMultiplier = 1;
    //public float bulletSpeedMultiplier = 1;
    public float bulletAccuracyMultiplier = 1;
    public float playerBulletCapacity = 200;
    public float playerNoUseBulletChance = 0;
    public float playerBulletCriticalHitChance = 0;
    public float playerBulletRegeneration = 0;


    public TextMeshPro[] bulletCounters;

    //public WeaponActive weaponActive; // Keeps track of the current weapons that the player is using




	void Start ()
    {
        foreach (TextMeshPro bulletCounter in bulletCounters)
        {
            bulletCounter.text = "" + playerBullets.ToString();
        }
        bulletString = playerBullets.ToString();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update ()
    {
        if (bulletString != playerBullets.ToString())
        {
            foreach (TextMeshPro bulletCounter in bulletCounters)
            {
                bulletCounter.text = "" + playerBullets.ToString();                
            }
        }

        if (playerHealth <= 0)
        {
            //gameManager.redemptionActive = true;
            //weaponActive.WeaponToActivate("SABER SWORD"); // Active saber sword for redemption mode
        }

        if (gameManager.roundActive)
        {
            playerHealth += Time.deltaTime * playerHealthRegenMultiplier;
            playerBullets += Mathf.RoundToInt(Time.deltaTime * playerBulletRegeneration);


            if (playerHealth >= (playerHealthMax * playerHealthMaxMultiplier))
            {
                playerHealth = playerHealthMax * playerHealthMaxMultiplier;
            }


        }

        if (playerBullets >= playerBulletCapacity)
        {
            playerBullets = playerBulletCapacity;
        }
    }
}
