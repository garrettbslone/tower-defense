using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int coins = 10;
    public float health = 3f, speed = 3f;

    public delegate void EnemyDeath(EnemyController enemy);
    public event EnemyDeath OnEnemyDeath;

    private Animator _animator;
    private Camera _camera;
    private GameplayManager _manager;
    private int _nextWaypoint = 0;
    private bool _running = true;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();

        transform.position = _manager.waypoints[_nextWaypoint++].transform.position;
        OnEnemyDeath += _manager.EnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_running)
            return;
        
        if (health <= 0f)
        {
            OnEnemyDeath?.Invoke(this);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    health -= 1f;
                    Debug.Log($"Enemy hit health {health}/3");
                }
            }
        }
        
        Vector3 targetPos = _manager.waypoints[_nextWaypoint].transform.position;
        Vector3 movementDir = (targetPos - transform.position).normalized;
        Vector3  target = targetPos - transform.position;
        
        transform.position += movementDir * speed * Time.deltaTime;
        
        Vector3 newPos = targetPos - transform.position;

        if (Vector3.Dot(target, newPos) <= 0)
        {
            transform.position = _manager.waypoints[_nextWaypoint].transform.position;

            if (++_nextWaypoint == _manager.waypoints.Length)
            {
                _running = false;
            }
        } 
    }
}
