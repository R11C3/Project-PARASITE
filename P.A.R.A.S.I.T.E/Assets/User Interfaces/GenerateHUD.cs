using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class GenerateHUD : MonoBehaviour
{

    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    private VisualElement _root;
    private VisualElement _topThird;
    private VisualElement _middleThird;
    private VisualElement _bottomThird;

    private VisualElement _ammoContainer;
    private Label _ammoLabel;
    private Label _currentAmmo;
    private Label _slash;
    private Label _maxAmmo;

    private VisualElement _healthContainer;
    private ProgressBar _healthBar;

    [SerializeField]
    private BallisticGun gun;
    [SerializeField]
    private SO_Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenerateBase());
        // StartCoroutine(GenerateHealth());
        StartCoroutine(GenerateAmmo());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GenerateBase());
        // StartCoroutine(GenerateHealth());
        StartCoroutine(GenerateAmmo());
    }

    private IEnumerator GenerateBase()
    {
        yield return null;
        _root = _document.rootVisualElement;
        _root.Clear();

        _root.styleSheets.Add(_styleSheet);
    }

    private IEnumerator GenerateHealth()
    {
        yield return null;

        _healthContainer = new VisualElement();
        _healthContainer.AddToClassList("health-container");

        _healthBar = new ProgressBar();
        _healthBar.AddToClassList("health-bar");

        _root.Add(_healthContainer);
        _healthContainer.Add(_healthBar);

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _healthBar.lowValue = 0.0f;
        _healthBar.highValue = player.maxHealth;

        _healthBar.value = player.health;
    }

    private IEnumerator GenerateAmmo()
    {
        yield return null;
        _ammoContainer = new VisualElement();
        _ammoContainer.AddToClassList("ammo-container");

        _ammoLabel = new Label("Ammo: ");
        _ammoLabel.AddToClassList("ammo-label");

        _currentAmmo = new Label("-1");
        _currentAmmo.AddToClassList("current-ammo");

        _slash = new Label("/");
        _slash.AddToClassList("ammo-label");

        _maxAmmo = new Label("-1");
        _maxAmmo.AddToClassList("current-ammo");

        _root.Add(_ammoContainer);
        _ammoContainer.Add(_ammoLabel);
        _ammoContainer.Add(_currentAmmo);
        _ammoContainer.Add(_slash);
        _ammoContainer.Add(_maxAmmo);

        UpdateAmmo();
    }

    private void UpdateAmmo()
    {
        _currentAmmo.text = gun.gunData.currentAmmo.ToString();
        _maxAmmo.text = gun.gunData.maxAmmo.ToString();
    }
}
