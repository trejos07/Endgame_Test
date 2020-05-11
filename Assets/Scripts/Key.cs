using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] float hoverAmplitude=0.5f;
    [SerializeField] float hoverSpeed =0.5f;
    Coroutine anim;
    private void Start()
    {
        anim = StartCoroutine(hoverAnim());
    }
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p) { 
            p.HaveKey = true;
            StopCoroutine(anim);
            SoundManager.instance.Play("Key");
            Destroy(gameObject);
        }
    }

    IEnumerator hoverAnim(){
        while (true) {
            transform.position += Vector3.up *Mathf.Sin(Time.realtimeSinceStartup* hoverSpeed)*hoverAmplitude;
            yield return null;
        }
    }
}
