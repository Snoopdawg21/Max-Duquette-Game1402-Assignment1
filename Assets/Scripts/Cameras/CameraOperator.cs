using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        ICameras cam = col.GetComponent<ICameras>();
        
        if (cam != null)
            cam.SwitchCamOn();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        ICameras cam = col.GetComponent<ICameras>();
        
        if (cam != null)
            cam.SwitchCamOff();
    }
}
