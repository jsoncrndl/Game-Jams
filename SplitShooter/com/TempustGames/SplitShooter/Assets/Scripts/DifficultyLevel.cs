using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new_difficulty", menuName = "Shooter/Difficulty Setting")]
public class DifficultyLevel : ScriptableObject
{
    public Vector2 spawnTimer;
    public Vector2Int moveSpeed;
    public int enemyScore;
}