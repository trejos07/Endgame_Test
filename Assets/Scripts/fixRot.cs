using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixRot : MonoBehaviour
{
    Quaternion initRot;
    void Awake(){
        initRot = transform.rotation;
    }

    void LateUpdate(){
        transform.rotation = initRot;
    }
}
