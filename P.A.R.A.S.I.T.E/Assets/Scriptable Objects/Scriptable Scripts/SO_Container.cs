using UnityEngine;

[CreateAssetMenu(fileName = "SO_Container", menuName = "Scriptable Objects/Container")]
public class SO_Container : ScriptableObject
{
    [SerializeField]
    public Dimensions dimensions;

    public GridInventory inventory;

    public void IntializeInventories()
    {
        inventory = new GridInventory(dimensions.width, dimensions.height);
    }
}
