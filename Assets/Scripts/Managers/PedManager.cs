using UnityEngine;
using System.Collections.Generic;

public class PedManager : MonoBehaviour
{
    public Dictionary<int, GameObject> Peds;
    public RuntimeAnimatorController[] Animators;
    public GameObject currentPed;
    public GameObject Spawner;

    public static PedManager Instance;
    private void Awake()
    {
        Instance = this;
        Peds = new Dictionary<int, GameObject>();
    }

    public void InitPeds()
    {
        for (int i = 0; i < LevelsManager.Instance.levelsSettings.Levels.Length; i++)
        {
            GameObject ped = Instantiate(LevelsManager.Instance.levelsSettings.Levels[i].Ped, Spawner.transform);
            ped.AddComponent<Ped>();
            ped.SetActive(false);
            Peds.Add(i, ped);
        }
    }

    public void SpawnPed(int level)
    {
        currentPed = Peds[level].gameObject;
        currentPed.transform.position = Spawner.transform.position;
        currentPed.SetActive(true);
        currentPed.GetComponent<Ped>().GoToTarget(new Vector3(-1.7f, 0, 4.5f), new Vector3(0, 60, 0));
    }

    public void HidePed()
    {
        currentPed.SetActive(false);
    }
}