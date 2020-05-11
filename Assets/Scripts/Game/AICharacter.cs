using UnityEngine;
using UnityEngine.AI;
using System;


public class AICharacter : Character
{
    
    private State currentState;
    [SerializeField] State start;
    [SerializeField] NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; set => agent = value; }

    public delegate void colliding(Collision collision);
    public event colliding OnCollision;

    public delegate void Death(AICharacter aI);
    public event Death OnDeasth;

    protected override void Start()
    {
        base.Start();
        mRenderer.material.SetColor("_Color1", new Color(1, .15f, 0));
        currentState = start;
        State[] states = GetComponents<State>();
        ShootFlashVFX.Stop();
        foreach (State s in states)
        {
            s.OnStateChange += SetNewState;
        }
    }

    public void SetNewState(State newState)
    {
        currentState = newState;
    }

    public void Reset(){
        Start();
    }

    protected override void OnDeath()
    {
        if (OnDeasth != null)
            OnDeasth(this);
        AisPool.Instance.ReturnToPool(this);
    }

    private void Update()
    {
        currentState.Execute();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (OnCollision != null)
            OnCollision(collision);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var path = agent.path;
        if(path.corners.Length>0)
            Gizmos.DrawLine(transform.position, path.corners[0]);
        for (int i = 1; i < path.corners.Length; i++)
        {
            Gizmos.DrawLine(path.corners[i-1], path.corners[i]);
        }
    }
}