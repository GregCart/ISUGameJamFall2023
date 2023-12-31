using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeSceneScript : MonoBehaviour
{
    public static string[] ingrediants = {"White Bread", "Wheat Bread", "Turkey", "Chicken", "Bacon", "Lettuce", "Tomato", "Egg", "Mustard", "Mayonaise"};
    public static CafeSceneScript instance;
    [SerializeField] private List<string> curIngrediants;
    private List<string> selectedIngredients;
    public float fadeIn = 5.0f;
    public bool isDone = false;


    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        selectedIngredients = new List<string>();

        if (curIngrediants == null)
        {
            Debug.Log("Awake!");
            curIngrediants = new List<string>();
            generateSandwich();
        }
    }

    void Start()
    {
        selectedIngredients = new List<string>();

        if (curIngrediants.Count == 0)
        {
            curIngrediants = new List<string>();
            Debug.Log("Start!");
            generateSandwich();
        }
    }

    void Update() {
        fadeIn -= Time.deltaTime;

        if (fadeIn <= 0.0f && isDone)
        {
            timerEnded();
        }
    }

    public List<string> getCurrentIngrediants()
    {
        return curIngrediants;
    }

    void timerEnded()
    {
        SceneManager.LoadScene("Menu");
    }

    public string[] generateSandwich()
    {
        var random = new System.Random();
        var bread = ingrediants[random.Next(0, 2)];
        var sause = ingrediants[random.Next(8, 10)];
        var mid1 = ingrediants[random.Next(2, 8)];
        var mid2 = ingrediants[random.Next(2, 8)];

        curIngrediants.Clear();
        selectedIngredients.Clear();
        while (mid2 == mid1)
        {
            mid2 = ingrediants[random.Next(2, 8)];
        }

        curIngrediants.Add(bread);
        curIngrediants.Add(mid1);
        curIngrediants.Add(mid2);
        curIngrediants.Add(sause);

        Debug.Log(arrayToString(curIngrediants.ToArray()));

        return curIngrediants.ToArray();
    }

    public string arrayToString(object[] array)
    {
        var str = "[";
        foreach (object o in array)
        {
            str += " " + o.ToString() + ",";
        }
        str += "\b]";

        return str;
    }

    public bool checkSandwich()
    {
        if (curIngrediants.Count == selectedIngredients.Count)
        {
            bool[] correct = new bool[4];
            for (int i = 0; i < selectedIngredients.Count; i++)
            {
                correct[i] = curIngrediants.Contains(selectedIngredients[i]);
            }

            return correct[0] && correct[1] && correct[2] && correct[3];
        }

        Debug.Log(curIngrediants.Count.ToString() + "!=" + selectedIngredients.Count.ToString());
        return false;
    }

    public void updateIngrediants(string ing)
    {
        if (selectedIngredients.Contains(ing))
        {
            selectedIngredients.Remove(ing);
            Debug.Log("Remove " + ing);
        }
        else
        {
            selectedIngredients.Add(ing);
            Debug.Log("Add " + ing);
        }
    }

    public void submitOrder()
    {
        generateNote(checkSandwich());
    }

    public void generateNote(bool correct)
    {
        if (!correct)
        {
            Debug.Log("Not Club");
        }
        else
        {
            Debug.Log("Club!");
        }

        fadeIn = 5f;
        isDone = true;
    }
}
