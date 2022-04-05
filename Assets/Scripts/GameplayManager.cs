using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // public GameObject[] waypoints;

    private TMP_Text _coins;
    private int _coinPurse = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        _coins = GameObject.Find("Coins").GetComponent<TMP_Text>();
        _coins.SetText($"Coins: {_coinPurse}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.transform.position.z));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().Damage();
                }
                else
                    print("I'm looking at nothing!");

        }
    }
    //
    // public void EnemyKilled(EnemyController enemy)
    // {
    //     _coinPurse += enemy.coins;
    //     Debug.Log($"Enemy killed. Coins: {_coinPurse}");
    //     Destroy(enemy.gameObject);
    // }

    public int coinPurse
    {
        get => _coinPurse;
        set
        {
            _coinPurse = value;
            _coins.SetText($"Coins: {_coinPurse}");
        }
    }
}
