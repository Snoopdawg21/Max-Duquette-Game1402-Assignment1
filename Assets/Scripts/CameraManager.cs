using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private Transform player;
    
    void FixedUpdate() 
    {
        transform.position = new Vector3(player.position.x, player.position.y + 2, transform.position.z);
    }
    
}
