using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        ICollectable otherCollectable = col.GetComponent<ICollectable>();

        if (otherCollectable != null)
        {
            otherCollectable.OnCollect();
        }
    }
}
