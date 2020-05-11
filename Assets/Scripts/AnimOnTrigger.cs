using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOnTrigger : MonoBehaviour
{
    [SerializeField] string animParameter;
    [SerializeField] Collider mCollider;
    Animator mAnimator;
    private int triggerCount = 0;
    private bool isLock = true;


    void Awake(){
        mAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other){
        Player p = other.GetComponent<Player>();
        if (p){
            isLock = !p.HaveKey;
            if (isLock) return;
        }
        mAnimator.SetBool(animParameter, true);
        SoundManager.instance.Play("DoorOpen");
        mCollider.enabled = false;
        triggerCount++;
    }

    private void OnTriggerExit(Collider other){
        Player p = other.GetComponent<Player>();
        if (p && isLock) return;
        triggerCount--;
        if (triggerCount == 0){
            SoundManager.instance.Play("DoorClose");
            mAnimator.SetBool(animParameter, false);
            mCollider.enabled = true;
        }
    }
}
