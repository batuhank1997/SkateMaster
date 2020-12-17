using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "LevelCount")
            gameObject.GetComponent<TextMeshProUGUI>().text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        else if(gameObject.tag == "CurrentLevel")
            gameObject.GetComponent<TextMeshProUGUI>().text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        else if(gameObject.tag == "NextLevel")
            gameObject.GetComponent<TextMeshProUGUI>().text = (SceneManager.GetActiveScene().buildIndex + 2).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
