using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autokoodi1 : MonoBehaviour
{
    
    void Update()
    {
        //Ohjataan autoa nuolinäppäimillä
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.GetComponent<Transform>().Rotate(0f, -50f * Time.deltaTime, 0f);
        }// if

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.GetComponent<Transform>().Rotate(0f, 50f * Time.deltaTime, 0f);
        }// if

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<Transform>().Translate(0f, 0f, 50f * Time.deltaTime);
        }// if

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.GetComponent<Transform>().Translate(0f, 0f, -30f * Time.deltaTime);
        }// if

    }// Update
}// class
