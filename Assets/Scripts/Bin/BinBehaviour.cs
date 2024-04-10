using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BinBehaviour : MonoBehaviour, ITouchable
{
    [Header("BinProperties")]
    public BinManager.BinColor binColor;
    public Transform printTransform;
    public GameObject destroyParticle;
    [HideInInspector]
    public GameObject rubbish = null;

    [HideInInspector]
    public Vector2 convertedPos;
    private ItemBar itemBar;
    private BinManager binManager;

    private void Start()
    {
        //Get the components
        itemBar = GameObject.FindGameObjectWithTag("ItemBar").GetComponent<ItemBar>();
        binManager = GameObject.FindGameObjectWithTag("BinManager").GetComponent<BinManager>();
    }

    //Check if the player matches the bin and the rubbish correctly
    private void ColorMatching(ItemGrid itemGrid)
    {
        RubbishBehaviour rub = itemGrid.item.GetComponent<RubbishBehaviour>();
        if (rub.matchedColor == binColor)
        {
            //Correct Matching  
            if(rubbish == null)
            {
                AudioManager.instance.PlaySound("Place");

                rubbish = itemGrid.item;

                //print the sprite of the rubbish at the bin 
                rubbish.GetComponent<PopingAnimation>().StopPopInAnimation();
                rubbish.transform.localScale *= rub.REDUCERATE;
                rubbish.transform.position = printTransform.position;

                itemGrid.item = null;
            }    

        }else if(rub.matchedColor == BinManager.BinColor.Recycle)
        {
            //Recycle
            if(rubbish != null)
            {
                //Start Recycle
                binManager.Recycle((int)convertedPos.x, (int)convertedPos.y);

                Destroy(itemGrid.item.gameObject);
                itemGrid.item = null;
            }
            
        }else
        {
            //Wrong Matching
            if(rubbish == null)
            {
                ScoreSystem.instance.SetWrongMatchScore();
                Handheld.Vibrate(); //Phone Vibrate
                AudioManager.instance.PlaySound("Wrong");
            }         
        }

    }

    public void TouchAction()
    {
        //Check does any grid is actived
        if (itemBar.activeGrid != null)
        {
            ColorMatching(itemBar.activeGrid);
        }
    }

}
