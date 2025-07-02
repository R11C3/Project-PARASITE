using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Container : Interactable
{
    [SerializeField]
    private SO_Container containerData;
    private SO_Container container;
    [SerializeField]
    private SO_LootTable lootTableData;
    private SO_LootTable lootTable;

    [SerializeField]
    private ContainerInventory inventory;

    [SerializeField]
    private PlayerStats stats;

    public bool looted = false;

    void Start()
    {
        container = Instantiate(containerData);

        lootTable = Instantiate(lootTableData);

        inventory = new ContainerInventory(container.size);

        lootTable.GenerateTable();
        GenerateLoot();
    }

    public void GenerateLoot()
    {
        int items = container.size;
        for (int i = 0; i < items; i++)
        {
            inventory.Add(lootTable.GetRandomItem());
        }
    }

    public override void Interact(GameObject source)
    {
        if (!looted)
        {
            Vector3 baseDirection = source.transform.position - transform.position;
            baseDirection.Normalize();

            LayerMask mask = LayerMask.GetMask("Default") | LayerMask.GetMask("EnvironmentWalkThrough");

            StartCoroutine(BoomItem(baseDirection, mask));
        }

        looted = true;
    }

    private IEnumerator BoomItem(Vector3 baseDirection, LayerMask mask)
    {
        foreach (SO_Item item in inventory.itemList)
        {
            GameObject newItem = Instantiate(item.obj, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            Rigidbody itemRB = newItem.GetComponent<Rigidbody>();

            Vector3 direction;

            Vector3 randomSpread = Random.insideUnitCircle;
            randomSpread.z = randomSpread.y;
            randomSpread.y = 0f;

            baseDirection += randomSpread;
            baseDirection.Normalize();

            direction.x = baseDirection.x * (5f + Random.Range(0, 5));
            direction.y = baseDirection.y * (5f + Random.Range(0, 15));
            direction.z = baseDirection.z * (5f + Random.Range(0, 5));

            itemRB.isKinematic = false;
            itemRB.linearDamping = 1;
            itemRB.excludeLayers = mask;
            itemRB.freezeRotation = true;

            itemRB.AddForce(direction, ForceMode.Impulse);

            yield return new WaitForSecondsRealtime(0.2f);
        }

        yield return null;
    }
}
