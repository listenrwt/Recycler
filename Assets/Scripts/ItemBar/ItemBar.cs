using UnityEngine;
using System.Collections.Generic;

public class ItemBar : MonoBehaviour
{
    [Header("Initialization_Objects")]
    public Transform[] grids;
    public GameObject[] rubbishPrefs;

    [HideInInspector]
    public ItemGrid activeGrid = null;

    private void Start()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            //creat new grid 
            ItemGrid rubbishGrid = grids[i].gameObject.AddComponent<ItemGrid>();
            //Constructor cannot be used with MonoBehaviour
            rubbishGrid.grid = grids[i];
            rubbishGrid.item = GenerateRubbish(i);
            rubbishGrid.SpriteRendererSetUp();
        }
    }

    //Generate random rubbish using grid number (overloading method)
    private GameObject GenerateRubbish(int gridNum)
    {
        int index = Random.Range(0, rubbishPrefs.Length);
        GameObject rubbish = (GameObject)Instantiate(rubbishPrefs[index], grids[gridNum].position, rubbishPrefs[index].transform.rotation);

        return rubbish;
    }
    //Generate random rubbish with RubbishGrid Class (overloading method)
    private GameObject GenerateRubbish(ItemGrid rg)
    {
        int index = Random.Range(0, rubbishPrefs.Length);
        GameObject rubbish = (GameObject)Instantiate(rubbishPrefs[index], rg.grid.position, rubbishPrefs[index].transform.rotation);

        return rubbish;
    }

    //Return true if there is any valid grid, vice versa
    private bool HaveValidGrid()
    {
        BinManager binManager = GameObject.FindGameObjectWithTag("BinManager").GetComponent<BinManager>();
        List<BinManager.BinColor> binColor = new List<BinManager.BinColor>();

        foreach(BinBehaviour binBehaviour in binManager.bins)
        {
            if (binBehaviour.rubbish != null)
            {
                foreach(Transform grid in grids)
                {
                    BinManager.BinColor matchedColor = grid.GetComponent<ItemGrid>().item.GetComponent<RubbishBehaviour>().matchedColor;
                    if (matchedColor == BinManager.BinColor.Recycle)
                        return true;
                }

                continue;
            }
                

            bool haveColor = false;
            foreach (BinManager.BinColor color in binColor)
            {
                if(color == binBehaviour.binColor)
                {
                    haveColor = true;
                    break;
                }                   
            }

            if (!haveColor)
                binColor.Add(binBehaviour.binColor);

            if (binColor.Count == 3)
                break;
        }

        bool haveMatchedColor = false; ;
        foreach (Transform grid in grids)
        {
            foreach(BinManager.BinColor color in binColor)
            {
                BinManager.BinColor matchedColor = grid.GetComponent<ItemGrid>().item.GetComponent<RubbishBehaviour>().matchedColor;
                
                if (matchedColor == color)
                {
                    haveMatchedColor = true;
                    break;
                }
            }
        }

        return haveMatchedColor;

    }

    //Grid filling
    private void GridFilling()
    {
        if (activeGrid != null)
        {
            if (activeGrid.item == null)
            {
                activeGrid.item = GenerateRubbish(activeGrid);
            }
        }
    }

    private void Update()
    {        
        GridFilling();
        //Refresh if no vaild grid
        if (!HaveValidGrid())
        {
            for (int i = 0; i < grids.Length; i++)
            {
                Destroy(grids[i].GetComponent<ItemGrid>().item);
                grids[i].GetComponent<ItemGrid>().item = GenerateRubbish(i);
            }
        }
    } 
}

public class ItemGrid : MonoBehaviour, ITouchable
{
    [HideInInspector]
    public GameObject item = null;
    [HideInInspector]
    public Transform grid = null;
    [HideInInspector]
    public SpriteRenderer sr = null;

    //Get the SpriteRenderer component
    public void SpriteRendererSetUp()
    {
        sr = grid.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    //Action when being touched
    public void TouchAction()
    {
        ItemBar itemBar = GameObject.FindGameObjectWithTag("ItemBar").GetComponent<ItemBar>();
        if(itemBar.activeGrid != this)
        {
            if (itemBar.activeGrid != null) itemBar.activeGrid.sr.enabled = false;

            itemBar.activeGrid = this;
            sr.enabled = true;
        }

        AudioManager.instance.PlaySound("Select");
    }
}
