using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAgentFSM : StateMachine
{

    [HideInInspector]
    public PatrolState patrolState;

    [HideInInspector]
    public AlertState alertState;

    [HideInInspector]
    public AttackState attackState;


    public Vector3 v3AgentPosition;
    public Rigidbody rbAgentRigidBody;

    //Posicion donde el agente va a estar parado mientras esta en el estado del patrol
    //y a la cual debe regresar en el estado de alert para volver al estado de patrol
    public Vector3 v3AgentPatrollingPosition;

    //Cono de vision
    //Variables que necesitamos para definir el cono de vision
    public float fVisionDist = 10.0f;
    [Range(0.0f, 360.0f)]
    public float fVisionAngle = 90.0f;

    //Hacia donde esta viendo el agente patrullero
    public Vector3 v3AgentFacingDirection;

    public Vector3 v3TargetPosition;

    public bool CheckFOV(out Vector3 v3TargetPos)
    {
        v3TargetPos = Vector3.zero;
        //Comprobacion de dos chequeos, uno similar al chequeo del area del circulo
        // otro que es respecto al angulo del circulo
       

        float fAgentToTargetDist = (v3AgentPosition - v3TargetPosition).magnitude;
        if (fAgentToTargetDist > fVisionDist)
        {
            return true;
        }



        return true;
    }





    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);
    }

    protected override BaseState GetInitialState()
    {
        //Segun definimos para este agente, el primer estado debe ser el patrullar
        return patrolState;
    }

}
