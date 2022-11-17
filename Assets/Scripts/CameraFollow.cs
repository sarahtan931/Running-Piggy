
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraDistance = 15.0f;
    public float start;
    public float end;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    void LateUpdate()
    { 
        if (player.position.x < start) {
            transform.position = new Vector3(start, transform.position.y, transform.position.z);
        } else if (player.position.x > end) {

            transform.position = new Vector3(end, transform.position.y, transform.position.z);
        }
        else {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

}
