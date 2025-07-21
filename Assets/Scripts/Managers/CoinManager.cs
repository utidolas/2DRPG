using BayatGames.SaveGameFree;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{

    public float Coins { get; private set; }
    private const string COIN_KEY = "CoinsPrefs"; // create a key to save coins in PlayerPrefs

    [SerializeField] public float coinsTest;
    private void Start()
    {
        Coins = SaveGame.Load(COIN_KEY, Coins);
        SaveGame.Delete(COIN_KEY); // delete coins data for testing purposes
    }

    public void AddCoins(float amount)
    {
        Coins += amount;
        SaveGame.Save(COIN_KEY, Coins);
    }

    public void RemoveCoins(float amount)
    {
        // check if we have enough coins before removing
        if (Coins >= amount)
        {
            Coins -= amount;
            SaveGame.Save(COIN_KEY, Coins);
        }
    }   
}
