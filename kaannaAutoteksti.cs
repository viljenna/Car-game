using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaannaAutoteksti : MonoBehaviour
{
    void Update()
    {
        //Tutkitaan onko main kamera olemassa
        if (Camera.main != null)
        {
            //Käännetään teksti kohtisuoraan main kameraan
            this.GetComponent<Transform>().LookAt(Camera.main.GetComponent<Transform>().position);
            this.GetComponent<Transform>().Rotate(0f, 180f, 0f);
        }//if
    } //update
}// class
