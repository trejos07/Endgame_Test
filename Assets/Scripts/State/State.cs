using UnityEngine;
using System;

public abstract class State : MonoBehaviour
{
    [SerializeField]
    private State nextState;

    public State NextState { get => nextState; set => nextState = value; }

    public abstract void Execute();

    public delegate void StateChange(State state);
    public event StateChange OnStateChange;

    public void SwitchToNextState()
    {
        if (nextState != null)
        {
            Toggle(false);
            nextState.Toggle(true);
        }
    }

    public void Toggle(bool val)
    {
        this.enabled = val;

        if (val)
        {
            if(OnStateChange!=null)
            {
                OnStateChange(this);
            }
        }
    }
}