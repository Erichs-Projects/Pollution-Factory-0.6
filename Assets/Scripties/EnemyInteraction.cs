using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyInteraction : MonoBehaviour
{

  public PlayerStats playerStats;
  private UIManager uiManager;
  public bool IsHit;
  public int MaxHealth = 100;
  public int heathCurrent;

  public HealthScript healthbar;
  public PlayerStats moneyValue;
  

  void Start()
  {
    heathCurrent = MaxHealth;
    healthbar.SetMaxHealth(MaxHealth);

  }

  void TakeDamge(int damage)
  {
    heathCurrent -= damage;

    healthbar.SetHealth(heathCurrent);
  }
  void Heal(int healing)
  {
    heathCurrent += healing;
     healthbar.SetHealth(heathCurrent);
  }
  void OnTriggerEnter (Collider other)
  {
    Debug.Log(other.GetComponent<Collider>().name);
    if (other.gameObject.tag == "SmogMon"){
IsHit = true;
      TakeDamge(20);
    }
else if (other.gameObject.tag != "SmogMon"){

  IsHit = false;
    }
 if (other.gameObject.tag == "HealPotion" && playerStats.moneyValue >=10 && heathCurrent < 100){
IsHit = false;
      Heal(10);
      playerStats.changeMoneyBy(-10);
    }

  }
  void Update()
  {
    if (heathCurrent == 0)
    {
      SceneManager.LoadScene(2);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.Confined;

    }
  }
}
