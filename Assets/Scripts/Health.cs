using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public GameObject player;
    
    public void OnCollect()
    {
        PlayerControler controler = player.GetComponent<PlayerControler>();
        controler.Heal(1);
        Destroy(gameObject);
    }
}
