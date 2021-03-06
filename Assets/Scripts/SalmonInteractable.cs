﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonInteractable : MonoBehaviour
{
    public GameObject xrRig = null;
    public GameObject camera = null;
    public float lookAtTime = 2.0f;  // track for the lookat time
    private float lookAtCounter = 0.0f;

    private Vector3 salmonLocation;

    private float distanceX;
    private float distanceZ;
    private float distanceY;

    public float moveSpeed = 0.6f;

    public Texture2D texture;
    private bool GazeSuccess = false;
    // Start is called before the first frame update
    void Start()
    {
        salmonLocation = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(xrRig.transform.position);
        if (GazeSuccess)
        {
            distanceX = xrRig.transform.position.x - this.transform.position.x;
            distanceY = xrRig.transform.position.y - this.transform.position.y;
            distanceZ = xrRig.transform.position.z - this.transform.position.z;
            if (Vector3.Distance(xrRig.transform.position, salmonLocation) > 0.5f)
            {
                TriggerMove();
            }

        }

    }

    public virtual void OnSelectEnter()
    {
        lookAtCounter += Time.deltaTime;
        // should add the position comparison to make the player not moving again
        if (lookAtCounter > lookAtTime)
        {
            GazeSuccess = true;
            lookAtCounter = 0.0f;
        }
    }

    public virtual void OnSelectExit()
    {
            GazeSuccess = false;
            lookAtCounter = 0.0f;
    }



    private void TriggerMove()
    {
        xrRig.transform.position -= new Vector3(
            distanceX * Time.deltaTime * moveSpeed,
            distanceY * Time.deltaTime * moveSpeed,
            distanceZ * Time.deltaTime * moveSpeed);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }
}
