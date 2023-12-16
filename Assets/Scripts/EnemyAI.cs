using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : StateManager<EnemyAI.EnemyState>
{
    public enum EnemyState 
    {
        WANDER,
        CHASE,
        INVESTIGATE
    }

    private Vector2 lastKnownPlayerPosition;

    void Awake()
    {
        States.Add(EnemyState.WANDER, new WanderState());
        States.Add(EnemyState.CHASE, new ChaseState());
        States.Add(EnemyState.INVESTIGATE, new InvestigateState());

        // Set initial state
        CurrentState = States[EnemyState.WANDER];
        CurrentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {   
        // Check for player detection and transition states accordingly
        if (PlayerDetected())
        {
            TransitionToState(EnemyState.CHASE);
        }
        else if (CurrentState.StateKey == EnemyState.CHASE && !PlayerDetected())
        {
            TransitionToState(EnemyState.INVESTIGATE);
        }
        else if (CurrentState.StateKey == EnemyState.INVESTIGATE && PlayerDetected())
        {
            TransitionToState(EnemyState.CHASE);
        }
        else if (CurrentState.StateKey == EnemyState.INVESTIGATE && ReachedLastKnownPosition())
        {
            TransitionToState(EnemyState.WANDER);
        }
        UpdateDirectorValues();
    }

    

    void UpdateDirectorValues()
    {
        Director.EnemyPosition.Set(this.transform.position.x, this.transform.position.y);
    }

    bool ReachedLastKnownPosition()
    {
        return false; 
    }

    bool PlayerDetected()
    {
        return (
            Director.PlayerPosition.x < (Director.EnemyPosition.x + 8) &&
            Director.PlayerPosition.x > (Director.EnemyPosition.x - 8) &&
            Director.PlayerPosition.y < (Director.EnemyPosition.y + 8) &&
            Director.PlayerPosition.y > (Director.EnemyPosition.y - 8));
    }
}

public class WanderState : BaseState<EnemyAI.EnemyState>
{

    public WanderState() : base(EnemyAI.EnemyState.WANDER) { }

    public override void EnterState()
    {
        Debug.Log("Wandering!!");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override EnemyAI.EnemyState GetNextState()
    {
        if (PlayerDetected())
        {
            return EnemyAI.EnemyState.CHASE;
        }

        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    private bool PlayerDetected()
    {
        return (
            Director.PlayerPosition.x < (Director.EnemyPosition.x + 8) &&
            Director.PlayerPosition.x > (Director.EnemyPosition.x - 8) &&
            Director.PlayerPosition.y < (Director.EnemyPosition.y + 8) &&
            Director.PlayerPosition.y > (Director.EnemyPosition.y - 8));
    }
}

public class ChaseState : BaseState<EnemyAI.EnemyState>
{
    public ChaseState() : base(EnemyAI.EnemyState.CHASE) { }

    public override void EnterState()
    {
            Debug.Log("Chasing!!");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override EnemyAI.EnemyState GetNextState()
    {
        if (!PlayerDetected())
        {
            return EnemyAI.EnemyState.INVESTIGATE;
        }

        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    private bool PlayerDetected()
    {
        return (
            Director.PlayerPosition.x < (Director.EnemyPosition.x + 8) &&
            Director.PlayerPosition.x > (Director.EnemyPosition.x - 8) &&
            Director.PlayerPosition.y < (Director.EnemyPosition.y + 8) &&
            Director.PlayerPosition.y > (Director.EnemyPosition.y - 8));
    }
}

public class InvestigateState : BaseState<EnemyAI.EnemyState>
{

    public InvestigateState() : base(EnemyAI.EnemyState.INVESTIGATE) { }

    public override void EnterState()
    {
            Debug.Log("Investigating!!");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override EnemyAI.EnemyState GetNextState()
    {
        if (PlayerDetected())
        {
            return EnemyAI.EnemyState.CHASE;
        }
        else //check if its on last known tile position
        {
            return EnemyAI.EnemyState.WANDER;
        }

        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    private bool PlayerDetected()
    {
        return (
            Director.PlayerPosition.x < (Director.EnemyPosition.x + 8) &&
            Director.PlayerPosition.x > (Director.EnemyPosition.x - 8) &&
            Director.PlayerPosition.y < (Director.EnemyPosition.y + 8) &&
            Director.PlayerPosition.y > (Director.EnemyPosition.y - 8));
    }
}


