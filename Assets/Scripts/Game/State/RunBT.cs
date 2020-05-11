using UnityEngine;

public class RunBT : State
{
    [SerializeField] protected Root btRoot;
    [SerializeField] float agrod;
    private Transform player;
    private bool execute =true;


    private void Awake() {
        Player p = FindObjectOfType<Player>();
        player = p.transform;
        p.OnDeathEvent += () => execute=false;
    }

    public override void Execute(){
        if (execute){
            float d = Vector3.Distance(transform.position, player.position);
            if (d <= agrod){
                if (btRoot != null){
                    btRoot.Execute();
                }
            }
            else{
                SwitchToNextState();
            }
        }
    }
}