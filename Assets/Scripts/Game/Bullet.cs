using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 10F;
    private Character instigator;
    private Rigidbody myRigidbody;
    [SerializeField] private ParticleSystem impactVFX;
    [SerializeField] private TrailRenderer trailVFX;


    public Character Instigator
    {
        get { return instigator; }
        set { instigator = value; }
    }

    public Rigidbody MyRigidbody { get => myRigidbody; set => myRigidbody = value; }

    public void Shoot(Character character)
    {
        trailVFX.enabled=true;
        trailVFX.Clear();
        Instigator = character;
        myRigidbody.AddForce(transform.forward * character.ShootForce, ForceMode.Impulse);
        Invoke("DestroyObject", 5F);
    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void DestroyObject()
    {
        trailVFX.enabled = false;
        trailVFX.Clear();

        Instantiate(impactVFX, transform.position, Quaternion.Euler(transform.eulerAngles+Vector3.up*180));
        BulletPool.Instance.ReturnToPool(this);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character")){
            Character character = other.gameObject.GetComponent<Character>();
            if (character != null && instigator != character){
                character.ModifyHP(damage * -1);
            }
        }
        DestroyObject();
    }
}