using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float playerSpeed;
    public bool isGameStart;
    public PlayableDirector timeline;


    private void Awake()
    {
        II = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartTimeline()
    {
        //starts timeline
        timeline.Play();
        Debug.Log("Timeline started");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static GameManager II;
    public static GameManager I
    {
        get
        {
            if(II == null)
            {
                II = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return II;
        }
    }

}
