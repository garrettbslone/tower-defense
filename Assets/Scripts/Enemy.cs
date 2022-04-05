using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Path route;
  private List<Waypoint> myPathThroughLife;
  public int coinWorth;
  public float health = 100;
  public float speed = .25f;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;
  private float healthPerUnit;

  public Transform healthBar;
  private GameplayManager _manager;
  
  void Start()
  {
    _manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();
    healthPerUnit = 100f / health;

    myPathThroughLife = route.path.ToList();
    var e = myPathThroughLife[^1];
    myPathThroughLife.RemoveAt(myPathThroughLife.Count - 1);
    myPathThroughLife.Reverse();
    myPathThroughLife.Add(e);
    
    foreach (var w in myPathThroughLife)
    {
      Debug.Log($"{w.name} -- @ -> {w.transform.position}");
    }

    transform.position = myPathThroughLife[index].transform.position;
    Recalculate();
  }

  void Update()
  {
    if (!stop)
    {
      if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
      {
        index = index + 1;
        Recalculate();
      }


      Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
      transform.Translate(moveThisFrame);
    }

  }

  void Recalculate()
  {
    if (index < myPathThroughLife.Count -1)
    {
      nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;

    }
    else
    {
      stop = true;
    }
  }

  public void Damage()
  {
    health -= 20;
    if (health <= 0)
    {

      _manager.coinPurse += this.coinWorth;
      Debug.Log($"{transform.name} is Dead");
      Destroy(this.gameObject);
    }

    float percentage = healthPerUnit * health;
    Vector3 newHealthAmount = new Vector3(percentage/100f , healthBar.localScale.y, healthBar.localScale.z);
    healthBar.localScale = newHealthAmount;
  }

}
