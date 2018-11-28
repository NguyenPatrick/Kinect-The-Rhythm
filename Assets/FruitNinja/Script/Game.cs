using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// change scale based on difficulty

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

    private int totalNumberOfNotes;
    private int numberOfNotesHit;
    public Text scoreText;
    public Text difficultyText;
    public Text comboText;

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
    public GameObject centerHand;
    public GameObject rightHand;

    // virtual gameobjects controllers
    HitBox innerLeftHitBox;
    HitBox innerCenterHitBox;
    HitBox innerRightHitBox;
    HitBox outerLeftHitBox;
    HitBox outerCenterHitBox;
    HitBox outerRightHitBox;

    Trigger leftTrigger;
    Trigger centerTrigger;
    Trigger rightTrigger;

    Charge leftCharge;
    Charge centerCharge;
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
    private float waitTime = 2f; // cooldown for notes
    private float speed = -3f; // temp value

    private bool isPaused;
    private bool enableSpawn;

    public enum Hand { Left, Center, Right };

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

        enableSpawn = true;
        validCombo = true;
    }


    public IEnumerator noteTimer()
    {
        enableSpawn = false;
        yield return new WaitForSeconds(waitTime);

        if(isPaused == true){
            StartCoroutine(noteTimer());
        } else{
            enableSpawn = true;
        }
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

        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo;


        if (enableSpawn == true)
        {
            createNote();
            StartCoroutine(noteTimer());
        }

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
                    score = score + 100;
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
                    score = score + 50;
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

        if (validCombo == false)
        {
            validCombo = true;
            combo = 0;
        }
    }


    public void pauseGame()
    {
        isPaused = true;
        enableSpawn = false;

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
        noteTimer();

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
        Debug.Log("Naomi has ended the game without winning");
    }
}
