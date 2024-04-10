using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    public TextMeshProUGUI ScoreTMPro;
    private void Start()
    {
        instance = this;
    }

    [SerializeField]
    private int initialScore;
    //Current Score, the score will automatically set to 0 if the score is negative (Property)
    public int Score
    {
        get
        {
            return initialScore;
        }

        set
        {
            if(value < 0)
            {
                initialScore = 0;
            }else
            {
                initialScore = value;
            }
        }
    }

    private void Update()
    {
        ScoreTMPro.text = "Score: " + Score.ToString(); //Show the current score
    }

    //Reduce Score if wrong match
    //Formula:
    //Score = currentScore - wrongMatchScore
    public int wrongMatchScore;
    public void SetWrongMatchScore()
    {
        Score -= wrongMatchScore;
    }

    public class RubbishRecord
    {
        public string name;
        public int count;

        //Constructor
        public RubbishRecord(string _name, int _count)
        {
            name = _name;
            count = _count;
        }

    }
    //Add Score when the bins are recycled
    //Formula :
    //Score = 10 * (n0 * 2^(n0 - 1) + n1 * 2^(n1 - 1) + ..... )
    //, where n0, n1, .... is the number of same rubbish(score will not be added when n = 0)
    public void SetRecycleScore(List<BinBehaviour> matchedBins)
    {
        List<RubbishRecord> rubbishRecords = new List<RubbishRecord>();
        foreach(BinBehaviour bin in matchedBins)
        {
            string rubbishName = bin.rubbish.GetComponent<RubbishBehaviour>().rubbishName;
            bool haveThisRubbish = false;
            foreach (RubbishRecord record in rubbishRecords)
            {                
                if(rubbishName == record.name)
                {
                    record.count += 1;
                    haveThisRubbish = true;
                    continue;
                }                
            }

            if(!haveThisRubbish)
            {
                rubbishRecords.Add(new RubbishRecord(rubbishName, 1));
            }
        }
        
        foreach(RubbishRecord record in rubbishRecords)
        {
            Score += 10 * record.count * (int)Mathf.Pow(2, record.count - 1);
        }

    }

}


