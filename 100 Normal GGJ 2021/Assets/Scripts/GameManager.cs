using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType {Food, Water, Gorbage, Shop, Animal}
public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;

    [SerializeField] GameObject earth;


    #region Procedural Generation Vars
    [SerializeField] GameObject foodMinigamePrefab;
    [SerializeField] GameObject waterMinigamePrefab;
    [SerializeField] GameObject gorbageMinigamePrefab;
    [SerializeField] GameObject shopPrefab;
    [SerializeField] GameObject animalPrefab;
    [SerializeField] GameObject[] interactParents;
    [SerializeField] GameObject[] interactGameObjects;
    [SerializeField] GameObject foodNodePrefab;
    [SerializeField] GameObject waterNodePrefab;
    [SerializeField] GameObject gorbageNodePrefab;
    [SerializeField] GameObject shopNodePrefab;
    [SerializeField] GameObject animalNodePrefab;
    [SerializeField] GameObject[] mapNodeParents;
    [SerializeField] float[] nodeWeights;
    [SerializeField] int numGeneratedFood = 0;
    [SerializeField] int numGeneratedWater = 0;
    [SerializeField] int numGeneratedGorbage = 0;
    [SerializeField] int numGeneratedShop = 0;
    [SerializeField] int numGeneratedAnimals = 0;
    #endregion

    #region Management Vars
    [SerializeField] bool minigameActive = false;
    [SerializeField] int foodAmt = 0;
    [SerializeField] int maxFoodAmt = 0;
    [SerializeField] int waterAmt = 0;
    [SerializeField] int maxWaterAmt = 0;
    [SerializeField] int gorbageAmt = 0;
    [SerializeField] int maxGorbageAmt = 0;
    [SerializeField] int animalAmt = 1;

    #endregion

    #region Minimap Vars
    [SerializeField] GameObject currentNode;
    [SerializeField] GameObject selectedNode;
    [SerializeField] GameObject[] currentNodeConnections;
    [SerializeField] GameObject[] selectedNodeConnections;
    [SerializeField] GameObject mapCanvas;
    [SerializeField] GameObject mapPointer;
    #endregion
    void Awake()
    {
        GameManagerInstance = this;
        currentNodeConnections = currentNode.GetComponent<NodeConnection>().getConnections();
        generateMinimap();
        updateMapPointer();
    }

    #region Generation Funcs
    public void spawnInteractables(float foodWeight, float waterWeight, float gorbageWeight, float shopWeight, float animalWeight, InteractType pity)
    {
        foreach(GameObject toDestroy in interactGameObjects)
        {
            Destroy(toDestroy);
        }
        numGeneratedFood = 0;
        numGeneratedWater = 0;
        numGeneratedGorbage = 0;
        numGeneratedShop = 0;
        numGeneratedAnimals = 0;

        float relativeFoodWeight = foodWeight;
        float relativeWaterWeight = relativeFoodWeight + waterWeight;
        float relativeGorbageWeight = relativeWaterWeight + gorbageWeight;
        float relativeShopWeight = relativeGorbageWeight + shopWeight;
        float relativeAnimalWeight = relativeShopWeight + animalWeight;

        for(int i = 0; i < interactParents.Length-1; i++)
        {
            generateInteractable(relativeFoodWeight, relativeWaterWeight, relativeGorbageWeight, relativeShopWeight, relativeAnimalWeight, i);
        }
        //Pity system, checks to see if the pity amount > 0 then force generates it if it is not
        if(pity == InteractType.Food && numGeneratedFood == 0)
        {
            interactGameObjects[interactParents.Length-1] = Instantiate(foodMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Water && numGeneratedWater == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(waterMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Gorbage && numGeneratedGorbage == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(gorbageMinigamePrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Shop && numGeneratedShop == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(shopPrefab, interactParents[interactParents.Length-1].transform);
        }
        else if(pity == InteractType.Animal && numGeneratedAnimals == 0)
        {
            interactGameObjects[interactParents.Length - 1] = Instantiate(animalPrefab, interactParents[interactParents.Length - 1].transform);
        }
        else
        {
            generateInteractable(relativeFoodWeight, relativeWaterWeight, relativeGorbageWeight, relativeShopWeight, relativeAnimalWeight, interactParents.Length-1);
        }
    }
    
    public void generateInteractable(float foodWeight, float waterWeight, float gorbageWeight, float shopWeight, float animalWeight, int iterator)
    {
        float randomlyGeneratedNumber = UnityEngine.Random.Range(0.0f, animalWeight);
        if (randomlyGeneratedNumber <= foodWeight)
        {
            numGeneratedFood++;
            interactGameObjects[iterator] = Instantiate(foodMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > foodWeight && randomlyGeneratedNumber <= waterWeight)
        {
            numGeneratedWater++;
            interactGameObjects[iterator] = Instantiate(waterMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > waterWeight && randomlyGeneratedNumber <= gorbageWeight)
        {
            numGeneratedGorbage++;
            interactGameObjects[iterator] = Instantiate(gorbageMinigamePrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > gorbageWeight && randomlyGeneratedNumber <= shopWeight)
        {
            numGeneratedShop++;
            interactGameObjects[iterator] = Instantiate(shopPrefab, interactParents[iterator].transform);
        }
        else if (randomlyGeneratedNumber > shopWeight && randomlyGeneratedNumber <= animalWeight)
        {
            numGeneratedAnimals++;
            interactGameObjects[iterator] = Instantiate(animalPrefab, interactParents[iterator].transform);
        }
    }
    
    public void generateMinimap()
    {
        foreach(GameObject node in mapNodeParents)
        {
            GameObject tempNode;
            float randomlyGeneratedNumber = UnityEngine.Random.Range(0.0f, nodeWeights[4]);
            if(randomlyGeneratedNumber <= nodeWeights[0])
            {
                tempNode = Instantiate(foodNodePrefab, node.transform);
            }
            else if(randomlyGeneratedNumber <= nodeWeights[1])
            {
                tempNode = Instantiate(waterNodePrefab, node.transform);
            }
            else if(randomlyGeneratedNumber <= nodeWeights[2])
            {
                tempNode = Instantiate(gorbageNodePrefab, node.transform);
            }
            else if(randomlyGeneratedNumber <= nodeWeights[3])
            {
                tempNode = Instantiate(shopNodePrefab, node.transform);
            }
            else
            {
                tempNode = Instantiate(animalNodePrefab, node.transform);
            }
            tempNode.GetComponent<RectTransform>().localPosition = new Vector3(
                tempNode.GetComponent<RectTransform>().localPosition.x, 
                tempNode.GetComponent<RectTransform>().localPosition.y, 
                1);
            tempNode.GetComponent<NodeLogic>().getParentConnections();
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

    public int getAnimals()
    {
        return animalAmt;
    }

    public void changeAnimals(int animal)
    {
        animalAmt += animal;
    }

    public GameObject getCurrentNode()
    {
        return currentNode;
    }

    public void setCurrentNode(GameObject node)
    {
        currentNode = node;
    }

    public GameObject getSelectedNode()
    {
        return selectedNode;
    }

    public void setSelectedNode(GameObject node)
    {
        selectedNode = node;
    }

    public GameObject[] getCurrentNodeConnections()
    {
        return currentNodeConnections;
    }

    public void setCurrentNodeConnections(GameObject[] connectionsList)
    {
        currentNodeConnections = connectionsList;
    }
    
    public GameObject[] getSelectedNodeConnections()
    {
        return selectedNodeConnections;
    }

    public void setSelectedNodeConnections(GameObject[] connectionsList)
    {
        selectedNodeConnections = connectionsList;
    }

    public void resetEarthRotation()
    {
        earth.GetComponent<MovementScript>().resetRotation();
    }
    #endregion

    #region Canvas Funcs
    public void openMap()
    {
        mapCanvas.SetActive(true);
    }
    
    public void closeMap()
    {
        mapCanvas.SetActive(false);
    }

    public void updateMapPointer()
    {
        mapPointer.GetComponent<RectTransform>().position = new Vector3(getCurrentNode().GetComponent<RectTransform>().position.x, 
            getCurrentNode().GetComponent<RectTransform>().position.y + 125, 
            0);
    }
    #endregion
}
