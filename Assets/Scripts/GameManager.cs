using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawners;
    [SerializeField] int maxAis;

    private GameManager instance;

    int ActiveAIs=0;

    public GameManager Instance { get => instance;}

    private void Start()
    {
        if (instance == null)
            instance = this;
        Invoke("SpawnAIs", 3);
    }

    void SpawnAIs()
    {
        for (int i = 0; i < maxAis; i++)
        {
            Transform t = spawners[Random.Range(0, spawners.Count)];
            AICharacter aI =  AisPool.Instance.GetBulletAt(t.position, t.rotation);
            aI.OnDeasth += ReadDeath;
            ActiveAIs++;
        }
        SoundManager.instance.Play("Spawn");
    }

    public void ReadDeath(AICharacter aI)
    {
        ActiveAIs--;
        aI.OnDeasth -= ReadDeath;
        if (ActiveAIs == 0){
            maxAis++;
            Invoke("SpawnAIs", 3);
        }
    }
}
