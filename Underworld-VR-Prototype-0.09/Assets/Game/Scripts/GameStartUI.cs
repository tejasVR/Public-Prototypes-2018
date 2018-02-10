﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour {

    public SteamVR_TrackedObject trackedRight;
    public SteamVR_TrackedObject trackedLeft;

    private SteamVR_Controller.Device controllerRight;
    private SteamVR_Controller.Device controllerLeft;

    //public TMP_FontAsset[] fonts;

    public Color32[] textColor;

    private GameManager gameManager;

    private TextMeshPro text;

    //public GameObject interactionUI;

    private Renderer rend;

    public bool gameStart;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        //rend.material = mats[0];
        text = GetComponent<TextMeshPro>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        controllerRight = SteamVR_Controller.Input((int)trackedRight.index);
        controllerLeft = SteamVR_Controller.Input((int)trackedLeft.index);
    }

    private void OnTriggerStay(Collider other)
    {
        //rend.material = mats[1];

        text.color = new Color32(255, 255, 255, 255);

        if (controllerRight.GetPress(SteamVR_Controller.ButtonMask.Trigger) || controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
           gameStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!gameStart)
        {
            //rend.material = mats[0];

            text.color = new Color32(0, 0, 0, 255);

        }
    }
}
