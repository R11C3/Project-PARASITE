using UnityEngine;

[CreateAssetMenu(fileName = "SO_WorldData", menuName = "Scriptable Objects/World Data")]
public class SO_WorldData : ScriptableObject
{
    [Header("Randomization Loot Seed")]
    [SerializeField]
    public int lootSeed;

    private System.Random random;
}
