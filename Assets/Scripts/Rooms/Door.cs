using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {

                this.cameraController.MoveToRoom(this.nextRoom);
                this.nextRoom.GetComponent<Room>().ActivateRoom(true);
                this.previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                this.cameraController.MoveToRoom(this.previousRoom);
                 this.nextRoom.GetComponent<Room>().ActivateRoom(false);
                this.previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }
}
