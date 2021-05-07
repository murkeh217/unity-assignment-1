using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LeaderboardController : MonoBehaviour //, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //private GameObject leaderboardRow;

    //private GameObject go;

    //private float y = 1.0f;

    public TextAsset highscores;

//    private ScrollRect scrollingRect;
/*
    private float planeHeight, viewPortHeight, topPixels;

    int elementHeight = 100;
    int spacingHeight = 10;

    private int leadingHiddenCount, visibleCount, topPadding;*/


    public struct LeaderboardCells
    {
        public string rank;
        public string name;
        public string score;

        public LeaderboardCells(string r, string n, string s)
        {
            rank = r;
            name = n;
            score = s;
        }
    }

    public RecyclingListView theList;
    
    private List<LeaderboardCells> data = new List<LeaderboardCells>();

    private void Start()
    {
        theList.ItemCallback = PopulateItem;

        RetrieveData();

        // This will resize the list and cause callbacks to PopulateItem for
        // items that are needed for the view
        theList.RowCount = data.Count;
    }

    private void RetrieveData()
    {
        data.Clear();
        var row = 0;

        // You'd obviously load real data here
        HighScores highscoresInJson = JsonUtility.FromJson<HighScores>(highscores.text);

        foreach (HighScore highscore in highscoresInJson.highscores)        
        {
            data.Add(new LeaderboardCells(highscore.rank.ToString(),highscore.name,highscore.score.ToString()));
        }
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as LeaderboardRow;
        //ChildData is a LeaderboardRow type. You're trying to assign something from data which is a list that holds LeaderboardCells. They're incompatible types
        child.ChildData = data[rowIndex];
    }

/*
    private void Awake()
    {
        
    }

    void OnEnable()
    {
        scrollingRect.onValueChanged.AddListener(ListenerMethod);
    }

    public void ListenerMethod(Vector2 value)
    {
        
        HighScores highscoresInJson = JsonUtility.FromJson<HighScores>(highscores.text);

        
        
        planeHeight = scrollingRect.GetComponent<RectTransform>().rect.height;
        viewPortHeight = scrollingRect.viewport.rect.height;
        topPixels = scrollingRect.verticalNormalizedPosition * planeHeight;

        leadingHiddenCount = Mathf.FloorToInt(topPixels / (elementHeight + spacingHeight));
        visibleCount = Mathf.CeilToInt(viewPortHeight / (elementHeight + spacingHeight));


        // first Y position of visible element
        topPadding = leadingHiddenCount * (elementHeight + spacingHeight);
        

        
        
        //foreach (HighScore highscore in highscoresInJson.highscores)
        //{
        for (int i = 0; i < 10; i++)
        {
            go = PoolManager.Instantiate(leaderboardRow); //, pos, Quaternion.identity);
            //necessary for proper alignment of go on canvas
            go.transform.SetParent(transform, false);

            //spawning in a line of y axis with spacing
            float currentPos = go.transform.position.y;
            go.transform.Translate(0f, currentPos - y, 0f);
            y += 0.5f;

            //adding json values
            go.transform.GetChild(0).GetComponent<Text>().text = highscoresInJson.highscores[i].rank.ToString();
            go.transform.GetChild(1).GetComponent<Text>().text = highscoresInJson.highscores[i].name;
            go.transform.GetChild(2).GetComponent<Text>().text = highscoresInJson.highscores[i].score.ToString();
        }

        if (value.y > 0.5f)
        {
           
            PoolManager.Destroy(go);
        }
        /*
 for (var k = 0; k < visibleCount; k++)
            {
                var j = leadingHiddenCount + k;
                if (j > highscoresInJson.highscores.Length) break;
                
                //AddAnElement(highscoresInJson.highscores[j], topPadding + j * (elementHeight + spacingHeight));
            }
     */
    //}
    //PoolManager.Destroy(go);

    /*
        var planeHeight = scrollingRect.GetComponent<RectTransform>().rect.height;
        var viewPortHeight = scrollingRect.viewport.rect.height;

        var topPixels = scrollingRect.verticalNormalizedPosition * planeHeight;

        var elementHeight = 100;
        var spacingHeight = 10;
        var leadingHiddenCount = Mathf.FloorToInt(topPixels / (elementHeight + spacingHeight));
        var visibleCount = Mathf.CeilToInt(viewPortHeight / (elementHeight + spacingHeight));
        // first Y position of visible element
        var topPadding = leadingHiddenCount * (elementHeight + spacingHeight);



        for (var i = 0; i < visibleCount; i++)
        {
            var j = leadingHiddenCount + i;
            if (j > highscoresInJson.highscores.Length) break;
            
            Vector3 pos = transform.position; // get a copy

            pos.x = 0;
            pos.y = topPadding + j * (elementHeight + spacingHeight);
            pos.z = 0;

            transform.position = pos; // now put it back and override the vector3 with the new one.

            //AddAnElement(highscoresInJson.highscores[j], topPadding + j * (elementHeight + spacingHeight));
        }
*/
    
}