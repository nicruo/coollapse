using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private GameManager GameManagerScript;
    public GameObject gameManagerObject;

    void Start()
    {
        GameManagerScript = gameManagerObject.GetComponent<GameManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Pink")
        {
            GameManagerScript.pinkCounter += 1;
        }
        if (tag == "Gold")
        {
            GameManagerScript.SpeedMultiplier += 0.5f;
        }
        gameObject.SetActive(false);
    }
}