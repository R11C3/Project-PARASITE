using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField]
    private SO_Item item;

    public override void Interact(GameObject source)
    {
        Debug.Log("Interacted");
        PlayerStats playerStats;
        source.TryGetComponent<PlayerStats>(out playerStats);

        playerStats._player.Add(item);

        Destroy(gameObject);
    }
}
