using System.Collections.Generic;
using UnityEngine;

public class BinManager : MonoBehaviour
{
    [Header("Bin Settings")]
    public GameObject[] binPrefs;
    public Vector2 offset;

    [Header("Array Settings")]
    public int length = 5;
    public int width = 3;
    [HideInInspector]
    public BinBehaviour[,] bins;
    
    public enum BinColor { Brown, Blue, Yellow, Recycle } //Define BinColor

    private void Start()
    {
        //Initialization
        bins = new BinBehaviour[length, width];
        //Generate Bin 
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GenerateBin(i, j); 
            }
        }
    }

    //RandomGenerateBin
    private void GenerateBin(int i, int j)
    {
        int index = Random.Range(0, binPrefs.Length);
        //Calculate the position of the bin
        Vector3 pos = new Vector3(transform.position.x + offset.x * i, transform.position.y - offset.y * j);

        GameObject bin = Instantiate(binPrefs[index], pos, binPrefs[index].transform.rotation);
        bins[i, j] = bin.GetComponent<BinBehaviour>();
        bins[i, j].convertedPos = new Vector2(i, j);
    }

    private List<BinBehaviour> matchedBins = new List<BinBehaviour>();
    private BinColor matchedColor;
    private enum Dir { left , right, up , down , None }
    public void Recycle(int i, int j)
    {
        matchedBins.Clear();
        matchedBins.Add(bins[i, j]);
        matchedColor = bins[i, j].binColor;

        SearchBin(i, j, Dir.None);

        ScoreSystem.instance.SetRecycleScore(matchedBins);
        //Clear Bin
        foreach (BinBehaviour bin in matchedBins)
        {

            int x = (int)bin.convertedPos.x;
            int y = (int)bin.convertedPos.y;
          
            Instantiate(bin.destroyParticle, bin.transform.position, bin.transform.rotation); //Instantiate particle effect
            bin.GetComponent<PopingAnimation>().PopOutAnimation(); //Pop Out animation

            Destroy(bin.rubbish);
            Destroy(bin.gameObject);

            GenerateBin(x, y);
        }

        AudioManager.instance.PlaySound("Break");
    }

    //using Recursion to search same color of bins nearby 
    private void SearchBin(int i, int j, Dir lockDir)
    {

       //Search Right
       if(i + 1 < length && j < width && lockDir != Dir.right)
        {
            BinBehaviour bin = bins[i + 1, j];
            if (bin.binColor == matchedColor && bin.rubbish != null && !FindObjectExistInList(matchedBins, bin))
            {
                matchedBins.Add(bin);
                SearchBin(i + 1, j, Dir.left);
            }
        }

        //Search Down
        if (i < length && j + 1 < width && lockDir != Dir.down)
        {
            BinBehaviour bin = bins[i, j + 1];
            if (bin.binColor == matchedColor && bin.rubbish != null && !FindObjectExistInList(matchedBins, bin))
            {
                matchedBins.Add(bin);
                SearchBin(i, j + 1, Dir.up);
            }
        }

        //Search Left
        if (i - 1 >= 0 && j < width && lockDir != Dir.left)
        {
            BinBehaviour bin = bins[i - 1, j];
            if (bin.binColor == matchedColor && bin.rubbish != null && !FindObjectExistInList(matchedBins, bin))
            {
                matchedBins.Add(bin);
                SearchBin(i - 1, j, Dir.right);
            }
        }

        //Search Up
        if (i < length && j - 1 >= 0 && lockDir != Dir.up)
        {
            BinBehaviour bin = bins[i , j - 1];
            if (bin.binColor == matchedColor && bin.rubbish != null && !FindObjectExistInList(matchedBins, bin))
            {
                matchedBins.Add(bin);
                SearchBin(i, j - 1, Dir.down);
            }             
        }
    }
    
    //Return true if the object is exist in the list, vice versa
    private bool FindObjectExistInList(List<BinBehaviour> list, BinBehaviour _object)
    {
        foreach(BinBehaviour listObject in list)
        {
            if(listObject.convertedPos == _object.convertedPos)
            {
                return true;
            }
        }

        return false;
    }

}
