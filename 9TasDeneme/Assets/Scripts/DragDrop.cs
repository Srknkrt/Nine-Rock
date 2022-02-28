using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private GameObject selectedObject;

    private float yPiece = 0.05f;

    private void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = CastRay();

                if (selectedObject == null)
                {
                    if (hit.collider.CompareTag("Tas"))
                    {
                        selectedObject = hit.collider.gameObject;

                        Cursor.visible = false;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                selectedObject.transform.position = new Vector3(worldPosition.x, yPiece, worldPosition.z);

                selectedObject = null;
                Cursor.visible = true;
            }

            if (selectedObject != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
            }
        }
        catch (System.Exception)
        {
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

    //public void GecerliHareketleriOlustur()
    //{
    //    for (int i = -9; i <= 9; i += 3)
    //    {
    //        for (int j = -9; j <= 9; j += 3)
    //        {
    //            //GecerliHareketler yerlestirildi.
    //            if (i != 0)
    //            {
    //                if ((j == i || j == -i || j == 0))
    //                {
    //                    Instantiate(GecerliHareket, new Vector3(i, 0.13f, j), transform.rotation);
    //                }
    //            }
    //            else
    //            {
    //                if (j == 3 || j == 6 || j == 9 || j == -3 || j == -6 || j == -9)
    //                {
    //                    Instantiate(GecerliHareket, new Vector3(i, 0.13f, j), transform.rotation);
    //                }
    //            }
    //        }
    //    }
    //}

    //public void GecerliHareketSil()
    //{
    //    GameObject[] GecerliHareket = GameObject.FindGameObjectsWithTag("GecerliHareket");
    //    foreach (GameObject i in GecerliHareket)
    //    {
    //        GameObject.Destroy(i);
    //    }
    //}
}
