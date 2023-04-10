using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{

    public PatrolState(StateMachine stateMachine) : base("Patrol", stateMachine)
    {
        //el ": base("Patrol", stateMachine)"
        // this.name = "Patrol";
        //this.StateMachine = stateMachine;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {

    }
}
public class AlertState : BaseState
{

    public AlertState(StateMachine stateMachine) : base("Alert", stateMachine)
    {
        //el ": base("Patrol", stateMachine)" equivale a escribir las siguientes lineas.
        // this.name = "Patrol";
        //this.StateMachine = stateMachine;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {

    }
}
public class AttackState : BaseState
{

    public AttackState(StateMachine stateMachine) : base("Attack", stateMachine)
    {
        //el ": base("Patrol", stateMachine)"
        // this.name = "Patrol";
        //this.StateMachine = stateMachine;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {

    }
}