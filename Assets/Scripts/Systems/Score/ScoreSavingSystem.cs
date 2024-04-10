using UnityEngine;

public class ScoreSavingSystem : MonoBehaviour
{
    public static ScoreSavingSystem instance;
    private void Awake()
    {     
        //Singelton pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Ranking Initialization
        if (!PlayerPrefs.HasKey("previous"))
        {
            RecordReset();
        }

    }

    public void SaveScore(int score)
    {
        //SaveScore
        PlayerPrefs.SetInt("previous", score);

        //Ranking
        if(PlayerPrefs.GetInt("previous") > PlayerPrefs.GetInt("first"))
        {
            PlayerPrefs.SetInt("third", PlayerPrefs.GetInt("second"));
            PlayerPrefs.SetInt("second", PlayerPrefs.GetInt("first"));
            PlayerPrefs.SetInt("first", PlayerPrefs.GetInt("previous"));                                 
        }
        else if (PlayerPrefs.GetInt("previous") > PlayerPrefs.GetInt("second"))
        {
            PlayerPrefs.SetInt("third", PlayerPrefs.GetInt("second"));
            PlayerPrefs.SetInt("second", PlayerPrefs.GetInt("previous"));
        }else if (PlayerPrefs.GetInt("previous") > PlayerPrefs.GetInt("third"))
        {
            PlayerPrefs.SetInt("third", PlayerPrefs.GetInt("previous"));
        }
    }

    public void RecordReset()
    {
        PlayerPrefs.SetInt("first", -1);
        PlayerPrefs.SetInt("second", -1);
        PlayerPrefs.SetInt("third", -1);
        PlayerPrefs.SetInt("previous", -1);
    }

    private void Update()
    {
        //Only for debug perpose
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.T))
        {
            RecordReset();
            Debug.Log("Score Reset!");
        }
    }
}
