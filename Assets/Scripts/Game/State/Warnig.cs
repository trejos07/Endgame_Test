using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warnig : State
{
     Transform player;
    [SerializeField] float warnigd;
    [SerializeField] float agrod;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    public override void Execute()
    {
        float d = Vector3.Distance(transform.position, player.position);
        if (d <= warnigd)
        {
            transform.LookAt(player.position);
            if (d <= agrod)
            {
                NextState = transform.GetComponent<RunBT>();
                SwitchToNextState();
            }

        }
        else
        {
            NextState = transform.GetComponent<Idle>();
            SwitchToNextState();
        }
    }
}
