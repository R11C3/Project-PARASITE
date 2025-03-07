using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class GenerateHUD : MonoBehaviour
{

    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    private VisualElement _root;
    private VisualElement _ammoContainer;
    private Label _ammoLabel;
    private Label _currentAmmo;
    private Label _slash;
    private Label _maxAmmo;

    [SerializeField]
    private BallisticGun gun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return null;
        _root = _document.rootVisualElement;
        _root.Clear();

        _root.styleSheets.Add(_styleSheet);

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
        _currentAmmo.text = gun.currentAmmo.ToString();
        _maxAmmo.text = gun.maxAmmo.ToString();
    }
}
