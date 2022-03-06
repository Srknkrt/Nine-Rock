using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public bool yuvaDoluMu;

    public GameObject gecerliHareket;

    public int[] yuvalar = new int[24];
    
    public List<GameObject> nodes = new List<GameObject>();


    private GameObject oyunNesnesi;

    GameRules()
    {

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GecerliHareketOlustur()
    {
        for (int i = -9; i <= 9; i += 3)
        {
            for (int j = -9; j <= 9; j += 3)
            {
                if (i != 0)
                {
                    if ((j == i || j == -i || j == 0))
                    {
                        Instantiate(gecerliHareket, new Vector3(i, 0.13f, j), Quaternion.identity);
                        //nodes.Add(gecerliHareket);
                    }
                }
                else
                {
                    if (j == 3 || j == 6 || j == 9 || j == -3 || j == -6 || j == -9)
                    {
                        Instantiate(gecerliHareket, new Vector3(i, 0.13f, j), Quaternion.identity);
                        //nodes.Add(gecerliHareket);
                    }
                }
            }
        }
    }

    public void GecerliHareketSil()
    {
        GameObject[] gecerliH = GameObject.FindGameObjectsWithTag("GecerliHareket");
        foreach (GameObject gH in gecerliH)
            GameObject.Destroy(gH);
        //nodes.Clear();
    }

    public void OyunNesnesiOlustur()
    {
        int k = 0;
        for (int i = -9; i <= 9; i += 3)
        {
            for (int j = -9; j <= 9; j += 3)
            {
                if (i != 0)
                {
                    if ((j == i || j == -i || j == 0))
                    {
                        oyunNesnesi = new GameObject();
                        oyunNesnesi.name = "oyunNesnesi " + k.ToString();
                        oyunNesnesi.transform.position = new Vector3(i, 0.13f, j);
                        nodes.Add(oyunNesnesi);
                        k++;
                    }
                }
                else
                {
                    if (j == 3 || j == 6 || j == 9 || j == -3 || j == -6 || j == -9)
                    {
                        oyunNesnesi = new GameObject();
                        oyunNesnesi.name = "oyunNesnesi " + k.ToString();
                        oyunNesnesi.transform.position = new Vector3(i, 0.13f, j);
                        nodes.Add(oyunNesnesi);
                        k++;
                    }
                }
            }
        }
    }
}
