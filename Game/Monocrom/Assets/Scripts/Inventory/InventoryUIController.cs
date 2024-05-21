using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject Cristal;
    public GameObject Nucleo;
    public GameObject Insiginea;
    public GameObject Eterna;
    public GameObject Arma;
    public GameObject Alinhamento;
    public GameObject Inventory;
    public GameObject Fe;
    public GameObject MainHUD;

    public void Open()
    {
        MainHUD.SetActive(true);
        OpenCristal();
    }
    public void Close()
    {
        MainHUD.SetActive(false);
    }
    public void OpenCristal()
    {
        Cristal.SetActive(true);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenNucleo()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(true);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenInsiginea()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(true);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenEterna()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(true);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenArma()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(true);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenAlinhamento()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(true);
        Inventory.SetActive(false);
        Fe.SetActive(false);
    }

    public void OpenInventory()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(true);
        Fe.SetActive(false);
    }

    public void OpenFe()
    {
        Cristal.SetActive(false);
        Nucleo.SetActive(false);
        Insiginea.SetActive(false);
        Eterna.SetActive(false);
        Arma.SetActive(false);
        Alinhamento.SetActive(false);
        Inventory.SetActive(false);
        Fe.SetActive(true);
    }


}
