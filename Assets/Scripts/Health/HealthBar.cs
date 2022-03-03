using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        this.totalHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        this.currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;

    }

}
