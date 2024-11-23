using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject bigMap;
    [SerializeField] private GameObject miniMap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (miniMap.activeSelf == true){
                miniMap.SetActive(false);
                bigMap.SetActive(true);
            }
            else{
                miniMap.SetActive(true);
                bigMap.SetActive(false);
            }
        }
    }
}
