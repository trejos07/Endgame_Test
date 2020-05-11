using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 5F;
    [SerializeField]private float maxHP;
    [SerializeField]private float shootForce = 20F;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private ParticleSystem shootFlashVFX;

    private float hp;
    protected Rigidbody mRigidbody;
    protected Animator mAnimator;
    protected Renderer mRenderer;
    public event Action<float> OnHealthChange;
    public event Action OnDeathEvent;

    public float HP{
        get { return hp; }
    }

    public float ShootForce { get { return shootForce; } }
    public Animator MAnimator { get => mAnimator; set => mAnimator = value; }
    public Rigidbody MRigidbody { get => mRigidbody; set => mRigidbody = value; }
    public float RotSpeed { get => rotSpeed; set => rotSpeed = value; }
    public ParticleSystem ShootFlashVFX { get => shootFlashVFX; set => shootFlashVFX = value; }

    public void ModifyHP(float delta){
        hp += delta;
        if (hp <= 0F){
            hp = 0;
            OnDeath();
        }
        if(delta!= maxHP)
            SoundManager.instance.Replay("Damage");
        OnHealthChange?.Invoke(hp/maxHP);
    }

    protected virtual void OnDeath(){
        Destroy(gameObject);
        OnDeathEvent?.Invoke();
    }

    protected virtual void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mAnimator = GetComponentInChildren<Animator>();
        mRenderer = GetComponentInChildren<Renderer>();
    }
    protected virtual void Start(){
        hp = 0;
        ModifyHP(maxHP);
    }
    public void SpawnBullet(){
        if (bulletSpawnPosition != null){
            Quaternion rot = Quaternion.LookRotation(bulletSpawnPosition.forward);
            BulletPool.Instance.GetAt(bulletSpawnPosition.position, bulletSpawnPosition.rotation).Shoot(this);
            SoundManager.instance.Replay("Shoot");
            SoundManager.instance.Replay("Bullet");
            if (!ShootFlashVFX.isPlaying) 
                ShootFlashVFX.Play();
        }
    }
    public void StepSound() {
        SoundManager.instance.Replay("Steps");
    }
}