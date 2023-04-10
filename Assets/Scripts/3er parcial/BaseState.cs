using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{

    //Nmbre para identificar el estado
    //p.e. "Patrol", "Chase", "Atack", etc.
    public string name;
    public StateMachine stateMachine;

    //Constructos que toma el nombre del estado y una referencia a la maquina
    //de estados que va a ser su dueño

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public void PrintName()
    {
        Debug.Log("State name is: " + name);
    }

    //Estas funciones son virtual para que no sea indispensable implementarlas en
    //las clases que hereden de BaseState, pero pueden sustituirlas con un 'override'.

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void UpdateLogic() { }

    public virtual void UpdatePhysics() { }

}