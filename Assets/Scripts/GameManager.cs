using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeCounter;
    public int pinkCounter;
    public int maxPink = 5;
    public GameObject gameLogo;

    public GameObject player;
    public GameObject mainCamera;

    public GameObject world;

    private Vector3 playerOriginalPosition;
    private Vector3 mainCameraOriginalPosition;

    public float SpeedMultiplier;

    public Text timeText;
    public Text pinkText;

    public bool gameRunning;

	void Start()
    {
        playerOriginalPosition = player.transform.position;
        mainCameraOriginalPosition = mainCamera.transform.position;
	}

	void Update()
    {
        if (gameRunning)
        {
            timeCounter += Time.deltaTime;

            timeText.text = Mathf.Floor(timeCounter) + "s";
            pinkText.text = pinkCounter + "/" + maxPink;

            if (pinkCounter == maxPink)
            {
                ShowScoreScreen();
            }
        }
        else
        {
            if (Input.anyKeyDown && !(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(0)))
            {
                StartGame();
            }
        }
	}

    void StartGame()
    {
        timeCounter = 0;
        pinkCounter = 0;
        gameRunning = true;
        gameLogo.SetActive(false);
    }

    void ShowScoreScreen()
    {
        gameRunning = false;
        gameLogo.SetActive(true);
        player.transform.position = playerOriginalPosition;
        mainCamera.transform.position = mainCameraOriginalPosition;

        world.transform.position = Vector3.zero;
        world.transform.rotation = Quaternion.identity;
        foreach(Transform child in world.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}