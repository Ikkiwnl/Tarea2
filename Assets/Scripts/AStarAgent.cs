using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarAgent : MonoBehaviour
{
    public bool Selected = false;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        Selected = true;
        Debug.Log("Click");
    }


}
