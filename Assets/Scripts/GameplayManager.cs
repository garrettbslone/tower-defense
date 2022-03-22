using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject[] waypoints;

    private int _coinPurse = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyKilled(EnemyController enemy)
    {
        _coinPurse += enemy.coins;
        Debug.Log($"Enemy killed. Coins: {_coinPurse}");
        Destroy(enemy.gameObject);
    }
}
