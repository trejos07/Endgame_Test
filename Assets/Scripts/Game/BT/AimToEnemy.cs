using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToEnemy : Task
{
    Transform player;
    protected override void Awake(){
        base.Awake();
        player = FindObjectOfType<Player>().transform;
    }
    public override bool Execute(){
        Vector3 start = targetAI.transform.position + Vector3.up;
        Vector3 end = player.position + Vector3.up;
        Quaternion lookRot = Quaternion.LookRotation(end -start);
        targetAI.transform.rotation = Quaternion.Slerp(targetAI.transform.rotation,lookRot,targetAI.RotSpeed*Time.deltaTime);
        return true;
    }
}
