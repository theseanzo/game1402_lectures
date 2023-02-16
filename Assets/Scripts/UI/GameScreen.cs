using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    //Our game screen is going to have a few features: it will load an exit pop up menu (we will get to soon)
    //handle pausing
    //Show scores

    public TextMeshProUGUI textPrefab;
    private int numTeams;
    private TextMeshProUGUI[] scoreFields;
    void Start()
    {
        //we are going to instantiate the numTeams amount of textPrefabs in our layouit
        //step 1: get number of teams and make an array of that size
        numTeams = GameManager.Instance.teams.Length;
        scoreFields = new TextMeshProUGUI[numTeams];
        for(int i = 0; i < numTeams; i++)
        { 
            scoreFields[i] = Instantiate(textPrefab); //Instantiate a copy of the prefab
            scoreFields[i].transform.SetParent(textPrefab.transform.parent, false); //set the copies parent to be the same as the original prefab's
            scoreFields[i].color = GameManager.Instance.teams[i];//set the color to be the team color
        }
        //question: at this point, do we need the original textPrefab?
        Destroy(textPrefab.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numTeams; i++)
        {
            scoreFields[i].text = ScoreManager.Instance.scores[i].ToString();
        }
    }
}
