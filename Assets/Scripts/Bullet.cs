using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;

    // private GameObject dir;
    
    void Update()
    {
        // var position = transform.position;
        //
        // var x = -Math.Sign(position.x - dir.transform.position.x) * speed + position.x;
        // var z = -Math.Sign(position.z - dir.transform.position.z) * speed + position.z;
        //
        // transform.position = new Vector3(x, position.y, z);
        //
        // if ((transform.position - dir.transform.position).sqrMagnitude <= 10f)
        // {
        //     dir.GetComponent<Enemy>().Damage();
        //     Destroy(gameObject);
        // }
    }

    public void Fire(Vector3 e)
    {
        transform.position = e;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Enemy")) return;
    //     other.GetComponent<Enemy>().Damage();
    //     Destroy(gameObject);
    // }
}
