using TMPro;
using UnityEngine;

public class PickupVisual : MonoBehaviour
{
    [SerializeField] public PickupHolder pickupHolder;
    [SerializeField] private TextMeshProUGUI HighScoreText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = pickupHolder.score.ToString(); // zet de text om in de string
    }
}
