using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        this.initialPosition = new Vector3[this.enemies.Length];
        for (int i = 0; i < this.enemies.Length; i++)
        {

            if (this.enemies[i] != null)
            {
                this.initialPosition[i] = this.enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool status)
    {
        for (int i = 0; i < this.enemies.Length; i++)
        {
            if (this.enemies[i] != null)
            {
                this.enemies[i].SetActive(status);
                this.enemies[i].transform.position = this.initialPosition[i];
            }

        }
    }
}
