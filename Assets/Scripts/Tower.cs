using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 250f;
    public GameObject bullet;

    private GameplayManager _manager;
    private float cdt = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cdt > 0f)
        {
            cdt -= Time.deltaTime;
            return;
        }
        
        foreach (var e in _manager.enemies)
        {
            float m = (transform.position - e.transform.position).magnitude;
            if (m <= range)
            {
                var b = Instantiate(bullet, transform.position, Quaternion.identity);
                b.GetComponent<Bullet>().Fire(e.transform.position);
                e.Damage();
                Destroy(b, 1f);
                cdt = 1f;
            }
        }
    }
}
