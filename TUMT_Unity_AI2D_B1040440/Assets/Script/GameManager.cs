using UnityEngine;
using UnityEngine.SceneManagement;

namespace LU
{
    public class GameManager : MonoBehaviour
    {
        public void Replay()
        {
            //Application.LoadLevel("遊戲");
            SceneManager.LoadScene("遊戲");
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}