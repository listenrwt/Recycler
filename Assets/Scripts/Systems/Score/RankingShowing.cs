using UnityEngine;
using TMPro;

public class RankingShowing : MonoBehaviour
{
    public TextMeshProUGUI firstTMPro;
    public TextMeshProUGUI secondTMPro;
    public TextMeshProUGUI thirdTMPro;
    public TextMeshProUGUI previousTMPro;

    private void Start()
    {
        AudioManager.instance.PlaySound("PanelPop");

        //Display ranking
        if(firstTMPro != null)
        {
            firstTMPro.text = "1st " + ScoreDisplay(PlayerPrefs.GetInt("first"));
            secondTMPro.text = "2nd " + ScoreDisplay(PlayerPrefs.GetInt("second"));
            thirdTMPro.text = "3rd " + ScoreDisplay(PlayerPrefs.GetInt("third"));
            previousTMPro.text = ScoreDisplay(PlayerPrefs.GetInt("previous"));
        }
        
    }
    private string ScoreDisplay(int score)
    {
        if(score < 0)
        {
            return "-------"; //Shows "-------" if no record 
        }else
        {
            return score.ToString();
        }
    }

}
