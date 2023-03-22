//Guarda cada nodo de nuestro grid, nos ayuda a detectar nuestro punto de inicio y final
//usando clic izquierdo y derecho
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    // Start is called before the first frame update

    //Referenciamos el codigo pathfinding
    public PathFindingTest PathFinding;
    //Coordenada de x, y
    public int i_x;
    public int i_y;
    //Booleanas que nos indican si ya se eligio el punto de inicio y final
    public bool b_initPoint;
    public bool b_endPoint;
    public TextMesh text;
    void Start()
    {
        PathFinding = FindObjectOfType<PathFindingTest>();
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detecta si el mouse esta sobre nuestro objeto
    private void OnMouseOver()
    {
        if (PathFinding.b_InitialPoint == false)
        {
            //Clic izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                //Si se elige el punto de inicio y final en el mismo nodo se reemplazan
                if (b_endPoint == true)
                {
                    PathFinding.go_EndPoint = null;
                    PathFinding.b_EndPoint = false;
                    b_endPoint = false;
                }
                //Guarda el nodo como punto inicial y activa la booleana
                Debug.Log("Funciona 1");
                PathFinding.go_InitialPoint = gameObject;
                PathFinding.b_InitialPoint = true;
                b_initPoint = true;

            }
        }



        if (PathFinding.b_EndPoint == false)
        {
            //Clic derecho
            if (Input.GetMouseButtonDown(1))
            {
                //Si se elige el punto de inicio y final en el mismo nodo se reemplazan
                if (b_initPoint == true)
                {
                    PathFinding.go_InitialPoint = null;
                    PathFinding.b_InitialPoint = false;
                    b_initPoint = false;
                }
                //Guarda el nodo como punto inicial y activa la booleana
                Debug.Log("Funciona 2");
                PathFinding.go_EndPoint = gameObject;
                PathFinding.b_EndPoint = true;
                b_endPoint = true;
            }
        }
    }

    //Consigue las coordenadas
    public void GetIndex(int y, int x)
    {
        i_x = x;
        i_y = y;
    }

    //Usamos gizmos para la informacion visual
    private void OnDrawGizmos()
    {
        //Creamos una esfera verde para el punto de inicio
        if (b_initPoint == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1);
        }
        //Creamos esfera roja para el punto final
        if (b_endPoint == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
