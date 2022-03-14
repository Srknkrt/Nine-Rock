using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    public GameObject gecerliHareket;

    private GameObject selectedObject;
    private GameObject oldPos;

    private List<GameObject> nesneler = new List<GameObject>();
    private GameObject oyunNesnesi;

    private float yTas = 0.13f;
    private float snapDistance = 1.0f;
    
    private bool[] yuvalar;
    private int indis;

    private int[,] nesnelerPos = new int[,] { { -9, -9 }, { -9, 0 }, { -9, 9 }, { 0, -9 }, { 0, 9 }, { 9, -9 }, { 9, 0 }, { 9, 9 },
                                          { -6, -6 }, { -6, 0 }, { -6, 6 }, { 0, -6 }, { 0, 6 }, { 6, -6 }, { 6, 0 }, { 6, 6 },
                                          { -3, -3 }, { -3, 0 }, { -3, 3 }, { 0, -3 }, { 0, 3 }, { 3, -3 }, { 3, 0 }, { 3, 3 } };
    private int[,] dogruPos = new int[,] { { -3, 0 }, { 0, 3 }, { 0, -3 }, { 3, 0 } };

    private string siradakiOyuncu;
    private int birinciOyuncuTasSayisi = 9;
    private int ikinciOyuncuTasSayisi = 9;
    private int kenardakiTasSayisi;

    private void Start()
    {
        kenardakiTasSayisi = birinciOyuncuTasSayisi + ikinciOyuncuTasSayisi;
        oldPos = new GameObject("OldPos");
        yuvalar = new bool[24];
        siradakiOyuncu = "YesilTas";
    }

    private void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
                MouseTiklandiginda();
            if (Input.GetMouseButton(0))
                MouseBasiliTutuldugunda();
            if (Input.GetMouseButtonUp(0))
                MouseTikiBirakildiginda();
        }
        catch (Exception) { }
    }

    private void MouseTiklandiginda()
    {
        RaycastHit hit = CastRay();

        if (selectedObject == null)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag(siradakiOyuncu))
                {
                    GecerliHareketOlustur();
                    
                    selectedObject = hit.collider.gameObject;
                    oldPos.transform.position = selectedObject.transform.position;
                    
                    TasinYeriniBul(selectedObject);

                    Cursor.visible = false;
                }
            }
        }
    }

    private void MouseBasiliTutuldugunda()
    {
        Vector3 worldPosition;

        if (selectedObject != null)
        {
            worldPosition = FindPosition();

            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
        }
    }

    private void MouseTikiBirakildiginda()
    {
        if (selectedObject != null)
        {
            selectedObject = MesafeHesapla(selectedObject);
            selectedObject = null;
            
            GecerliHareketSil();

            Cursor.visible = true;
        }
    }

    private void TasinYeriniBul(GameObject selectedObj)
    {
        float mesafe;
        foreach (GameObject nesne in nesneler)
        {
            mesafe = Vector3.Distance(selectedObj.transform.position, nesne.transform.position);
            if (mesafe <= snapDistance)
            {
                indis = IndisBul(nesne);
            }
        }
    }

    private GameObject MesafeHesapla(GameObject selectedObj)
    {
        bool truePos = false;
        bool doluMu = false;
        GameObject n = null;
        float mesafe;
        foreach (GameObject nesne in nesneler)
        {
            mesafe = Vector3.Distance(selectedObj.transform.position, nesne.transform.position);
            if (mesafe <= snapDistance)
            {
                if (yuvalar[IndisBul(nesne)])
                {
                    doluMu = true;
                }
                else
                {
                    n = nesne;
                    yuvalar[indis] = false;
                    yuvalar[IndisBul(nesne)] = true;
                    truePos = true;
                }
            }
        }
        if (!truePos || doluMu)
        {
            selectedObj.transform.position = oldPos.transform.position;
        }
        else
        {
            SiradakiOyuncuyuBul();
            selectedObj.transform.position = n.transform.position;
            kenardakiTasSayisi -= 1;
        }
        return selectedObj;
    }

    private void SiradakiOyuncuyuBul()
    {
        if (siradakiOyuncu == "YesilTas")
            siradakiOyuncu = "SariTas";
        else if (siradakiOyuncu == "SariTas")
            siradakiOyuncu = "YesilTas";
    }

    private int IndisBul(GameObject nesne)
    {
        string[] indis = nesne.name.Split(' ');
        return Convert.ToInt32(indis[1]);
    }

    private void GecerliHareketOlustur()
    {
        if(kenardakiTasSayisi == 0)
        {
        }
        else
        {
            for (int i = 0; i < nesnelerPos.GetLength(0); i++)
            {
                for (int j = 0; j < nesnelerPos.GetLength(1) - 1; j++)
                {
                    Instantiate(gecerliHareket, new Vector3(nesnelerPos[i, j], yTas, nesnelerPos[i, j + 1]), transform.rotation);
                    oyunNesnesi = new GameObject("OyunNesnesi " + j + i);
                    oyunNesnesi.tag = "oyunNesnesi";
                    oyunNesnesi.transform.position = new Vector3(nesnelerPos[i, j], yTas, nesnelerPos[i, j + 1]);
                    nesneler.Add(oyunNesnesi);
                }
            }
        }
    }

    private void GecerliHareketSil()
    {
        GameObject[] gecerliH = GameObject.FindGameObjectsWithTag("GecerliHareket");
        foreach (GameObject gH in gecerliH)
            GameObject.Destroy(gH);
        GameObject[] oyunN = GameObject.FindGameObjectsWithTag("oyunNesnesi");
        foreach (GameObject oN in oyunN)
            GameObject.Destroy(oN);
        nesneler.Clear();
    }

    private Vector3 FindPosition()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, 
                                       Input.mousePosition.y, 
                                       Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        return worldPosition;
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x,
                                                Input.mousePosition.y,
                                                Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x,
                                                 Input.mousePosition.y,
                                                 Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
