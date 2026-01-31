using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public GameObject gm;
    private int scoreValue = 1;
    
    public void OnCollect()
    {
        GameManager manager = gm.GetComponent<GameManager>();

        if (manager == null) return;
        
        manager.IncreaseScore(scoreValue); 
        Destroy(gameObject);
    }
}
