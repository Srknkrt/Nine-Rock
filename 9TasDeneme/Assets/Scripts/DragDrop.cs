using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private GameObject selectedObject;

    private GameObject oldObj;

    private float snapDistance = 1;

    private List<GameObject> oyunNesnesi;

    public GameObject gameRules;

    void Start()
    {
        oldObj = new GameObject();
        oyunNesnesi = gameRules.GetComponent<GameRules>().nodes;
        gameRules.GetComponent<GameRules>().OyunNesnesiOlustur();
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
                if (hit.collider.CompareTag("Tas"))
                {
                    gameRules.GetComponent<GameRules>().GecerliHareketOlustur();
                    
                    selectedObject = hit.collider.gameObject;
                    oldObj.transform.position = selectedObject.transform.position;
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
        string[] yuvaIndisi;

        bool truePos = false;

        float mesafe;

        if (selectedObject != null)
        {
            foreach (GameObject nesne in oyunNesnesi)
            {
                mesafe = Vector3.Distance(selectedObject.transform.position, nesne.transform.position);
                if (mesafe <= snapDistance)
                {
                    selectedObject.transform.position = nesne.transform.position;

                    yuvaIndisi = nesne.name.Split(' ');
                    gameRules.GetComponent<GameRules>().yuvalar[Convert.ToInt32(yuvaIndisi[1])] = 1;

                    truePos = true;
                }
            }
            if (!truePos)
            {
                selectedObject.transform.position = oldObj.transform.position;
            }

            gameRules.GetComponent<GameRules>().GecerliHareketSil();
            selectedObject = null;
            Cursor.visible = true;
        }
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
