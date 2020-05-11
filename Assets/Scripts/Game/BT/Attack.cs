using UnityEngine;

public class Attack : Task
{
    Transform player;
    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>().transform;
    }
    public override bool Execute()
    {
        Vector3 start = targetAI.transform.position + targetAI.transform.up*1.2f;
        Vector3 end = player.position + Vector3.up;
        Quaternion lookRot = Quaternion.LookRotation(end - start);
        targetAI.transform.rotation = Quaternion.Slerp(targetAI.transform.rotation, lookRot, targetAI.RotSpeed * Time.deltaTime);
        RaycastHit hit;
        Vector3 fwd = targetAI.transform.TransformDirection(Vector3.forward);
        if(Physics.SphereCast(start,0.1f, fwd, out hit, 10)){
            Debug.DrawRay(start, fwd * hit.distance, Color.green);
            if (hit.transform == player){
                targetAI.MAnimator.SetBool("shooting",true);
                targetAI.MAnimator.SetLayerWeight(1, 1);
                return true;
            }
        }
        targetAI.ShootFlashVFX.Stop();
        targetAI.MAnimator.SetBool("shooting", false);
        targetAI.MAnimator.SetLayerWeight(1, 0);
        return false;
    }
}