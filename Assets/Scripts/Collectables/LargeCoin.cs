using UnityEngine;

public class LargeCoin : MonoBehaviour, ICollectable
{
    public GameObject gm;
    private int scoreValue = 10;
    
    public void OnCollect()
    {
        GameManager manager = gm.GetComponent<GameManager>();

        if (manager == null) return;
        
        manager.IncreaseScore(scoreValue); 
        Destroy(gameObject);
    }
}
