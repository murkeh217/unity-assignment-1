using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine.UIElements;


public class LeaderboardController : MonoBehaviour //, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject leaderboardRow;
    //[SerializeField] private int totalRows = 10;
    private GameObject go;
    private float y = 1.0f;

    private Camera mainCamera;

    //int head = 0;

    public TextAsset highscores;


    public ScrollRect scrollingRect;
    
    
    

    /*
    #region Child Components

    [SerializeField] ScrollRect ScrollRect;
    [SerializeField] RectTransform viewPortT;
    [SerializeField] RectTransform DragDetectionT;
    [SerializeField] RectTransform ContentT;
    //[SerializeField] ListViewItemPool ItemPool;

    #endregion
    
    #region Layout Parameters

    [SerializeField] float ItemHeight = 1;      // TODO: Replace it with dynamic height
    [SerializeField] int BufferSize;

    #endregion



    #region Layout Variables

    int TargetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(viewPortT.rect.height / ItemHeight), 0); } }
    int TopItemOutOfView { get { return Mathf.CeilToInt(ContentT.anchoredPosition.y / ItemHeight); } }

    float dragDetectionAnchorPreviousY = 0;

    #endregion



    #region Data

    ListViewItemModel[] data;
    int dataHead = 0;
    int dataTail = 0;

    #endregion
    
     public void Setup(ListViewItemModel[] data)
    {
        ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);

        this.data = data;

        DragDetectionT.sizeDelta = new Vector2(DragDetectionT.sizeDelta.x, this.data.Length * ItemHeight);

        for(int i = 0; i < TargetVisibleItemCount + BufferSize; i++)
        {
            GameObject itemGO = ItemBorrow();
            itemGO.transform.SetParent(ContentT);
            itemGO.SetActive(true);
            itemGO.transform.localScale = Vector3.one;
            itemGO.GetComponent<ListViewItem>().Setup(data[dataTail]);
            dataTail++;
        }
    }



    #region UI Event Handling

    public void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
    {
        float dragDelta = DragDetectionT.anchoredPosition.y - dragDetectionAnchorPreviousY;

        ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y + dragDelta);

        UpdateContentBuffer();

        dragDetectionAnchorPreviousY = DragDetectionT.anchoredPosition.y;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDetectionAnchorPreviousY = DragDetectionT.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    #endregion



    #region Infinite Scroll Mechanism

    void UpdateContentBuffer()
    {
        if(TopItemOutOfView > BufferSize)
        {
            if(dataTail >= data.Length)
            {
                return;
            }

            Transform firstChildT = ContentT.GetChild(0);
            firstChildT.SetSiblingIndex(ContentT.childCount - 1);
            firstChildT.gameObject.GetComponent<ListViewItem>().Setup(data[dataTail]);
            ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y - firstChildT.gameObject.GetComponent<ListViewItem>().ItemHeight);
            dataHead++;
            dataTail++;
        }
        else if(TopItemOutOfView < BufferSize)
        {
            if(dataHead <= 0)
            {
                return;
            }

            Transform lastChildT = ContentT.GetChild(ContentT.childCount - 1);
            lastChildT.SetSiblingIndex(0);
            dataHead--;
            dataTail--;
            lastChildT.gameObject.GetComponent<ListViewItem>().Setup(data[dataHead]);
            ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y + lastChildT.gameObject.GetComponent<ListViewItem>().ItemHeight);

        }
    }

    #endregion
*/


    private void OnEnable()
    {
        //head = 0;

        //PoolManager.InitializePool(leaderboardRow, 100, false);


        HighScores playersInJson = JsonUtility.FromJson<HighScores>(highscores.text);

        foreach (HighScore player in playersInJson.highscores)
        {
            go = PoolManager.Instantiate(leaderboardRow);
            go.transform.SetParent(transform, false);

            //spawning in a line of y axis with spacing
            float currentPos = go.transform.position.y;
            go.transform.Translate(0f, currentPos - y, 0f);
            y += 0.5f;

            go.transform.GetChild(0).GetComponent<Text>().text = player.rank.ToString();
            go.transform.GetChild(1).GetComponent<Text>().text = player.name;
            go.transform.GetChild(2).GetComponent<Text>().text = player.score.ToString();
        }
        
        var planeHeight = scrollingRect.GetComponent<RectTransform>().rect.height;
        var viewPortHeight = scrollingRect.viewport.rect.height;

        var topPixels = scrollingRect.verticalNormalizedPosition * planeHeight;
        
        var elementHeight = 100;
        var spacingHeight = 10;
        var leadingHiddenCount = Mathf.FloorToInt(topPixels / (elementHeight + spacingHeight));
        var visibleCount = Mathf.CeilToInt(viewPortHeight / (elementHeight + spacingHeight));
        // first Y position of visible element
        var topPadding = leadingHiddenCount * (elementHeight + spacingHeight);

        for (var i = 0; i < visibleCount; i++) {
            var j = leadingHiddenCount + i;
            if (j > playersInJson.highscores.Length) break;
            add(playersInJson.highscores[j], topPadding + j * (elementHeight + spacingHeight));
        }
        //PoolManager.Destroy(go);
    }


/*
public GameObject ItemBorrow()
{
    if(head >= totalRows) {
        return null;
    }

    head++;
    return this.transform.GetChild(0).gameObject;
}

public void ItemReturn(GameObject go)
{
    if(head <= 0) {
        return;
    }

    head--;
    PoolManager.Destroy(go);
    go.transform.SetParent(this.transform);
}
*/
    private void OnDisable()
    {
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

// Update is called once per frame
    void Update()
    {
        /*
                Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
                if (screenPosition.y > Screen.height || screenPosition.y < 0)
                {
                    PoolManager.Destroy(go);
                }*/
    }
}