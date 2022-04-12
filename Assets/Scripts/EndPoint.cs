using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int health = 1000;
    public Transform healthBar;

    private float _healthPerUnit;
    private GameplayManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _healthPerUnit = 1000f / health;
        _manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_manager.running)
            return;
    }
    
    public void Damage()
    {
        health -= 20;
        if (health <= 0)
        {
            _manager.running = false;
            Debug.Log("Game Over you lost!");
        }

        float percentage = _healthPerUnit * health;
        Vector3 newHealthAmount = new Vector3(percentage/100f , healthBar.localScale.y, healthBar.localScale.z);
        healthBar.localScale = newHealthAmount;
    }
}
