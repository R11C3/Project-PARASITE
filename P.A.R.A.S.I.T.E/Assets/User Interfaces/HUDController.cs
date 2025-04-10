using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private PlayerStats player;

    public VisualElement ui;

    public bool visible;

    public Label currentAmmo;
    public Label maxAmmo;
    public Label fireMode;

    public ProgressBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        player = GameObject.Find("Character").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.visible = visible;

        SO_Gun gun = player.equipmentInventory.EquippedGun();
        if(gun != null)
        {
            currentAmmo.text = gun.currentAmmo.ToString();
            maxAmmo.text = gun.maxAmmo.ToString();
            fireMode.text = gun.currentFireMode.ToString();
        }
        healthBar.highValue = player.maxHealth;
        healthBar.value = player.currentHealth;
    }

    void OnEnable()
    {
        currentAmmo = ui.Q<Label>("current-ammo");
        maxAmmo = ui.Q<Label>("max-ammo");
        fireMode = ui.Q<Label>("fire-mode");

        healthBar = ui.Q<ProgressBar>("health-bar");
    }
}
