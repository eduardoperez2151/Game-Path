using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Room camera
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity;


    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(this.currentPositionX, transform.position.y, transform.position.z), ref this.velocity, this.speed);

    }

    public void MoveToRoom(Transform nextRoomTransform)
    {
        this.currentPositionX = nextRoomTransform.position.x;
    }


}
