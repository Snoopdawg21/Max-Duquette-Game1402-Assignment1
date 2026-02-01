using UnityEngine;

public class Shop : MonoBehaviour, IInteractables
{
    public bool clicked;
    public GameObject shopMenu;
    
    public void Interact()
    {
        if(clicked)
            shopMenu.SetActive(true);
        else
            shopMenu.SetActive(false);
        
        Debug.Log(clicked);
    }
}
