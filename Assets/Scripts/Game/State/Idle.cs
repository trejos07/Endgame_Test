using UnityEngine;

public class Idle : State
{
     Transform player;
    [SerializeField] float warnigd;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    public override void Execute()
    {
        float d = Vector3.Distance(transform.position, player.position);
        if (d <= warnigd)
        {
            SwitchToNextState();
        }
    }
}