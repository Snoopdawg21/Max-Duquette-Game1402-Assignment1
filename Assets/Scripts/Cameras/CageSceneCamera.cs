using UnityEngine;

public class CageSceneCamera : MonoBehaviour, ICameras
{
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject thisCam;

    public void SwitchCamOn()
    {
        if (playerCam == null) return;
        
        thisCam.SetActive(true);
        playerCam.SetActive(false);
    }

    public void SwitchCamOff()
    {
        if (playerCam == null) return;
        
        thisCam.SetActive(false);
        playerCam.SetActive(true);
    }
}
