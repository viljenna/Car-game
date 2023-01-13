using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tämä tarvitaan verkkoa varten
using UnityEngine.Networking;

public class autokoodi2 : NetworkBehaviour
{
    // Oman auton malli
    public int automalli = 1;
    //Oma viesti
    public string viesti = "";

    private int laskuri = 0;


    void Start()
    {
        //Jos paikallisen pelaajan auto
        if (isLocalPlayer)
        {
            //Kytketään paikalliselle pelaajalle oman auton ohjaus päälle
            this.GetComponent<autokoodi1>().enabled = true;

            //Laitetaan kamera seuraamaan paikallisen pelaajan autoa
            this.GetComponentInChildren<Camera>().enabled = true;

            //Siirretään paikallisen pelaajan tekstiä
            this.GetComponent<Transform>().GetChild(4).GetComponent<Transform>().Translate(8f, 2f, 0f);

        }//if
    }// start

    private void OnGUI()
    {
        //Luodaan paikalliselle pelaajalle tekstikenttä tietojen välitystä varten
        if (isLocalPlayer)
        {
            this.viesti = GUI.TextField(new Rect(25f, Screen.height - 40f, 200f, 30f), this.viesti);

        }//if
    }//GUI

    //Lähetetään tieto serverille
    [Command]
    public void Cmd_vaihdaAuto(int uusimalli)
    {
        this.Rpc_LahetaTietoMuille(uusimalli);
    }// Cmd_vaihdaAuto

    //Kaiutetaan tieto muillekin
    [ClientRpc]
    public void Rpc_LahetaTietoMuille(int uusimalli)
    {
        this.automalli = uusimalli;
    }// Rpc_LahetaTietoMuille

    //Lähetetään tieto serverille
    [Command]
    public void Cmd_vaihdaViesti(string uusiviesti)
    {
        this.viesti = uusiviesti;
        this.Rpc_LahetaViestiMuille(uusiviesti);
    }// Cmd_vaihdaViesti

    //Kaiutetaan tieto muillekin
    [ClientRpc]
    public void Rpc_LahetaViestiMuille(string uusiviesti)
    {
        this.viesti = uusiviesti;
    }// Rpc_LahetaViestiMuille




    void Update()
    {
        //Automallin vaihto V-näppäimellä
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (this.automalli < 3)
                {
                    this.automalli++;
                }
                else
                {
                    this.automalli = 1;
                }// if

                //Lähetetään tieto muille
                this.Cmd_vaihdaAuto(this.automalli);
            }// if

            //Päivitetään auton tietoja muille
            if (this.laskuri < 30)
            {
                this.laskuri++;
            } else
            {
                this.Cmd_vaihdaAuto(this.automalli);
                this.Cmd_vaihdaViesti(this.viesti);
                this.laskuri = 0;
            }

        }// if

        //Pidetään auton teksti ajantasalla
        this.GetComponent<Transform>().GetChild(4).GetComponent<TextMesh>().text = this.viesti;

        //Pidetään automalli ajantasalla
        if (this.automalli == 1)
        {
            this.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
            this.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            this.GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
        }
        else if (this.automalli == 2)
        {
            this.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            this.GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
            this.GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
        }
        else if (this.automalli == 3)
        {
            this.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            this.GetComponent<Transform>().GetChild(1).gameObject.SetActive(false);
            this.GetComponent<Transform>().GetChild(2).gameObject.SetActive(true);
        } //if

    }//update
} //class
