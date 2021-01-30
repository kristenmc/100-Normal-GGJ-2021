﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;


/// <summary>
/// Moddifed version of code found at https://videlais.com/2019/07/10/unity-ink-part-4-tags-and-rich-text/ by  Dan Cox 
/// This code takes the ink files and displays them onto a canvas while also making choices avaiable as buttons (better explained at the link)
/// this moddifed verison add some code that continues looking through the ink file as long as the story can continue but no
/// text was found, so it refreshes again to see to capture the next text
/// </summary>



public class ShopMenu : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Load the next story block
        story = new Story(inkJSONAsset.text);

        // Start the refresh cycle
        refresh();

    }

    // Refresh the UI elements
    //  – Clear any current elements
    //  – Show any text chunks
    //  – Iterate through any choices and create listeners on them
    void refresh()
    {
        // Clear the UI
        clearUI();
        // Create a new GameObject
        GameObject newGameObject = new GameObject("TextChunk");
        // Set its transform to the Canvas (this)
        newGameObject.transform.SetParent(this.transform, false);

        // Add a new Text component to the new GameObject
        Text newTextObject = newGameObject.AddComponent<Text>();
        // Set the fontSize larger
        newTextObject.fontSize = 24;
        // Set the text from new story block
        newTextObject.text = getNextStoryBlock();

        Debug.Log(newTextObject.text);

        // Load Arial from the built-in resources
        newTextObject.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            // Gets the text from the button prefab
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            // Set listener
            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

        }

        //if there is no text to put onto the canvas it will continue to the next part of the story
        if (newTextObject.text.Length == 1)
            refresh();

        checkTags();
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refresh();
    }

    // Clear out all of the UI, calling Destory() in reverse
    void clearUI()
    {
        int childCount = this.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }


    // Load and potentially return the next story block
    string getNextStoryBlock()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.Continue();
        }

        return text;
    }


     void checkTags()
    {
        List<string> tags = story.currentTags;
        if (tags.Count == 1)
        {   
            //checks if players can buy anything at the shop
            if (tags[0] == "check_gorbage")
            {
                //get_gorbage() > 0
                //story.variablesState["haveGorbage"] = true;
                //setGorbage(getGorbage() - 1);
                //else
                //story.variablesState["haveGorbage"] = false;
            }
            //adds food to the player resource 
            else if (tags[0] == "food")
            {
                //setfood(getFood() + 1);
            }
            //adds water to the player resource
            else if (tags[0] == "water")
            {
                //setWater(getWater() + 1);
            }
            //This resets the canvas and prepares for the next visit
            else if (tags[0] == "end")
            {   
                restartStory();
                refresh();
                this.GetComponent<Canvas>().enabled = false;
            }
        }
    }
    
    void restartStory()
    {
        story.ResetState();
    }

}