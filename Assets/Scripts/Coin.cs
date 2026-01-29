using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("straight up coining it bro");
        Destroy(gameObject);
    }
}
