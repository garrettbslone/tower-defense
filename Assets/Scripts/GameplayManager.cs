using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    // public GameObject[] waypoints;

    public List<Enemy> enemies;
    public List<GameObject> towers;
    public GameObject enemy;
    public bool running = false;
    
    private TMP_Text _coins;
    private int _coinPurse = 50;
    private AudioSource _audio;
    private Button _start, _restart;

    // Start is called before the first frame update
    void Start()
    {
        _coins = GameObject.Find("Coins").GetComponent<TMP_Text>();
        _coins.SetText($"Coins: {_coinPurse}");
        _audio = GetComponent<AudioSource>();
        _start = GameObject.Find("Start").GetComponent<Button>();
        _restart = GameObject.Find("Restart").GetComponent<Button>();
        _restart.gameObject.SetActive(false);
        
        _start.onClick.AddListener(() =>
        {
            _start.gameObject.SetActive(false);
            _restart.gameObject.SetActive(false);

            SpawnEnemies();
            
            running = true;
        });
        
        _restart.onClick.AddListener(() =>
        {
            _start.gameObject.SetActive(false);
            _restart.gameObject.SetActive(false);
            
            SpawnEnemies();
            
            running = true;
        });
    }

    private async void SpawnEnemies()
    {
        var p = GameObject.Find("Waypoints").GetComponent<Path>();
        
        for (var i = 0; i < 5; i++)
        {
            await Task.Delay(300);
            var e = Instantiate(enemy).GetComponent<Enemy>();
            e.Spawn(p);
            enemies.Add(e);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            _restart.gameObject.SetActive(true);
            return;
        }

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

    public void PlayAudio(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }

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
