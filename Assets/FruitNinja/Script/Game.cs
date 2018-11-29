using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// change scale based on difficulty
// data logging: hit %
// data logging: green %
// data logging: yellow %


public class Game : MonoBehaviour {

    private KinectManager kinectManager;

    // score tracking
    public static int score;
    public static int combo;
    public static int maxCombo;
    public static bool validCombo;
    public static int totalNotes;
    public static int perfectNotes;
    public static int partialNotes;
    public static int missedNotes;

    public Text scoreText;
    public Text comboText;
    public Text timeText;


    public Text finalScoreText;
    public Text highScoreText;
    public Text maxComboText;

    // prefabs
    public GameObject notePrefab;
    public GameObject innerHitBoxPrefab;
    public GameObject outerHitBoxPrefab;
    public GameObject ringPrefab;
    public GameObject triggerPrefab;
    public GameObject chargePrefab;
    public GameObject deletePrefab;

    // hand controllers
    public GameObject leftHand;
    public GameObject rightHand;


    // virtual gameobjects controllers
    HitBox innerLeftHitBox;
    HitBox innerRightHitBox;
    HitBox outerLeftHitBox;
    HitBox outerRightHitBox;

    Trigger leftTrigger;
    Trigger rightTrigger;

    Charge leftCharge;
    Charge rightCharge;

    // apex points for the hit boxes, will be dependant on user
    private Vector2 hitBoxCoordinate = new Vector2(2.5f, 0f);
    private const float spawnFactor = 10f;
    private const float triggerFactor = 4f; 
    private const float chargeFactor = 10.5f;
    private const float deleteFactor = 2.75f;
    private const float controlFactor = 1f;
    private const float handFactor = 7f;

    public Vector2[] spawnPositions; // possible spawn positions of the notes
    private float waitTime = 1.5f; // cooldown for notes
    private float speed = -3f; // temp value

    private static string gameMode; 
    private static string difficulty;
    private const int finalGameTime = 5;
    private static int gameTime;


    private bool isPaused;
    private bool isDetected;

    public GameObject gameInfoMenu;
    public GameObject scoreMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject detectedMenu;
    public GameObject pauseButton;

    public static bool trigger;

    public void controlDetect()
    {
        if (trigger == true)
        {
            trigger = false;
        }
        else
        {
            trigger = true;
        }
    }



    void Start () {

        kinectManager = KinectManager.Instance;

        // generates spawn points for the notes
        spawnPositions = new Vector2[4];
        spawnPositions[0] = new Vector2(-hitBoxCoordinate.x, hitBoxCoordinate.y + spawnFactor);
        spawnPositions[1] = new Vector2(hitBoxCoordinate.x, hitBoxCoordinate.y + spawnFactor);

        // generates the hitboxes
        innerLeftHitBox = createGameComponent(-hitBoxCoordinate.x, hitBoxCoordinate.y, innerHitBoxPrefab).GetComponent<HitBox>();
        innerRightHitBox = createGameComponent(hitBoxCoordinate.x, hitBoxCoordinate.y, innerHitBoxPrefab).GetComponent<HitBox>();

        outerLeftHitBox = createGameComponent(-hitBoxCoordinate.x, hitBoxCoordinate.y, outerHitBoxPrefab).GetComponent<HitBox>();
        outerRightHitBox = createGameComponent(hitBoxCoordinate.x, hitBoxCoordinate.y, outerHitBoxPrefab).GetComponent<HitBox>();

      //  innerLeftHitBox.gameObject.GetComponent<Transform>().localScale = new Vector3(2,2,0);

        // generates the controls
        leftTrigger= createGameComponent(-hitBoxCoordinate.x - controlFactor, hitBoxCoordinate.y - triggerFactor , triggerPrefab).GetComponent<Trigger>();
        rightTrigger = createGameComponent(hitBoxCoordinate.x  + controlFactor, hitBoxCoordinate.y - triggerFactor, triggerPrefab).GetComponent<Trigger>(); 
        leftCharge = createGameComponent(-hitBoxCoordinate.x - controlFactor, hitBoxCoordinate.y - chargeFactor, chargePrefab).GetComponent<Charge>();
        rightCharge = createGameComponent(hitBoxCoordinate.x + controlFactor, hitBoxCoordinate.y - chargeFactor, chargePrefab).GetComponent<Charge>();

        //createGameComponent(0, hitBoxCoordinate.y - deleteFactor, deletePrefab);
        createGameComponent(0, -3f, deletePrefab);
        createGameComponent(0, -11.5f, deletePrefab);


        leftHand.transform.position = new Vector2(-hitBoxCoordinate.x - controlFactor, -handFactor);
        rightHand.transform.position = new Vector2(hitBoxCoordinate.x + controlFactor, -handFactor);

        validCombo = true;

        gameTime = finalGameTime;

        score = 0;
        combo = 0;
        maxCombo = 0;
        totalNotes = 0;
        perfectNotes = 0;
        partialNotes = 0;
        missedNotes = 0;

        StartCoroutine(countDown());
        StartCoroutine(noteTimer());
        isDetected = false;
        isPaused = false;
    }

    public IEnumerator noteTimer()
    {
        if (isPaused == false)
        {
            createNote();
        }

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(noteTimer());
    }

    public IEnumerator countDown()
    {
        yield return new WaitForSeconds(1f);

        if (gameTime > 0 && isPaused == false)
        {
            gameTime = gameTime - 1;
        }
        else if(gameTime == 0)
        {
            endGame();
        }

        StartCoroutine(countDown());
    }


