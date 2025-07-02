using UnityEngine;

[CreateAssetMenu(fileName = "SO_Container", menuName = "Scriptable Objects/Container")]
public class SO_Container : ScriptableObject
{
    [SerializeField]
    public int size;
}
