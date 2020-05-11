using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AisPool : MonoBehaviour
{
    public static AisPool Instance;

    [SerializeField] private List<AICharacter> Ais;
    [SerializeField] int size;

    List<AICharacter> objects = new List<AICharacter>();
    Vector3 defPos = Vector3.one * 5000;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        for (int i = 0; i < size; i++)
        {
            objects.Add(RequireT());
        }
    }

    public AICharacter GetBulletAt(Vector3 pos, Quaternion rot)
    {
        if (objects.Count > 0)
        {
            AICharacter obj = objects[Random.Range(0,objects.Count)];
            obj.Agent.Warp(pos);
            objects.Remove(objects[objects.Count - 1]);
            obj.gameObject.SetActive(true);
            obj.Reset();
            

            return obj;
        }
        else
        {
            objects.Add(RequireT());
            return GetBulletAt(pos, rot);
        }


    }

    public void ReturnToPool(AICharacter ai)
    {
        objects.Add(ai);
        ai.transform.position = defPos;
        ai.gameObject.SetActive(false);

    }

    public virtual AICharacter RequireT()
    {
        AICharacter aI = Ais[Random.Range(0,Ais.Count)];
        return Instantiate<AICharacter>(aI, defPos, transform.rotation, transform);
    }


}
