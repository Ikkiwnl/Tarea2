//Controla a nuestro agente y le heredamos steering behaviors
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class AStarAgent : NewSteeringBehaviors
{
    //Usamos una booleana para saber si nuestro agente ya fue seleccionado
    public bool Selected = false;
    //Color del agente apra informacion visual
    private SpriteRenderer color;

    //Usamos una lsita para nuestro camino
    public List<Vector3> Path = null;
    //El nodo actual
    int i_CurrentWaypoint = 0;

    public PathFindingTest Pathfinding;
    ClassGrid s_Grid;

    public float f_NearArea;

    void Start()
    {
        color = GetComponent<SpriteRenderer>();
        s_Grid = Pathfinding.myTest;
    }

   
    void Update()
    {
        //Una vez que los puntos de inicio y final este puestos comenzamos a buscar el camino
        if (Pathfinding.b_PathR == true)
        {
            Path = s_Grid.ConvertBacktrackToWorldPos(Pathfinding.Pathfinding_result);
            Pathfinding.b_PathR = false;

        }

    }


    private void FixedUpdate()
    {
        Vector3 v3SteeringForce = Vector3.zero;

        if (Path != null && Selected == true)
        {
            float f_Distance = (Path[i_CurrentWaypoint] - transform.position).magnitude;
            Debug.Log("fDistance to Point is: " + f_Distance);

            if (f_NearArea > f_Distance && i_CurrentWaypoint != Path.Count - 1)
            {

                i_CurrentWaypoint++;
                i_CurrentWaypoint = math.min(i_CurrentWaypoint, Path.Count - 1);
            }

            v3SteeringForce = i_CurrentWaypoint == Path.Count - 1 ? Seek(Path[i_CurrentWaypoint]) : Arrive(Path[i_CurrentWaypoint]);


            r_myRigidbody.AddForce(v3SteeringForce, ForceMode.Acceleration);  //Aceleración ignora la masa

            //Clamp es para que no exceda la velocidad máxima
            r_myRigidbody.velocity = Vector3.ClampMagnitude(r_myRigidbody.velocity, f_maxSpeed);




        }
    }



    private void OnMouseOver()
    {

        //Clic izquierdo para seleccionar agente
        if (Input.GetMouseButtonDown(0))
        {
            Selected = true;
            Debug.Log("A*Agent is now Selected");
            color.color = Color.cyan;
            r_myRigidbody.isKinematic = false;
        }

        //Clic derecho para deseleccionar a nuestro agente
        if (Input.GetMouseButtonDown(1))
        {
            Selected = false;
            Debug.Log("A*Agent isnt selected");
            color.color = Color.white;
            r_myRigidbody.isKinematic = true;
        }


    }


}