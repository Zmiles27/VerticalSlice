using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] public PickupHolder pickupHolder;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private TextMeshProUGUI CoinCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = pickupHolder.score.ToString(); // zet de text om in de string
        CoinCounter.text = pickupHolder.coin.ToString(); // hetzelfde maar dan met een coin
    }
}
