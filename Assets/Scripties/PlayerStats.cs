using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/PlayerStatsContainer", order = 1)]
public class PlayerStats : ScriptableObject
{

    public int moneyValue = 0;
    public int pollutionValue = 0; // 0-100
    public bool hasOil = false; // If player picked up oil
    public int day = 0;
    public int[] plotTypes;
    public int[] buildingCounts;
    public string[] buildingNumToName = new string[] { };
    public bool isEvening = false;

    private GameObject oilCan;
    private MeshRenderer oilCanMesh;
    private GameObject Smog1, Smog2;
    private GameObject BigCloud, BigCloud002;

    public void Start()
    {
        buildingCounts = new int[] { 0, 0, 0 };
        plotTypes = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    public void changePollutionAmount(int amount)
    {
        if (pollutionValue <= 29 && pollutionValue + amount > 29)
        {
            GameObject Enemies = GameObject.Find("Enemies");
            Smog1 = Enemies.transform.Find("Smog1").gameObject;
            Smog1.SetActive(true);

        }
        else if (pollutionValue >= 29 && pollutionValue + amount < 29)
        {
            GameObject Enemies = GameObject.Find("Enemies");
            Smog1 = Enemies.transform.Find("Smog1").gameObject;
            Smog1.SetActive(false);
        }
        if (pollutionValue <= 50 && pollutionValue + amount > 50)
        {
            GameObject Enemies = GameObject.Find("Enemies");
            Smog2 = Enemies.transform.Find("Smog2").gameObject;
            Smog2.SetActive(true);
        }
        else if (pollutionValue >= 50 && pollutionValue + amount < 50)
        {
            GameObject Enemies = GameObject.Find("Enemies");
            Smog2 = Enemies.transform.Find("Smog2").gameObject;
            Smog2.SetActive(false);
        }
         if (pollutionValue <= 10 && pollutionValue + amount > 10)
        {
            GameObject PollutionAir = GameObject.Find("PollutionAir");
            BigCloud = PollutionAir.transform.Find("BigCloud").gameObject;
            BigCloud.SetActive(true);

        }
        else if (pollutionValue >= 10 && pollutionValue + amount < 10)
        {
            GameObject PollutionAir = GameObject.Find("PollutionAir");
            BigCloud = PollutionAir.transform.Find("BigCloud").gameObject;
            BigCloud.SetActive(false);
        }
        if (pollutionValue <= 20 && pollutionValue + amount > 20)
        {
            GameObject PollutionAir = GameObject.Find("PollutionAir");
            BigCloud002 = PollutionAir.transform.Find("BigCloud002").gameObject;
            BigCloud002.SetActive(true);

        }
        else if (pollutionValue >= 20 && pollutionValue + amount < 20)
        {
            GameObject PollutionAir = GameObject.Find("PollutionAir");
            BigCloud002 = PollutionAir.transform.Find("BigCloud002").gameObject;
            BigCloud002.SetActive(false);
        }
        pollutionValue += amount;
        if (pollutionValue > 100) pollutionValue = 100;
        if (pollutionValue < 0) pollutionValue = 0;
    }

    public void changeMoneyBy(int val)
    {
        moneyValue += val;
        if (moneyValue < 0) moneyValue = 0;
    }
      public void changeDayTo(int val)
    {
        day = 0;
    }
    public void pickUpOil()
    {
        oilCan = GameObject.Find("OilCan");
        oilCanMesh = oilCan.GetComponent<MeshRenderer>();

        oilCanMesh.enabled = true;
        hasOil = true;
    }

    public void submitOil()
    {
        oilCan = GameObject.Find("OilCan");
        oilCanMesh = oilCan.GetComponent<MeshRenderer>();
        oilCanMesh.enabled = false;

        hasOil = false;
        moneyValue += 100;
        changePollutionAmount(20);
    }

    public void increaseDayByOne() { day += 1; }

    public void IncreaseBuildingCount(int type)
    {
        buildingCounts[type - 1] = buildingCounts[type - 1] + 1;
    }

    public void DecreaseBuildingCount(int type)
    {
        buildingCounts[type - 1] = buildingCounts[type - 1] - 1;
    }

    public void SetHealth(int heathCurrent)
    {
        GameObject healthBar = GameObject.Find("Health bar");
    }

    public void UpdateMoney()
    {
        if (!isEvening)
        {
            int totalMoney = 1 * buildingCounts[0] + 3 * buildingCounts[1] + 6 * buildingCounts[2];
            changeMoneyBy(totalMoney);
            int totalPollution = 0 * buildingCounts[0] + 1 * buildingCounts[1] + 2 * buildingCounts[2];
            changePollutionAmount(totalPollution);
        }
    }

}
