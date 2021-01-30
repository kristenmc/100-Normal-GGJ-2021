using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType {Food, Water, Gorbage, Shop}
public class GameManager : MonoBehaviour
{
    public static GameManager Game_Manager_Instance;

    #region Procedural Generation Vars
    [SerializeField] GameObject foodMinigamePrefab;
    [SerializeField] GameObject waterMinigamePrefab;
    [SerializeField] GameObject gorbageMinigamePrefab;
    [SerializeField] GameObject shopPrefab;
    [SerializeField] GameObject[] interactParents;
    [SerializeField] GameObject[] interactGameObjects; 
    private int numFood = 0;
    private int numWater = 0;
    private int numGorbage = 0;
    private int numShop = 0;
    #endregion

    #region Management Vars
    [SerializeField] bool minigameActive = false;
    [SerializeField] int foodAmt = 0;
    [SerializeField] int maxFoodAmt = 0;
    [SerializeField] int waterAmt = 0;
    [SerializeField] int maxWaterAmt = 0;
    [SerializeField] int gorbageAmt = 0;
    [SerializeField] int maxGorbageAmt = 0;
    
    #endregion
    void Awake()
    {
        Game_Manager_Instance = this;
    }

    #region Generation Funcs
    public void spawnInteractables(float foodWeight, float waterWeight, float gorbageWeight, float shopWeight, InteractType pity)
    {
        foreach(GameObject toDestroy in interactGameObjects)
        {
            Destroy(toDestroy);
        }
        numFood = 0;
        numWater = 0;
        numGorbage = 0;
        numShop = 0;

        float relativeFoodWeight = foodWeight;
        float relativeWaterWeight = relativeFoodWeight + waterWeight;
        float relativeGorbageWeight = relativeWaterWeight + gorbageWeight;
        float relativeShopWeight = relativeGorbageWeight + shopWeight;

        for(int i = 0; i < interactParents.Length-1; i++)
        {
            generateInteractable(relativeFoodWeight, relativeWaterWeight, relativeGorbageWeight, relativeShopWeight, i);
        }
        //Pity system, checks to see if the pity amount > 0 then force generates it if it is not
        if(pity == InteractType.Food && numFood == 0)
        {
            interactGameObjects[interactParents.Length-1] = Instantiate(foodMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Water && numWater == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(waterMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Gorbage && numGorbage == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(gorbageMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Shop && numShop == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(shopPrefab, interactParents[interactParents.Length-1].transform);
        }
        else
        {
            generateInteractable(relativeFoodWeight, relativeWaterWeight, relativeGorbageWeight, relativeShopWeight, interactParents.Length-1);
        }
    }
    
    public void generateInteractable(float foodWeight, float waterWeight, float gorbageWeight, float shopWeight, int iterator)
    {
        float randomlyGeneratedNumber = Random.Range(0.0f, shopWeight);
        if (randomlyGeneratedNumber <= foodWeight)
        {
            numFood++;
            interactGameObjects[iterator] = Instantiate(foodMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > foodWeight && randomlyGeneratedNumber <= waterWeight)
        {
            numWater++;
            interactGameObjects[iterator] = Instantiate(waterMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > waterWeight && randomlyGeneratedNumber <= gorbageWeight)
        {
            numGorbage++;
            interactGameObjects[iterator] = Instantiate(gorbageMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > gorbageWeight && randomlyGeneratedNumber <= shopWeight)
        {
            numShop++;
            interactGameObjects[iterator] = Instantiate(shopPrefab, interactParents[iterator].transform);
        }
    }
    #endregion

    #region Getters and Setters
    public bool getMinigameActivity()
    {
        return minigameActive;
    }

    public void setMinigameActivity(bool active)
    {
        minigameActive = active;
    }

    public int getFoodAmt()
    {
        return foodAmt;
    }

    public int getMaxFood()
    {
        return maxFoodAmt;
    }

    public void changeFood(int food)
    {
        foodAmt += food;
    }

    public int getWaterAmt()
    {
        return waterAmt;
    }

    public int getMaxWater()
    {
        return maxWaterAmt;
    }

    public void changeWater(int water)
    {
        waterAmt += water;
    }

    public int getGorbageAmt()
    {
        return gorbageAmt;
    }

    public int getMaxGorbage()
    {
        return maxGorbageAmt;
    }

    public void changeGorbage(int gorbage)
    {
        gorbageAmt += gorbage;
    }
    #endregion
}
