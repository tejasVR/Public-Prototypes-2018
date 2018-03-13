﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public UpgradeManager upgradeManager;
    public Player playerController;
    public PlayerShield playerShield;
    public WeaponActive weaponActive;

    public float upgradeCost;

    // Player Health Upgrades
    public float addPlayerHealth;
    public float addPlayerHealthMaxMultiplier;
    public float addPlayerHealthRegenMultiplier;

    // Player Attack Upgrades
    public float addBulletFireRateMultiplier;
    public float addBulletDamageMultiplier;
    //public float addBulletSpeedMultiplier; <--- No one really know why we should have this
    public float addBulletAccuracyMultiplier;
    public float addBulletCapacity;
    public float addNoUseBulletChance;
    public float addBulletCriticalHitChance;
    public float addBulletRegeneration;


    // Player Defense Upgrades
    public float addShieldRegenerationMultiplier;
    public float addShieldHealthMaxMultiplier;
    public float addShieldAbsorbtionChance;
    public float addShieldSizeMultipler;

    // Enemy Upgrades
    public float addEnemyAdditionalBullets;
    public float addEnemyNegativeHealthMultiplier;

    // Weapon Upgrades
    public bool unlockRifle;
    public bool unlockShotgun;
    public bool unlockSaberSword;
    public bool unlockLaserRifle;

    private void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {
        //upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        //playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        //playerShield = GameObject.Find("PlayerShield").GetComponent<PlayerShield>();
    }

    private void OnEnable()
    {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        playerShield = GameObject.FindGameObjectWithTag("Shield").GetComponent<PlayerShield>();
        weaponActive = GameObject.FindGameObjectWithTag("WeaponHand").GetComponent<WeaponActive>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet" && !upgradeManager.upgradeSelected)
        {
            if (playerController.playerBullets >= upgradeCost)
            {
                playerController.playerBullets -= upgradeCost;
                AddUpgradeEffect();
                upgradeManager.upgradeSelected = true;
            }            
        }

        Destroy(other.gameObject);
    }

    void AddUpgradeEffect()
    {
        // Attack Effects
        playerController.bulletFireRateMultiplier += addBulletFireRateMultiplier;
        playerController.bulletDamageMultiplier += addBulletDamageMultiplier;
        playerController.bulletAccuracyMultiplier += addBulletAccuracyMultiplier;
        playerController.playerBulletCapacity += addBulletCapacity;
        playerController.playerNoUseBulletChance += addNoUseBulletChance;
        playerController.playerBulletCriticalHitChance += addBulletCriticalHitChance;

        // Player Health Effects
        playerController.playerHealthMaxMultiplier += addPlayerHealthMaxMultiplier;
        playerController.playerHealth += addPlayerHealth;
        playerController.playerHealthRegenMultiplier += addPlayerHealthRegenMultiplier;


        // Player Defense Effects
        playerShield.shieldRechargeSpeedMultiplier += addShieldRegenerationMultiplier;
        playerShield.shieldHealthMaxMultiplier += addShieldHealthMaxMultiplier;
        playerShield.shieldAbsorbtionChance += addShieldAbsorbtionChance;
        playerShield.shieldSizeMultiplier += addShieldSizeMultipler;

        // Enemy Effects
        playerController.enemyGiveAdditionalBullets += addEnemyAdditionalBullets;
        playerController.enemyNegativeHealthMultiplier += addEnemyNegativeHealthMultiplier;

        if (unlockRifle)
        {
            weaponActive.WeaponToActivate("RIFLE");
        }
        else if(unlockShotgun)
        {
            weaponActive.WeaponToActivate("SHOTGUN");
        }
        else if (unlockSaberSword)
        {
            weaponActive.WeaponToActivate("SABER SWORD");
        }
        else if (unlockLaserRifle)
        {
            weaponActive.WeaponToActivate("LASER RIFLE");
        }



    }
}
