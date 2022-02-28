using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject yuva;
    public GameObject dikCizgi;
    public GameObject yanCizgi;
    public GameObject YesilTas;
    public GameObject SariTas;

    private float yPiece = 0.05f;

    void Start()
    {
        Debug.Log("Hello World!");

        TaslariYerlestir();

        CizgileriVeYuvalariYerlestir();
    }

    private void TaslariYerlestir()
    {
        //Sari ve Yesil taslar yerlestirildi.
        for (int i = 0; i < 9; i++)
        {
            Instantiate(YesilTas, new Vector3(-12, yPiece, 6), transform.rotation);
            Instantiate(SariTas, new Vector3(-12, yPiece, -6), transform.rotation);
        }
    }

    private void CizgileriVeYuvalariYerlestir()
    {
        float k = 1.5f;

        for (int i = -9; i <= 9; i += 3)
        {
            for (int j = -9; j <= 9; j += 3)
            {
                //Yuvalar yerlestirildi.
                if(i != 0)
                {
                    if ((j == i || j == -i || j == 0))
                    {
                        Instantiate(yuva, new Vector3(i, yPiece, j), transform.rotation);
                    }
                }
                else
                {
                    if (j == 3 || j == 6 || j == 9 || j == -3 || j == -6 || j == -9)
                    {
                        Instantiate(yuva, new Vector3(i, yPiece, j), transform.rotation);
                    }
                }
                //Cizgiler Yerlestirildi.
                if(j != 9)
                {
                    if (i == -9 || i == 9)
                    {
                        Instantiate(dikCizgi, new Vector3(i, yPiece, j + k), transform.rotation);
                        Instantiate(yanCizgi, new Vector3(j + k, yPiece, i), transform.rotation);
                    }
                    else if (i == -6 || i == 6)
                    {
                        if (j + k == -4.5 || j + k == -1.5 || j + k == 1.5 || j + k == 4.5)
                        {
                            Instantiate(dikCizgi, new Vector3(i, yPiece, j + k), transform.rotation);
                            Instantiate(yanCizgi, new Vector3(j + k, yPiece, i), transform.rotation);
                        }
                    }
                    else if (i == -3 || i == 3)
                    {
                        if (j + k == -1.5 || j + k == 1.5)
                        {
                            Instantiate(dikCizgi, new Vector3(i, yPiece, j + k), transform.rotation);
                            Instantiate(yanCizgi, new Vector3(j + k, yPiece, i), transform.rotation);
                        }
                    }
                    else if (i == 0)
                    {
                        if (j + k == -7.5 || j + k == -4.5 || j + k == 4.5 || j + k == 7.5)
                        {
                            Instantiate(dikCizgi, new Vector3(i, yPiece, j + k), transform.rotation);
                            Instantiate(yanCizgi, new Vector3(j + k, yPiece, i), transform.rotation);
                        }
                    }
                }
            }
        }
    }
}
