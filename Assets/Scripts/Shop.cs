using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopObject;
    

    private bool clickedButton;
    
    public void ButtonPress()
    {
        clickedButton = true;
    }

    public void ButtonUp()
    {
        clickedButton = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            Debug.Log("hi");
            if(!clickedButton)
                shopObject.SetActive(true);
            else
                shopObject.SetActive(false);
        }
    }

    
}
