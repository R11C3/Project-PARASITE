using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private PlayerStats player;

    public VisualElement ui;

    public Label currentAmmo;
    public Label maxAmmo;

    public ProgressBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        SO_Gun gun = player.weaponInventory.Get(player.activeSlot);
        if(gun != null)
        {
            currentAmmo.text = gun.currentAmmo.ToString();
            maxAmmo.text = gun.maxAmmo.ToString();
        }
        healthBar.highValue = player.maxHealth;
        healthBar.value = player.currentHealth;
    }

    void OnEnable()
    {
        currentAmmo = ui.Q<Label>("current-ammo");
        maxAmmo = ui.Q<Label>("max-ammo");

        healthBar = ui.Q<ProgressBar>("health-bar");
    }
}
