using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private ParticleSystem footstepsEffect;
    [SerializeField] private string targetTag = "ground";

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            footstepsEffect.Play();
        }
    }
    
}
