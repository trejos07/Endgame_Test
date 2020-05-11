using UnityEngine;

public abstract class Node : MonoBehaviour
{
    protected AICharacter targetAI;

    protected virtual void Awake()
    {
        targetAI =  transform.parent.GetComponent<AICharacter>();
    }

    public AICharacter TargetAI
    {
        get { return targetAI; }
        protected set { targetAI = value; }
    }

    public virtual void SetTargetAI(AICharacter target)
    {
        if (TargetAI == null)
        {
            TargetAI = target;
        }
    }

    public abstract bool Execute();
}