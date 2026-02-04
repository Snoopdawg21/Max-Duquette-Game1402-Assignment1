using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float cycleTime = 5f;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    private float currentTime = 0f;
    private float speed = 1f;
    
    // Update is called once per frame
    void Update()
    {
        currentTime += speed * Time.deltaTime;

        if (currentTime > cycleTime) speed = -1f;
        if (currentTime < 0f) speed = 1f;
        
        float t = currentTime / cycleTime;
        
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}
