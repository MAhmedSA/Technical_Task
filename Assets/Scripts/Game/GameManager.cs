using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy")]
    [Tooltip("Array of Enemies")]
    [SerializeField]
    GameObject[] enemies;

    [Header("Spawn Positions")]
    [Tooltip("Array of random positions")]
    [SerializeField]
    Transform[] spwanPositions;

    [Header("Time Variables")]
    [Tooltip("Time Between Spawns")]
    float timeBetweenSpawns = 2f;
    float waveTimeRespwan = 5f;




}
