using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player; // reference to script "Player.cs"

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            player.ResetPlayer();
        }
    }
}
