using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private SO_Player player;

    public VisualElement ui;

    public Label currentAmmo;
    public Label maxAmmo;

    public ProgressBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        currentAmmo.text = player.weaponInventory[player.activeSlot].currentAmmo.ToString();
        maxAmmo.text = player.weaponInventory[player.activeSlot].maxAmmo.ToString();

        healthBar.highValue = player.maxHealth;
        healthBar.value = player.health;
    }

    void OnEnable()
    {
        currentAmmo = ui.Q<Label>("current-ammo");
        maxAmmo = ui.Q<Label>("max-ammo");

        healthBar = ui.Q<ProgressBar>("health-bar");
    }
}
