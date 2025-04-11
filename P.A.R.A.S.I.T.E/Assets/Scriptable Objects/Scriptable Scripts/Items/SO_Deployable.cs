using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Deployable", menuName = "Scriptable Objects/Item/Deployable")]
public class SO_Deployable : SO_Item
{
    public void Reset()
    {
        type = Type.Deployable;
    }
}
