using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDownTime;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float coolDownTime;

    public void Attack()
    {
        this.coolDownTime = 0;

        this.arrows[this.FindArrow()].transform.position = firePoint.position;
        this.arrows[this.FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < this.arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        this.coolDownTime += Time.deltaTime;

        if (this.coolDownTime >= this.attackCoolDownTime)
        {
            this.Attack();
        }
    }
}
