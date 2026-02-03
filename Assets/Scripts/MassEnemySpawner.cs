using UnityEngine;

public class MassEnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnLocation;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("player")) return;

        for (int i = 0; i < 25; i++)
        {
            Instantiate(enemy, spawnLocation.position, spawnLocation.rotation);
            
            spawnLocation.position = new Vector3(spawnLocation.position.x + 7.72f, spawnLocation.position.y, spawnLocation.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("player") || !col.gameObject.CompareTag("Enemy")) return;
        
        spawnLocation.position = new Vector3(-68.6f, spawnLocation.position.y, spawnLocation.position.z);
        
        if(col.gameObject.CompareTag("Enemy"))
            Destroy(col.gameObject);
    }
}
