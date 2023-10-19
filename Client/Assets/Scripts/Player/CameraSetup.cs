using Cinemachine;
using Photon.Pun;

public class CameraSetup : MonoBehaviourPun
{
    void Start()
    {
        if (this.photonView.IsMine)
        {
            var followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = this.transform;
            followCam.LookAt = this.transform;
        }
    }
}
