using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public GameObject player;
    
    private int healthIncrease = 1;

    
    public void OnCollect()
    {
        PlayerControler controler = player.GetComponent<PlayerControler>();

        if (controler == null) return;
        
        controler.Heal(healthIncrease);
        Destroy(gameObject);
    }
}
