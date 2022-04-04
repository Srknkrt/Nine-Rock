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

    private int[,] yuvalarPos = new int[,] { { -9, -9 }, { -9, 0 }, { -9, 9 }, { 0, -9 }, { 0, 9 }, { 9, -9 }, { 9, 0 }, { 9, 9 },
                                          { -6, -6 }, { -6, 0 }, { -6, 6 }, { 0, -6 }, { 0, 6 }, { 6, -6 }, { 6, 0 }, { 6, 6 },
                                          { -3, -3 }, { -3, 0 }, { -3, 3 }, { 0, -3 }, { 0, 3 }, { 3, -3 }, { 3, 0 }, { 3, 3 } };

    private float[,] dikCizgilerPos = new float[,] { { -9, -7.5f }, { -9, -4.5f }, { -9, -1.5f }, { -9, 1.5f }, { -9, 4.5f }, { -9, 7.5f },
                                                  { -6, -4.5f }, { -6, -1.5f }, { -6, 1.5f }, { -6, 4.5f },
                                                  { -3, -1.5f }, { -3, 1.5f }, { 3, -1.5f }, { 3, 1.5f },
                                                  { 0, -7.5f }, { 0, -4.5f }, { 0, 4.5f }, { 0, 7.5f },
                                                  { 6, -4.5f }, { 6, -1.5f }, { 6, 1.5f }, { 6, 4.5f },
                                                  { 9, -7.5f }, { 9, -4.5f }, { 9, -1.5f }, { 9, 1.5f }, { 9, 4.5f }, { 9, 7.5f } };

    private float[,] yanCizgilerPos = new float[,] { { -7.5f, -9 }, { -4.5f, -9 }, { -1.5f, -9 }, { 1.5f, -9 }, { 4.5f, -9 }, { 7.5f, -9 },
                                                  { -4.5f, -6 }, { -1.5f, -6 }, { 1.5f, -6 }, { 4.5f, -6 },
                                                  { -1.5f, -3 }, { 1.5f, -3 }, { -1.5f, 3 }, { 1.5f, 3 },
                                                  { -7.5f, 0 }, { -4.5f, 0 }, { 4.5f, 0 }, { 7.5f, 0 },
                                                  { -4.5f, 6 }, { -1.5f, 6 }, { 1.5f, 6 }, { 4.5f, 6 },
                                                  { -7.5f, 9 }, { -4.5f, 9 }, { -1.5f, 9 }, { 1.5f, 9 }, { 4.5f, 9 }, { 7.5f, 9 } };

    void Start()
    {
        YuvalariYerlestir();
        CizgileriYerlestir();
        TaslariYerlestir();
    }

    private void YuvalariYerlestir()
    {
        for (int i = 0; i < yuvalarPos.GetLength(0); i++)
        {
            for (int j = 0; j < yuvalarPos.GetLength(1) - 1; j++)
            {
                Instantiate(yuva, new Vector3(yuvalarPos[i, j], yPiece, yuvalarPos[i, j + 1]), transform.rotation);
            }
        }
    }

    private void CizgileriYerlestir()
    {
        for (int i = 0; i < dikCizgilerPos.GetLength(0); i++)
        {
            for (int j = 0; j < dikCizgilerPos.GetLength(1) - 1; j++)
            {
                Instantiate(dikCizgi, new Vector3(dikCizgilerPos[i, j], yPiece, dikCizgilerPos[i, j + 1]), transform.rotation);
                Instantiate(yanCizgi, new Vector3(yanCizgilerPos[i, j], yPiece, yanCizgilerPos[i, j + 1]), transform.rotation);
            }
        }
    }

    private void TaslariYerlestir()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(YesilTas, new Vector3(-12, yPiece, (i * -2) + 8), transform.rotation);
            Instantiate(SariTas, new Vector3(12, yPiece, (i * -2) + 8), transform.rotation);
        }
    }
}
