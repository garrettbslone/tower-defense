using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceTower9001 : MonoBehaviour
{
  public GameplayManager manager;
  // each tower costs 20 for now
  public GameObject Tower;

  public GameObject World;
    // Start is called before the first frame update
    void Start()
    {
      manager = GameObject.FindWithTag("GPM").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (!manager.running)
        return;
      
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
          if (hit.transform.tag == "TowerSpot")
          {
            //Book keeping
            // if good
            if (manager.coinPurse < 20)
            {
              Debug.Log($"Tower costs 30 coins to place, but you only have {manager.coinPurse}!");
              return;
            }

            manager.coinPurse -= 20;
            hit.transform.gameObject.SetActive(false);
            PlaceTower(hit.transform.position);
          }
        else
          print("I'm looking at nothing!");
        
    }

    //raycast
    //hitplace
    //purse script $$$$
    //instantiate a tower

  }

    void PlaceTower(Vector3 position)
    {
      //Book keeping
      var t = Instantiate(Tower, position, Quaternion.identity, World.transform);
      manager.towers.Add(t);
    }
}
