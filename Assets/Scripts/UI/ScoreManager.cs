using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int[] scores;
    private void Awake() //this happens when this object is first loaded
    {
        Instance = this;
        scores = new int[GameManager.Instance.teams.Length];//We set up our scores to have the size of the number of teams in our GameManager
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
