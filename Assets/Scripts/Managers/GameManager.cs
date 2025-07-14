using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private Player player; // reference to script "Player.cs"

    public Player Player => player; // prop to use 'player' variable


    private void Update()
    {
        /* Debugging purposes
        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            player.ResetPlayer();
        }
        */
    }

    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }
}
