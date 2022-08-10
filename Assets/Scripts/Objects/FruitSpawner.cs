using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    GameObject currentFruit;
    public string Name;

    private void Start(){
        objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable() {
        StartCoroutine("SpawnStart", 1f);
    }

    IEnumerator SpawnStart(float delay){
        yield return new WaitForSeconds(delay);
        Spawn();
    }

    public void Spawn(){
        currentFruit = objectPooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
        currentFruit.transform.position = transform.position;
    }

    public void PushFruit(){
        Mixer.Instance.AddToMixer(currentFruit);
        Spawn();
    }
}
