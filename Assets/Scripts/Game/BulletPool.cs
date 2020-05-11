using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField]private Bullet bullet;
    [SerializeField]int size;

    List<Bullet> objects = new List<Bullet>();
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

    public Bullet GetAt(Vector3 pos, Quaternion rot)
    {
        if (objects.Count > 0)
          {
            Bullet obj = objects[0];
            obj.transform.position = pos;
            obj.transform.rotation =(rot);
            objects.Remove(objects[0]);
            return obj;
        }
        else
        {
            objects.Add(RequireT());
            return GetAt(pos, rot);
        }


    }

    public void ReturnToPool(Bullet bullet)
    {
        objects.Add(bullet);
        bullet.transform.position = defPos;
        bullet.transform.rotation = Quaternion.identity;
        bullet.MyRigidbody.velocity = Vector3.zero;
        bullet.Instigator = null;

    }

    public virtual Bullet RequireT()
    {
        return Instantiate<Bullet>(bullet, defPos, transform.rotation,transform);
    }




}
