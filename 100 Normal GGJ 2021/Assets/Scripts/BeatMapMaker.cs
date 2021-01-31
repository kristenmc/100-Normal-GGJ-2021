using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapMaker : MonoBehaviour
{
    /*This is the class made to create beatMaps. For reference, a 0 implies no action should be taken on that particular half count, 
     *a 1 implies that the player needs to take a game action (pressing a button), and a 2 implies end of the item that is terminated with a game action
     * (for fishing, the time when the fish would be fully out of the water).*/

    [SerializeField] string smallFish = "02";
    [SerializeField] string BIGFish = "00101012";
    [SerializeField] string medFish = "00102";

    public List<string> endList;
    [SerializeField] List<string> listOfThings;

    private void Start()
    {
        listOfThings = new List<string>();
        listOfThings.Add(smallFish);
        listOfThings.Add(BIGFish);
        listOfThings.Add(medFish);
    }

    public string GenerateBeatMap(int length, List<string> typesOfThing)
    {
        endList = new List<string>();
        string endString = "";
        for (int i = 0; i < length; i++)
        {
            int randChoice = Random.Range(0, typesOfThing.Count);
            endString = endString + typesOfThing[randChoice];
        }


        return endString;
    }
    


}
