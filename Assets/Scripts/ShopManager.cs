using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject shopObject;

    private bool clickedButton;

    void OnEnable()
    {
        Shop shop = shopObject.GetComponent<Shop>();
        
        inputManager.OnInteract += shop.Interact;
    }
    
    void OnDisable()
    {
        Shop shop = shopObject.GetComponent<Shop>();
        
        inputManager.OnInteract -= shop.Interact;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shop"))
            col.gameObject.GetComponent<Shop>().clicked = true;
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shop"))
            col.gameObject.GetComponent<Shop>().clicked = false;
    }
}
