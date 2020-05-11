using UnityEngine;

public abstract class Composite : RootChild
{
    [SerializeField]
    private RootChild[] children;

    public override void SetTargetAI(AICharacter target)
    {
        base.SetTargetAI(target);

        foreach (Node node in children)
        {
            node.SetTargetAI(TargetAI);
        }
    }

    protected abstract bool ValueToBreak { get; }

    public override bool Execute()
    {
        bool result = ValueToBreak;

        foreach (Node node in children)
        {
            result = node.Execute();

            if (result == ValueToBreak)
            {
                break;
            }
        }
        return result;
    }
}