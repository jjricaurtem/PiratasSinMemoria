using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int numberOfCards;
    [SerializeField] private CardEventChannel cardEventChannel;

    // Use this for initialization
    void Start()
    {
        cardEventChannel.currentCardUp = null;
    }
}
