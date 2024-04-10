using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CountDownSystem : MonoBehaviour
{
    [Header("Start")]
    public float startTime;
    public TextMeshProUGUI startTMPro;
    public Image panel;

    [Header("CountDown")]
    public GameObject gamingCanvas;
    public float countDownTime; 
    public TextMeshProUGUI timeTMPro;
    public Color startColor;
    public Color endColor;

    [Header("End")]
    public GameObject endCanvas;
    public TextMeshProUGUI scoreTMPro;

    private BinManager binManager;
    private ItemBar itemBar;

    private void Awake()
    {       
        binManager = GameObject.FindGameObjectWithTag("BinManager").GetComponent<BinManager>();
        itemBar = GameObject.FindGameObjectWithTag("ItemBar").GetComponent<ItemBar>();
        binManager.enabled = false;
        itemBar.enabled = false;        
        endCanvas.SetActive(false);
    }
    private void Start()
    {
        TouchSystem.instance.enabled = false;
        StartCoroutine(startCount()); // activate start game counter 
    }
    //Start game counter
    IEnumerator startCount()
    {
        AudioManager.instance.PlaySound("startCount");

        float timer = startTime;
        Color currentColor = startColor;
        while(timer > 0)
        {
            startTMPro.text = Mathf.RoundToInt(timer).ToString(); 
            yield return new WaitForSeconds(1f);
            timer--;

        }
        startTMPro.enabled = false;
        panel.enabled = false;

        binManager.enabled = true;
        itemBar.enabled = true;
        TouchSystem.instance.enabled = true;

        StartCoroutine(CountDown()); // activate end game counter
    }

    //End game counter
    IEnumerator CountDown()
    {
        AudioManager.instance.PlaySound("Counting");

        float timerInSec = countDownTime;

        float dr = endColor.r - startColor.r;
        float dg = endColor.g - startColor.g;
        float db = endColor.b - endColor.b;

        while(timerInSec >= 0)
        {
            timeTMPro.text = Display(timerInSec); //Time Display

            //Color Change
            float r = Camera.main.backgroundColor.r + dr * Time.deltaTime / countDownTime;
            float g = Camera.main.backgroundColor.g + dg * Time.deltaTime / countDownTime;
            float b = Camera.main.backgroundColor.b + db * Time.deltaTime / countDownTime;
            Camera.main.backgroundColor = new Color(r, g, b, Camera.main.backgroundColor.a);

            yield return new WaitForSeconds(0);
            timerInSec -= Time.deltaTime;
        }

        AudioManager.instance.StopSound("Counting");

        TimeStop();
        
    }
    //Display timer (MM:SS:FF)
    private string Display(float currentTimeInSec)
    {
        float min = currentTimeInSec / 60f;
        float sec = currentTimeInSec % 60f;
        float msec = currentTimeInSec * 60f % 60f;

        return num2time(min) + ":" + num2time(sec) + ":" + num2time(msec);
    }

    //Convert minutes / seconds / miniseconds in terms of XX
    private string num2time(float num)
    {
        if(Mathf.FloorToInt(num) < 10f)
        {
            return "0" + Mathf.FloorToInt(num).ToString();
        }else
        {
            return Mathf.FloorToInt(num).ToString();
        }
    }

    //action when the game ends
    private void TimeStop()
    {
        ScoreSavingSystem.instance.SaveScore(ScoreSystem.instance.Score);
        scoreTMPro.text = ScoreSystem.instance.ScoreTMPro.text;

        endCanvas.SetActive(true); 
        gamingCanvas.SetActive(false);
        TouchSystem.instance.enabled = false;

        AudioManager.instance.PlaySound("PanelPop");

    }
}
