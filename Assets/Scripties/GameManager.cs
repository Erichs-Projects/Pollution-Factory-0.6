using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerStats playerStats;
    private UIManager uiManager;
    private GameObject sun;
    private GameObject fuelCanister;
    private GameObject factory1;
    private GameObject factory2;
    private GameObject bed;
    private SoundManager SoundManager;
    public GameObject treePrefab;
    private GameObject terrain;
    private GameObject plotSpawner;
    private GameObject bluePlotSpawner;
    private MeshRenderer plotPrefabMesh;
    public GameObject plotPrefab;
    public GameObject bluePlotPrefab;
    private int plots = 2;
    public float timeValue;
    bool nextClickPlace = false;
    bool bluePlot = false;
    private GameObject holdPlot;
    private MeshRenderer holdPlotMesh;

    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        factory1 = GameObject.Find("Factory001");
        factory2 = GameObject.Find("Factory002");
        fuelCanister = GameObject.Find("FuelCanister");
        bed = GameObject.Find("Bed");
        sun = GameObject.Find("Sun");
        terrain = GameObject.Find("Terrain");
        plotSpawner = GameObject.Find("PSpawner");
        bluePlotSpawner = GameObject.Find("BPSpawner");
        
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        plotPrefabMesh = plotPrefab.GetComponent<MeshRenderer>();

        playerStats.Start();
        playerStats.moneyValue = 0;
        playerStats.pollutionValue = 0;
        playerStats.hasOil = false;
        playerStats.day = 0;

        SoundManager.PlaySong("ForestAmbience");

        SetTreeArea();
    }

    public void SetTreeArea()
    {
        int i = 0;

        foreach (Transform child in terrain.transform)
        {
            if (child.name.Contains("terrain-plane-plain") || child.name.Contains("terrain-valley"))
            {
                child.tag = "PlantTreeArea";
                child.gameObject.layer = 6;
            }

            i += 1;
        }
    }


    public void ObjectInteraction(GameObject clickedObject, RaycastHit hit)
    {
        string name = clickedObject.name;
        //Debug.Log("clickedLayer = " + clickedObject.gameObject.layer);

        if (nextClickPlace == false)
        {
            if (clickedObject == fuelCanister)
            {
                playerStats.pickUpOil();
                SoundManager.PlaySound("OilCanPickup", hit.point);
            }

            if (playerStats.hasOil == true)
            {
                if (clickedObject == factory1 || clickedObject == factory2)
                {
                    playerStats.submitOil();
                    SoundManager.PlaySound("OilCanSubmit", hit.point);
                }
            }

            if (clickedObject == bed)
            {
                sun.GetComponent<SunController>().ResetSun();
                playerStats.increaseDayByOne();
            }

            if (clickedObject.tag == "Plot")
            {
                uiManager.showBuyMenu(int.Parse(name.Substring(name.Length - 1)));
            }

            if (clickedObject == plotSpawner)
            {
                Debug.Log("plotSpanwerClicked");
                if (playerStats.moneyValue >= 10)
                {
                    bluePlot = false;
                    playerStats.changeMoneyBy(-10);
                    nextClickPlace = true;
                    plotPrefabMesh.enabled = true;
                    Debug.Log("Plot was bought");

                    holdPlot = GameObject.Find("RedPlot");
                    holdPlotMesh = holdPlot.GetComponent<MeshRenderer>();
                    holdPlotMesh.enabled = true;
        
                }
            }

            if (clickedObject == bluePlotSpawner)
            {
                if (playerStats.moneyValue >= 0)
                {
                    bluePlot = true;
                    playerStats.changeMoneyBy(-100);
                    nextClickPlace = true;
                    //plotPrefabMesh.enabled = true;
                
                }
            }
        }

        if (nextClickPlace == true)
        {
            Debug.Log("1");
            if(bluePlot == false)
            {
                if (clickedObject.layer == 6)
                {
                    var newPrefab = Instantiate(plotPrefab, hit.point, Quaternion.identity);
                    newPrefab.name = "Plot" + ++plots;
                    nextClickPlace = false;
                    Debug.Log("2");
                    holdPlot = GameObject.Find("RedPlot");
                    holdPlotMesh = holdPlot.GetComponent<MeshRenderer>();
                    holdPlotMesh.enabled = false;
                }
            }
            else
            {
                if (clickedObject.layer == 6)
                {
                    var newPrefab = Instantiate(bluePlotPrefab, hit.point, Quaternion.identity);
                    newPrefab.name = "Plot" + ++plots;
                    nextClickPlace = false;
                    Debug.Log("2");
                    holdPlot = GameObject.Find("RedPlot");
                    holdPlotMesh = holdPlot.GetComponent<MeshRenderer>();
                    holdPlotMesh.enabled = false;
                }
            }
            
        }
    }

    public void RightClickInteraction(GameObject RightClickedObject, RaycastHit hit)
    {
        if (RightClickedObject.tag == "PlantTreeArea")
        {
            if (playerStats.moneyValue < 20) return;
            Instantiate(treePrefab, hit.point, Quaternion.identity);
            playerStats.changeMoneyBy(-20);
            playerStats.changePollutionAmount(-10);
            SoundManager.PlaySound("TreeGrowth", hit.point);
        }
        else if (RightClickedObject.tag == "Factory")
        {
            uiManager.ShowFactoryMenu();
        }
        else if (RightClickedObject.tag == "Village")
        {
            //uiManager.showVillageMenu();
        }
        else
            Debug.Log("Not plant area");
    }

    public void CreateBuilding(int site, int type)
    {

        GameObject plot = GameObject.Find("Plot" + site);

        int prevBuildingType = playerStats.plotTypes[site - 1];
        if (prevBuildingType != 0)
        {
            GameObject prevBuilding = plot.transform.Find("Building00" + prevBuildingType).gameObject;
            prevBuilding.SetActive(false);
            playerStats.DecreaseBuildingCount(prevBuildingType);
            PrintArr(playerStats.buildingCounts);
        }

        GameObject curBuilding = plot.transform.Find("Building00" + (type)).gameObject;
        curBuilding.SetActive(true);

        // Increase number of the created building type
        playerStats.IncreaseBuildingCount(type);

        // Update building type on the plots
        // TODO: Check list range, extend if needed, or still use this if limited plot num
        playerStats.plotTypes[site - 1] = type;

        print("selected for " + "Plot" + site + " Building00" + (type));
    }

    public void PrintDictionary(Dictionary<int, int> dict)
    {
        foreach (KeyValuePair<int, int> kvp in dict)
        {
            print("<color=red>Dict: " + kvp.Key + " " + kvp.Value + "</color>");
        }
    }

    public void PrintArr(int[] arr)
    {
        string ret = "";
        foreach (int val in arr)
        {
            ret = ret + " " + val;
        }
        print("<color=red>Dict: " + ret + "</color>");
    }

    // Update is called once per frame
    void Update() { 
    if (playerStats.moneyValue >= 1000)
     {
      SceneManager.LoadScene(3);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.Confined;



     }
     if (playerStats.day == 5)
     {
      SceneManager.LoadScene(2);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.Confined;



     }

    }

 



}
