using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDrop : MonoBehaviour
{
    private Vector3 RightPosition;

    public GameObject GecerliHareket;

    public bool InRightPosition;
    public bool Selected;

    private Vector3 a = new Vector3(0, 0.05f, 0);

    private void Start()
    {
        RightPosition = a;
    }

    private void Update()
    {
        if(Vector3.Distance(GecerliHareket.transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                a = RightPosition;
                InRightPosition = true;
            }
        }
    }
}
