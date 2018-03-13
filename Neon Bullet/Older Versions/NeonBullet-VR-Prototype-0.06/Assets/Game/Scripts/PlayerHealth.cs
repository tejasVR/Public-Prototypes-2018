﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private Player playerController;
    private Renderer rendHitbody;
    private Renderer rendPlatform;

    public float alphaHealth = 0; //current alpha representing health
    public float alphaHealthMax; //the maximum alpha value representing a full loss of health
    public float alphaHit = 0; // the alpha addition needed for when the player gets hit

    public float healthSmooth;

    public GameObject hitbodyProjection; //How the hologram will show

    public GameObject platformTriangle; // The platform triangle the player is on
    public GameObject[] belowTriangle1; // The first triangle under player
    public GameObject[] belowTriangle2;
    public GameObject[] belowTriangle3;
    public GameObject[] belowTriangle4;

    public Color[] triangleBlueColors;
    public Color[] triangleRedColors;

    public Color[] triangleCurrentColors;

    public Light bottomLight;

    public Material[] belowPlatformNeon;





    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        rendHitbody = hitbodyProjection.GetComponent<Renderer>();
        rendPlatform = platformTriangle.GetComponent<Renderer>();
        
    }
	
	// Update is called once per frame
	void Update () {

        healthSmooth = Mathf.Lerp(healthSmooth, playerController.playerHealth / playerController.playerHealthMax, Time.deltaTime * 3f);

        //var alphaPercent = 1 - (playerController.playerHealth / playerController.playerHealthMax);

        //alphaHealth = Mathf.Lerp(alphaHealth, alphaPercent, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHealth = Mathf.Lerp(alphaHealth, 0, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHit = Mathf.Lerp(alphaHit, 0, Time.unscaledDeltaTime * 3f);


        rendHitbody.material.SetFloat("_Alpha", alphaHealth);

        if (alphaHit <= 0)
        {
            alphaHit = 0;
        }

        //////////////// Watch out for performance issues //////////////////////////


        for (int i = 0; i < 4; i++)
        {
            triangleCurrentColors[i] = Color.Lerp(triangleRedColors[i], triangleBlueColors[i], healthSmooth - .15f);
            //triangleCurrentColors[i] = Color.Lerp(triangleBlueColors[i], triangleRedColors[i], healthPercent);
        }

        rendPlatform.material.SetColor("_MKGlowColor", triangleCurrentColors[0]);
        rendPlatform.material.SetColor("_MKGlowTexColor", triangleCurrentColors[0]);
        bottomLight.color = triangleCurrentColors[0];

        // Here we are directly accessing the materials of the triangle objects, and changing them permanently at runtime
        for (int i = 0; i < 4; i++)
        {
            belowPlatformNeon[i].SetColor("_MKGlowColor", triangleCurrentColors[i]);

        }

        /* ///Here we are changing the material properties of all the triangle parts individually at runtime
        for (int i = 1; i < 4; i++)
        {
            //belowPlatformNeon = GetComponent<Renderer>().materials;
            for (int j = 0; j < 3; j++)
            {
                belowTriangle1[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle1[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle2[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle2[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle3[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle3[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle4[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
            }
            
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            playerController.playerHealth -= 1;
            alphaHit = .1f;
            Destroy(other.gameObject);
        }
    }
}
