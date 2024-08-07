using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    Text moneyLabel;
    Text pollutionLabel;
    Text dayLabel;
    Canvas buyCanvas;
    [SerializeField]
    Canvas factoryCanvas;
    Canvas villageCanvas;
    Canvas blueCanvas;
    Canvas timerCanvas;
    [SerializeField]
    public Text timerText;
    Dropdown buildingSelection;
    Dropdown blueBuildingSelection;
    Text buildingInfoText;
    GameObject playerController;
    GameObject plotSpawner;
    GameObject bluePlotSpawner;
 
    GameManager gameManager;

    private int curPlotNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        moneyLabel = GameObject.Find("MoneyLabel").GetComponent<Text>();
        pollutionLabel = GameObject.Find("PollutionLabel").GetComponent<Text>();
        dayLabel = GameObject.Find("DayLabel").GetComponent<Text>();
        buyCanvas = GameObject.FindGameObjectWithTag("BuyCanvas").GetComponent<Canvas>();
        factoryCanvas = GameObject.FindGameObjectWithTag("FactoryCanvas").GetComponent<Canvas>();
        blueCanvas = GameObject.Find("BlueCanvas").GetComponent<Canvas>();
        timerCanvas = GameObject.FindGameObjectWithTag("TimerCanvas").GetComponent<Canvas>();
        //villageCanvas = GameObject.FindGameObjectWithTag("VillageCanvas").GetComponent<Canvas>();
        buildingInfoText = GameObject.Find("BuildingInfoText").GetComponent<Text>();
        buildingSelection = GameObject.Find("BuildingDropdownMenu").GetComponent<Dropdown>();
        //blueBuildingSelection = GameObject.Find("BlueBuildingDropdownMenu").GetComponent<Dropdown>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag("Player");
        plotSpawner = GameObject.Find("PSpawner");
        bluePlotSpawner = GameObject.Find("BPSpawner");
        timerText = GameObject.Find("TimerText").GetComponent<Text>();

        moneyLabel.text = "Money: " + playerStats.moneyValue;
        pollutionLabel.text = "Pollution: " + playerStats.pollutionValue;
        dayLabel.text = "Day: " + playerStats.day;
        buyCanvas.enabled = false;
        factoryCanvas.enabled = false;
        blueCanvas.enabled = false;
        //villageCanvas.enabled = false;
        //timerCanvas.enabled = false;
        buildingSelection.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(buildingSelection);
        });

        /*blueBuildingSelection.onValueChanged.AddListener(delegate
        {
            BlueDropdownValueChanged(blueBuildingSelection);
        });
        */
    }

    // Update is called once per frame
    void Update()
    {
        moneyLabel.text = "Money: " + playerStats.moneyValue;
        pollutionLabel.text = "Pollution: " + playerStats.pollutionValue;
        dayLabel.text = "Day: " + playerStats.day;
    }

    public void showBuyMenu(int plotNum)
    {
        buyCanvas = GameObject.FindGameObjectWithTag("BuyCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Update buy canvas content
        curPlotNum = plotNum;

        // Enable buy canvas
        buyCanvas.enabled = true;

        // Change cursor state to unlocked
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Disable player controller
        playerController.GetComponent<SC_FPSController>().enabled = false;
    }

    public void showBlueBuyMenu(int plotNum)
    {
        blueCanvas = GameObject.FindGameObjectWithTag("BlueCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Update buy canvas content
        curPlotNum = plotNum;

        // Enable buy canvas
        blueCanvas.enabled = true;

        // Change cursor state to unlocked
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Disable player controller
        playerController.GetComponent<SC_FPSController>().enabled = false;
    }

    /*
    public void showVillageMenu()
    {
        villageCanvas = GameObject.FindGameObjectWithTag("VillageCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Update buy canvas content
        //curPlotNum = plotNum;

        // Enable buy canvas
        villageCanvas.enabled = true;

        // Change cursor state to unlocked
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Disable player controller
        playerController.GetComponent<SC_FPSController>().enabled = false;
    }
    */
    public void ShowFactoryMenu()
    {
        factoryCanvas = GameObject.FindGameObjectWithTag("FactoryCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        factoryCanvas.enabled = true;

        // Change cursor state to unlocked
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Disable player controller
        playerController.GetComponent<SC_FPSController>().enabled = false;
    }

    public void PurchasePlot()
    {
        gameManager.ObjectInteraction(plotSpawner, new RaycastHit());
        hideFactoryMenu();
    }

    public void PurchaseBluePlot()
    {
        gameManager.ObjectInteraction(bluePlotSpawner, new RaycastHit());
        hideVillageMenu();
    }

    public void purchaseBuilding()
    {
        StartCoroutine("purchaseBuildingCoroutine");
    }
    IEnumerator purchaseBuildingCoroutine()
    {
        int[] costs = new int[] { 100, 200, 300 };
        int curCost = costs[buildingSelection.value];
        if (curCost <= playerStats.moneyValue)
        {
            playerStats.changeMoneyBy(-curCost);
            hideBuyMenu();
            timerText.text = "Building...";
            yield return new WaitForSeconds(costs[buildingSelection.value]/10);
            gameManager.CreateBuilding(curPlotNum, buildingSelection.value + 1);
            timerText.text = "";
        }
    }

    public void hideBuyMenu()
    {
        buyCanvas = GameObject.FindGameObjectWithTag("BuyCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Disable buy canvas
        buyCanvas.enabled = false;

        // Change cursor state to locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player controller
        playerController.GetComponent<SC_FPSController>().enabled = true;
    }

    public void hideBlueMenu()
    {
        blueCanvas = GameObject.FindGameObjectWithTag("BlueCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Disable buy canvas
        blueCanvas.enabled = false;

        // Change cursor state to locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player controller
        playerController.GetComponent<SC_FPSController>().enabled = true;
    }


    public void hideVillageMenu()
    {
        villageCanvas = GameObject.FindGameObjectWithTag("VillageCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Disable buy canvas
        villageCanvas.enabled = false;

        // Change cursor state to locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        // Enable player controller
        playerController.GetComponent<SC_FPSController>().enabled = true;
    }

    public void hideFactoryMenu()
    {
        factoryCanvas = GameObject.FindGameObjectWithTag("FactoryCanvas").GetComponent<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player");

        // Disable buy canvas
        factoryCanvas.enabled = false;

        // Change cursor state to locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player controller
        playerController.GetComponent<SC_FPSController>().enabled = true;
    }


    void DropdownValueChanged(Dropdown change)
    {

        switch (change.value)
        {
            case 0:
                buildingInfoText.text = " Wooden Low Power Windmil its cheap... but it works! Cost: 100";
                break;
            case 1:
                buildingInfoText.text = "Windmil: Very renewable. Cost: 200";
                break;
            case 2:
                buildingInfoText.text = "Coal Engine: May Cause Pollusion, but extremely powerful! Cost: 300";
                break;
            default:
                buildingInfoText.text = "Default Text";
                break;



         
        }
    }
    
    void BlueDropdownValueChanged(Dropdown change)
    {

        switch (change.value)
        {
            case 0:
                buildingInfoText.text = "Starting Windmil: Cheap, but it works! Cost: 150";
                break;
            case 1:
                buildingInfoText.text = "Powerful Windmil: Very renewable. Cost: 250";
                break;
            case 2:
                buildingInfoText.text = "Nuclear Reactor: Very Green and extremely powerful! Cost: 350";
                break;
            default:
                buildingInfoText.text = "Default Text";
                break;




        }
    }
}