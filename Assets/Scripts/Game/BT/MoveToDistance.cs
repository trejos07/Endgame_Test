using UnityEngine.AI;
using UnityEngine;

public class MoveToDistance : Task
{
    Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float stopDistance;

    protected override void Awake(){
        base.Awake();
        player = FindObjectOfType<Player>().transform;
    }

    public override bool Execute(){
        float d = Vector3.Distance(TargetAI.transform.position, player.position);
        targetAI.MAnimator.SetLayerWeight(1, 0);
        targetAI.MAnimator.SetFloat("moving", agent.velocity.magnitude);
        agent.SetDestination(player.position);
        if (d<=stopDistance && GetPathLength(agent.path)<= stopDistance && agent.path.corners.Length <=2){
            agent.isStopped = true;
            agent.updateRotation = false;
            return true;
        }else{
            targetAI.ShootFlashVFX.Stop();
            agent.isStopped = false;
            agent.updateRotation = true;
            return false;
        }
    }
    public float GetPathLength(NavMeshPath path){
        float lng = 0.0f;
        if (path.corners.Length > 0)
            lng += Vector3.Distance(transform.position, path.corners[0]);
        for (int i = 1; i < path.corners.Length; ++i){
            lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        return lng;
    }
}