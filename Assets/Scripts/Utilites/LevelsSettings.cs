using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color Mixer/Levels Settings")]
public class LevelsSettings : ScriptableObject
{
    [NonReorderable]
    public Level[] Levels;

    [System.Serializable]
    public class Level
    {
        public GameObject[] Fruits;
        public GameObject Ped;
        public Color ResultColor;
    }
}
