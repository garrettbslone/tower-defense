using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public AudioClip deathSound;
  public Path route;
  public Waypoint[] myPathThroughLife;
  public int coinWorth;
  public float health = 100;
  public float speed = .25f;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;
  private float healthPerUnit;
  private float timeBetweenDmg = 2f;
  private float timeSinceDmg = 0f;

  public Transform healthBar;
  private GameplayManager _manager;
  private EndPoint _end;
  
  void Start()
  {
    _manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();
    _end = GameObject.Find("End").GetComponent<EndPoint>();
    healthPerUnit = 100f / health;
  }

  public void Spawn(Path p)
  {
    route = p;
    
    myPathThroughLife = route.path;
    myPathThroughLife = myPathThroughLife.Reverse().ToArray();

    index = myPathThroughLife.Length - 1;
    
    transform.position = myPathThroughLife[index].transform.position;
    
    Recalculate();
  }

  void Update()
  {
    if (!_manager.running)
      return;
    if (!stop)
    {
      if (!transform)
      {
        Debug.Log("no trans");
        return;
      }

      if (!myPathThroughLife[index + 1])
      {
        Debug.Log("no ind + 1");
        return;
      }
      if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
      {
        index = index + 1;
        Recalculate();
      }


      Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
      transform.Translate(moveThisFrame);
    }
    else
    {
      timeSinceDmg += Time.deltaTime;

      if (timeSinceDmg >= timeSinceDmg)
      {
        timeSinceDmg = 0;
        _end.Damage();
      }
    }
  }

  void Recalculate()
  {
    if (index < myPathThroughLife.Length -1)
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
      _manager.PlayAudio(deathSound);
      _manager.coinPurse += this.coinWorth;
      Debug.Log($"{transform.name} is Dead");
      Destroy(this.gameObject);
    }

    float percentage = healthPerUnit * health;
    Vector3 newHealthAmount = new Vector3(percentage/100f , healthBar.localScale.y, healthBar.localScale.z);
    healthBar.localScale = newHealthAmount;
  }

}
