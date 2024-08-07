using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        playerCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 2)) //If ray collides with something
            {
                gameManager.GetComponent<GameManager>().ObjectInteraction(hit.collider.gameObject, hit);
                Debug.Log(hit.collider.gameObject.name);
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 3)) //If ray collides with something
            {
                gameManager.GetComponent<GameManager>().RightClickInteraction(hit.collider.gameObject, hit);
                //Debug.Log(hit.collider.gameObject.name);

            }
        }
    }
}
