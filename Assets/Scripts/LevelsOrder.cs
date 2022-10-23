using UnityEngine;

[CreateAssetMenu(fileName = "LevelsOrder", menuName = "ScriptableObjects/SpawnLevelsOrderObject", order = 1)]
public class LevelsOrder : ScriptableObject
{
    public GameObject[] levels;
}