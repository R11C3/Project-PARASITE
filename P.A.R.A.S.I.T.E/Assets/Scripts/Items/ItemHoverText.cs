using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHoverText : MonoBehaviour
{
    private TextMeshProUGUI hoverText;
    private SO_Item item;
    private string itemName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemName = GetComponent<PickUp>().itemData.itemName;
        hoverText = GameObject.Find("Character").GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnMouseOver()
    {
        if (hoverText != null)
        {
            hoverText.text = itemName;
            Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
            position.y += 35f;
            hoverText.gameObject.transform.position = position;
        }
    }

    void OnMouseExit()
    {
        if (hoverText != null)
        {
            hoverText.text = "";
        }
    }
}
