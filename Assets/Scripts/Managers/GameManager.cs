using UnityEngine;

public class GameManager : MonoBehaviour
{
    // create new instance using singleton
    public static GameManager Instance;

    [SerializeField] private Player player; // reference to script "Player.cs"

    public Player Player => player; // prop to use 'player' variable

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            player.ResetPlayer();
        }
    }

    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }
}
