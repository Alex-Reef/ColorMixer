using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] Spawners;
    public LayerMask clickable;

    public static SpawnerManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Enable()
    {
        Disable();
        var Fruits = LevelsManager.Instance.levelsSettings.Levels[LevelsManager.Instance.CurrentLevel].Fruits;
        for (int i = 0; i < Fruits.Length; i++)
        {
            Spawners[i].GetComponent<FruitSpawner>().Name = Fruits[i].name;
            Spawners[i].SetActive(true);
        }
    }

    public void Disable()
    {
        foreach (var spawner in Spawners)
        {
            spawner.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (hit.collider.gameObject.GetComponent<FruitSpawner>())
                {
                    hit.collider.gameObject.GetComponent<FruitSpawner>().PushFruit();
                }
            }
        }
    }
}