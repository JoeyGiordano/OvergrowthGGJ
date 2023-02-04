using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    private static CameraController _CameraController;

    public static CameraController Instance { get { return _CameraController; } }


    private void Awake()
    {
        if (_CameraController != null && _CameraController != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _CameraController= this;
        }
    }
    #endregion

    public enum PlayerPositionState{
        PlayerIsInRoom, //case when player is in room; camera should be focused on room
        PlayerIsInHallway, //default case; camera should track player
    };
    // Start is called before the first frame update
    public Transform target;
    public float lerpSpeed = 5.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    PlayerPositionState playerState = PlayerPositionState.PlayerIsInHallway;

    private void Start()
    {
        
        target = GameObject.Find("Player").transform;
        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target == null) return;

        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
       
    }

    public void setPlayerPositionState(PlayerPositionState newPlayerState, Transform newTarget){
        playerState = newPlayerState;
        target = newTarget; //either player or a room
    }
}
