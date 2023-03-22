//Crea el grid y el pathfinding en nuetra escena
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class PathFindingTest : MonoBehaviour
{
    [Header("Grid")]
    public int Height = 5;
    public int Width = 5;
    public float fTileSize = 10.0f;

    [Header("StartPoints&EndPoints")]
    public int Initx = 0;
    public int Inity = 0;
    public int Endx = 4;
    public int Endy = 4;

    public Transform t;
    //public Transform t;

    public int2 StartPosition = int2.zero;
    public int2 EndPosition = int2.zero;

    public GameObject go_InitialPoint;
    public GameObject go_EndPoint;
    public bool b_InitialPoint = false;
    public bool b_EndPoint = false;
    public bool b_Ready = false;
    public ClassGrid myTest;
    public List<Node> Pathfinding_result;
    public bool b_PathR = false;


    // Start is called before the first frame update
    private void Awake()
    {
        //Creamos el grid
        myTest = new ClassGrid(Height, Width, fTileSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
        }


        //Usamos una booleana para saber si los dos puntos ya fueron seleccionados
        if (b_InitialPoint == true && b_EndPoint == true)
            b_Ready = true;

        //Incializamos el pathfinding con barra espaciadora
        if (b_Ready == true && Input.GetKeyDown("space"))
        {
            GridTile s_InitialPoint = go_InitialPoint.GetComponent<GridTile>();
            GridTile s_EndPoint = go_EndPoint.GetComponent<GridTile>();
            Pathfinding_result = myTest.AStarSearch(s_InitialPoint.i_x, s_InitialPoint.i_y, s_EndPoint.i_x, s_EndPoint.i_y);
            List<Vector3> WorldPositionPathfinding = new List<Vector3>();

            foreach (Node n in Pathfinding_result)
            {
                WorldPositionPathfinding.Add(myTest.GetWorldPosition(n.x, n.y));
            }

            //Nuestro pathfinging esta listo
            b_PathR = true;
        }


    }

}