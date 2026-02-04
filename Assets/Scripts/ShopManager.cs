using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject shopObject;

    public Shop shop;

    private bool clickedButton;

    void OnEnable()
    {
        inputManager.OnInteract += shop.Interact;
    }
    
    void OnDisable()
    {
        inputManager.OnInteract -= shop.Interact;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (shop == null) return;
        
        shop.inZone = true;
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (shop != null)
            shop.inZone = false;
    }
}
