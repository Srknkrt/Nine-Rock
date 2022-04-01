using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    //public GameObject gecerliHareket;

    private GameObject selectedObject;
    private GameObject oldPos;

    //private float yTas = 0.13f;

    private float snapDistance = 1.0f;

    /*private int[,] nesnelerPos = new int[,] { { -9, -9 }, { -9, 0 }, { -9, 9 }, { 0, -9 }, { 0, 9 }, { 9, -9 }, { 9, 0 }, { 9, 9 },
                                          { -6, -6 }, { -6, 0 }, { -6, 6 }, { 0, -6 }, { 0, 6 }, { 6, -6 }, { 6, 0 }, { 6, 6 },
                                          { -3, -3 }, { -3, 0 }, { -3, 3 }, { 0, -3 }, { 0, 3 }, { 3, -3 }, { 3, 0 }, { 3, 3 } };
    private int[,] dogruPos = new int[,] { { -3, 0 }, { 0, 3 }, { 0, -3 }, { 3, 0 } };
    */

    List<GameObject> slotsPos = new List<GameObject>();
    List<GameObject> firstPlayerPiecesPos = new List<GameObject>();
    List<GameObject> secondPlayerPiecesPos = new List<GameObject>();

    List<Vector3> filledSlotsPos = new List<Vector3>();
    List<Vector3> emptySlotsPos = new List<Vector3>();

    private string nextPlayer;
    private int turnCount;


    private void Start()
    {
        oldPos = new GameObject("OldPos");
        nextPlayer = "FirstPlayer";
        turnCount = 0;

        SlotsScan();
    }

    private void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
                MouseClicked();
            if (Input.GetMouseButton(0))
                MouseHolded();
            if (Input.GetMouseButtonUp(0))
                MouseRelease();
        }
        catch (Exception) { }
    }

    private void MouseClicked()
    {
        PiecesScan();
        RaycastHit hit = CastRay();

        if (selectedObject == null)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag(nextPlayer))
                {
                    selectedObject = hit.collider.gameObject;
                    oldPos.transform.position = selectedObject.transform.position;

                    filledSlotsPos.Add(selectedObject.transform.position);

                    //GecerliHareketOlustur();
                    
                    //TasinYeriniBul(selectedObject);
                    
                    Cursor.visible = false;
                }
            }
        }
    }

    private void MouseHolded()
    {
        Vector3 worldPosition;

        if (selectedObject != null)
        {
            worldPosition = FindPosition();

            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
        }
    }

    private void MouseRelease()
    {
        if (selectedObject != null)
        {
            CalculateDistance();

            //selectedObject = MesafeHesapla(selectedObject);
            selectedObject = null;

            //GecerliHareketSil();

            PiecesPosAndSlotsPosClear();

            Cursor.visible = true;
        }
    }

    private void CalculateDistance()
    {
        float distance;
        bool isTruePos = false;
        bool isEmpty = true;
        GameObject obj = null;

        

        FindFulledSlots();

        FindEmptySlots();

        foreach (GameObject slot in slotsPos)
        {
            distance = Vector3.Distance(selectedObject.transform.position, slot.transform.position);
            if(distance <= snapDistance)
            {
                obj = slot;

                isTruePos = true;

                if (turnCount >= firstPlayerPiecesPos.Count + secondPlayerPiecesPos.Count)
                {
                    isTruePos = false;
                }
               

                foreach (Vector3 filledSlotPos in filledSlotsPos)
                {
                    if (slot.transform.position == filledSlotPos)
                    {
                        isEmpty = false;
                    }
                }
            }
        }

        if (isTruePos && isEmpty && obj != null)
        {
            selectedObject.transform.position = obj.transform.position;
            FindNextPlayer();
            turnCount++;
        }
        else
        {
            selectedObject.transform.position = oldPos.transform.position;
        }
    }

    private void FindFulledSlots()
    {
        for (int i = 0; i < slotsPos.Count; i++)
        {
            for (int j = 0; j < firstPlayerPiecesPos.Count; j++)
            {
                if(slotsPos[i].transform.position == firstPlayerPiecesPos[j].transform.position)
                {
                    filledSlotsPos.Add(slotsPos[i].transform.position);
                }
            }
            for (int j = 0; j < secondPlayerPiecesPos.Count; j++)
            {
                if(slotsPos[i].transform.position == secondPlayerPiecesPos[j].transform.position)
                {
                    filledSlotsPos.Add(slotsPos[i].transform.position);
                }
            }

        }
    }

    private void FindEmptySlots()
    {
        foreach (GameObject slot in slotsPos)
        {
            foreach (Vector3 filledSlotPos in filledSlotsPos)
            {
                if(slot.transform.position != filledSlotPos)
                {
                    emptySlotsPos.Add(slot.transform.position);
                }
            }
        }
    }

    private void SlotsScan()
    {
        slotsPos.AddRange(GameObject.FindGameObjectsWithTag("Slot"));
    }

    private void PiecesScan()
    {
        firstPlayerPiecesPos.AddRange(GameObject.FindGameObjectsWithTag("FirstPlayer"));
        secondPlayerPiecesPos.AddRange(GameObject.FindGameObjectsWithTag("SecondPlayer"));
    }

    private void PiecesPosAndSlotsPosClear()
    {
        firstPlayerPiecesPos.Clear();
        secondPlayerPiecesPos.Clear();
        filledSlotsPos.Clear();
    }

    private void FindNextPlayer()
    {
        nextPlayer = (nextPlayer == "FirstPlayer") ? "SecondPlayer" : "FirstPlayer";
    }

    /*
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
        else if(kenardakiTasSayisi > 0)
        {

            TaslariTara();

            for (int i = 0; i < bosYuvalarinKonumu.Length; i++)
            {
                Instantiate(gecerliHareket, new Vector3(bosYuvalarinKonumu[i].transform.position.x,
                                                        yTas,
                                                        bosYuvalarinKonumu[i].transform.position.z), Quaternion.identity);
                oyunNesnesi = new GameObject("OyunNesnesi " + i);
                oyunNesnesi.tag = "oyunNesnesi";
                oyunNesnesi.transform.position = bosYuvalarinKonumu[i].transform.position;
                nesneler.Add(oyunNesnesi);
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
    */
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
