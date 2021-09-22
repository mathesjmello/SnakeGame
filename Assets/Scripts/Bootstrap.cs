
using DefaultNamespace;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<Bootstrap>().gameObject;
                if (obj == null)
                {
                    obj = new GameObject("Bootstrap");
                    obj.AddComponent<Bootstrap>();
                }
                _instance = obj.GetComponent<Bootstrap>();
            }

            return _instance;
        }
    }

    private static Bootstrap _instance;

    public Player Player;
    public Matrix Matrix;
    public GameManager GM;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        Player = FindObjectOfType<Player>();
        Matrix = FindObjectOfType<Matrix>();
        GM = FindObjectOfType<GameManager>();
    }
}