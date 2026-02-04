using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour, IInteractables
{
    [FormerlySerializedAs("clicked")] public bool inZone;
    private bool clicked;
    public GameObject shopMenu;
    
    public void Interact()
    {
        clicked = !clicked;
        
        if (inZone && clicked)
        {
            shopMenu.SetActive(true);
        } else
        {
            shopMenu.SetActive(false);
        }
    }
}
