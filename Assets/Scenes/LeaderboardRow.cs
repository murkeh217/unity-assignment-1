using UnityEngine.UI;

public class LeaderboardRow : RecyclingListViewItem {
    public Text rank;
    public Text name;
    public Text score;
    
    private LeaderboardController.LeaderboardCells childData;
    
    public LeaderboardController.LeaderboardCells ChildData {
        get
        {
            return childData;
        }
        set 
        {
            childData = value;
            rank.text = childData.rank;
            name.text = childData.name;
            score.text = childData.score;
        }
    }
}