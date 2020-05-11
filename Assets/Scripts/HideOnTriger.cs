using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTriger : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1;
    [SerializeField] private List<SpriteRenderer> toHideObjs = new List<SpriteRenderer>();
    //[SerializeField] private List<Collider> onTrigerColliders;
    private Collider mCollider;
    private List<Coroutine> fadeCoruutines = new List<Coroutine>();
    private int triggerCount = 0;

    void Awake() {
    }

    private void OnTriggerEnter(Collider other) {
        triggerCount++;
        StopFadeObjects();
        FadeObjects(0);
    }
    private void OnTriggerExit(Collider other){
        triggerCount--;
        if (triggerCount == 0){
            StopFadeObjects();
            FadeObjects(1);
        }
    }

    //private void SwitchCollidersState(bool state){
    //    for (int i = 0; i < onTrigerColliders.Count; i++){
    //        onTrigerColliders[i].enabled = state;
    //    }
    //}
    private void FadeObjects(float alpha){
        for (int i = 0; i < toHideObjs.Count; i++){
            fadeCoruutines.Add(StartCoroutine(SmoothFade(toHideObjs[i], alpha)));
        }
    }
    private void StopFadeObjects(){
        for (int i = 0; i < fadeCoruutines.Count; i++){
            if (fadeCoruutines[i] != null){
                StopCoroutine(fadeCoruutines[i]);
            }
        }
        fadeCoruutines.Clear();
    }

    IEnumerator SmoothFade(SpriteRenderer mRenderer, float alpha){
        float t = 0;
        Color c = mRenderer.color;
        Color targetColor = new Color(c.r, c.g, c.b, alpha);
        while (Mathf.Abs(alpha - mRenderer.color.a) >= 0.005f){
            t += Time.deltaTime;
            mRenderer.color = Color.Lerp(c, targetColor, fadeSpeed * t);
            yield return null;
        }
    }
}
