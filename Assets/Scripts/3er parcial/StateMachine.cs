using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    // Referencia al estado actual de la máquina.
    BaseState currentState;


    public void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }


    //Los estado de la máquina tienen dos updates, uno para la lógica de juego 
    //(UpdateLogic), y otro para las fisicas (UpdatePhysics), esto es para ser
    //congruentes con los update y fixedupdate de unity

    public void Update()
    {
        //si hay un estado actual(es decir, no es nulo)
        if (currentState != null)
        {
            //entonces, que actualice la logica de juego, inputs, y otras
            currentState.UpdateLogic();
        }
    }

    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }
    public void ChangeState(BaseState newState)
    {
        //primero que el estado actual haga la limpieza que requiera
        currentState.Exit();
        // despues asignamos el nuevo estado como el estado actual de la maquina
        currentState = newState;
        //finalmente que el nuevo estado haga las inicializaciones que requiera en su enter
        currentState.Enter();
    }


    //Como las maquinas de estados que vamos a usar deben heredar de esta clase y hacer un
    // override de esta funcion para inicial en su estado que deseen.
    //OJO: esta funcion es protected para que solo esta clase y sus hijas puedan accederla.

    protected virtual BaseState GetInitialState()
    {
        //por defecto regresa null
        return null;
    }

    private void OnGUI()
    {

        string text = currentState != null ? currentState.name : "No current State asigned";
        GUILayout.Label($"<size = 40 >{text}</size>");
    }
}
