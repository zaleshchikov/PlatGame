using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    public enum GameStates
    { // Just for readability
        Menu,
        Playing,
        Paused,
        GameOver
    }

    public class GameSession
    {

        GameStates _state = GameStates.Menu;

        public void SetGameState(GameStates state)
        {

            if (state != _state)
            {
                _state = state;
                switch (state)
                {
                    case GameStates.Menu:
                        // Show menu window or overlay here if not already open
                        // Pause game world time flow
                        // Start potential transitions here
                        break;
                    case GameStates.Playing:
                        // Resume game world time flow
                        break;
                    case GameStates.Paused:
                        // Pause game world time flow
                        // Maybe show game paused UI
                        break;
                    case GameStates.GameOver:
                        // Show game over screen or overlay
                        // Pause game world time flow (not strictly)
                        // Any interaction from player leads to state e.g. GameStates.Menu
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
