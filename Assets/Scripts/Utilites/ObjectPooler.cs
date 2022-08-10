using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Material material;
        public int size;
    }

    [NonReorderable]
    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        Init();
    }

    #endregion
    void Init()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.AddComponent<Fruit>();
                obj.GetComponent<Fruit>().MainMaterial = pool.material;

                obj.AddComponent<SphereCollider>();
                obj.AddComponent<Rigidbody>();
                obj.GetComponent<Rigidbody>().mass = 10;

                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        rotation.y = Random.Range(0, 360);
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        objectToSpawn.GetComponent<SphereCollider>().enabled = false;
        objectToSpawn.GetComponent<Rigidbody>().isKinematic = true;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void DisableAll(){
        var list = GameObject.FindObjectsOfType<Fruit>();
        foreach(var fruit in list){
            fruit.gameObject.SetActive(false);
        }
    }
}
