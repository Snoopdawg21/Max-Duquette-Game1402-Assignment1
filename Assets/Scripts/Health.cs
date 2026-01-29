using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("in the stripped club, straight up healing it, and by it, haha, well, lets justr say, my healthbar");
        Destroy(gameObject);
    }
}