    void Update()
    {
        // run code to detect the user --> detected user in 3..2..1
        // if(user can't be detected, pause the game)

        /*
         * for all values in the queue, dequeue and set velocity to zero
         * all the dequeues values to pauseQueue, when user is detected again
         * re add all the pauseQueue values into the noteQueue
         */

        // if user is detected again, game continues in 3..2..1

        // every x-y seconds, depending on difficulty, generate a note at a random location
        // different note types for different difficulties

        /*
        // moves controls vertically relative to hands
          if (kinectManager && kinectManager.IsInitialized())
            {
                if (kinectManager.IsUserDetected())
                {
                    long userId = kinectManager.GetPrimaryUserID();
                    int jointType = (int)KinectInterop.JointType.HandRight;
                    if (kinectManager.IsJointTracked(userId, jointType))
                    {
                        Vector3 rHandPosition = kinectManager.GetJointKinectPosition(userId, jointType);
                        rightHand.transform.position = new Vector3(rightHand.transform.position.x, rHandPosition.y, rightHand.transform.position.z);
                    }

                    jointType = (int)KinectInterop.JointType.HandLeft;
                    if (kinectManager.IsJointTracked(userId, jointType))
                    {
                        Vector3 lHandPosition = kinectManager.GetJointKinectPosition(userId, jointType);
                        leftHand.transform.position = new Vector3(leftHand.transform.position.x, lHandPosition.y, leftHand.transform.position.z);
                    }
                }
            }
        */

        if(trigger)
        {
            if(isDetected == false)
            {
                resumeGame();
                isDetected = true;
                pauseButton.SetActive(true);
                pauseMenu.SetActive(false);
                scoreMenu.SetActive(true);
                gameInfoMenu.SetActive(true);
                detectedMenu.SetActive(false);
            }
        }
        else
        {
            pauseGame();
            isPaused = true;
            isDetected = false;
            pauseButton.SetActive(false);
            pauseMenu.SetActive(false);
            scoreMenu.SetActive(false);
            gameInfoMenu.SetActive(false);
            detectedMenu.SetActive(true);
        }

        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo;
        timeText.text = "Time Left: " + gameTime;

        controlGameComponent(leftTrigger, leftCharge, innerLeftHitBox, outerLeftHitBox);
        controlGameComponent(rightTrigger, rightCharge, innerRightHitBox, outerRightHitBox);
    }


    private void createNote()
    {
        int hitBoxCoordinatePosition = Random.Range(0, spawnPositions.Length-2);
        Vector2 tempSpawnCoordinate = spawnPositions[hitBoxCoordinatePosition];

        Vector2 newNotePosition = new Vector2(tempSpawnCoordinate.x, tempSpawnCoordinate.y); // based on a random spawn point
        GameObject newNoteObject = (GameObject)Instantiate(notePrefab, newNotePosition, notePrefab.transform.rotation);

        newNoteObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        totalNotes = totalNotes + 1;
    }


    private GameObject createGameComponent(float x, float y, GameObject prefab)
    {
        Vector2 newPosition = new Vector2(x, y);
        GameObject newPrefab = (GameObject)Instantiate(prefab, newPosition, prefab.transform.rotation);
        return newPrefab;
    }


    private void controlGameComponent(Trigger trigger, Charge charge, HitBox innerHitBox, HitBox outerHitBox)
    {

        Vector2 position = innerHitBox.GetComponent<Transform>().position;

        if (trigger.getIsDetected())
        {
            // only valid if trigger is charged
            if (charge.getIsCharged())
            {
                charge.setNotCharged();
                trigger.setTriggered();

                if (innerHitBox.getNoteIsTouching() && !outerHitBox.getNoteIsTouching())
                {
                    score = score + 100 + 5 * combo;
                    perfectNotes = perfectNotes + 1;
                    Destroy(innerHitBox.getNoteObject());

                    Ring ringObject = ((GameObject)Instantiate(ringPrefab, position, ringPrefab.transform.rotation)).GetComponent<Ring>();
                    ringObject.createGreenRing();

                    if (validCombo == true)
                    {
                        combo = combo + 1;
                    }
                }
                else if (innerHitBox.getNoteIsTouching() && outerHitBox.getNoteIsTouching())
                {
                    score = score + 50 + 5 * combo;
                    partialNotes = partialNotes + 1;
                    Destroy(innerHitBox.getNoteObject());

                    Ring ringObject = ((GameObject)Instantiate(ringPrefab, position, ringPrefab.transform.rotation)).GetComponent<Ring>();
                    ringObject.createYellowRing();

                    if (validCombo == true)
                    {
                        combo = combo + 1;
                    }
                }
                else
                {
                    validCombo = false;
                    missedNotes = missedNotes + 1;
                    Ring ringObject = ((GameObject)Instantiate(ringPrefab, position, ringPrefab.transform.rotation)).GetComponent<Ring>();
                    ringObject.createRedRing();
                }
            }
        }

        if (charge.getIsDetected() == true)
        {
            charge.setCharged();
            trigger.setNotTriggered();
        }

        if(combo > maxCombo)
        {
            maxCombo = combo;
        }

        if (validCombo == false)
        {
            validCombo = true;
            combo = 0;
        }
    }

    public void pauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        foreach (GameObject note in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (note.name == Note.noteName)
            {
                note.GetComponent<Note>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    public void resumeGame(){

        isPaused = false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

        foreach (GameObject note in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (note.name == Note.noteName)
            {
                note.GetComponent<Note>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
        }
    }

    public void endGame()
    {
        Debug.Log("Naomi has ended the game without achieving anything");

        isPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        scoreMenu.SetActive(false);
        gameInfoMenu.SetActive(false);
        detectedMenu.SetActive(false);
        gameOverMenu.SetActive(true);

        foreach (GameObject note in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (note.name == Note.noteName)
            {
                Destroy(note);
            }
        }
    }
}
