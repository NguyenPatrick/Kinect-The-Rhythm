using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour {


    private KinectManager kinectManager;

    // score tracking
    public static int score;
    private int totalNumberOfNotes;
    private int numberOfNotesHit;
    public Text scoreText;
    public Text difficultyText;
    public Text comboText;

    // prefabs
    public GameObject notePrefab; // scripted note object
    public GameObject hitBoxPrefab; // scripted hitbox object
    public GameObject triggerPrefab; // scripted control
    public GameObject chargePrefab; // scripted trigger
    public GameObject innerHitBoxPrefab;
    public GameObject outerHitBoxPrefab;

    // for pausing
    public static Queue<Note> noteQueue = new Queue<Note>();
    public static Queue<Note> pauseQueue = new Queue<Note>();
    
    public GameObject leftHand;
    public GameObject rightHand;

    HitBox innerLeftHitBox;
    HitBox innerCenterHitBox;
    HitBox innerRightHitBox;
    HitBox outerLeftHitBox;
    HitBox outerCenterHitBox;
    HitBox outerRightHitBox;


    Trigger centerTrigger;
    Trigger leftTrigger;
    Trigger rightTrigger;
    Charge leftCharge;
    Charge rightCharge;


    // apex points for the hit boxes, will be dependant on user
    private Vector2 hitBoxCoordinate = new Vector2(6f, 1.5f);
    // y coordinates of locations relative to hitboxes
    private const float spawnFactor = 10f;
    private const float triggerFactor = 5f; 
    private const float chargeFactor = 12f; 

    public Vector2[] spawnPositions; // possible spawn positions of the notes
    private float waitTime = 2f;
    private float speed = -3f; // temp value

    private bool isPaused;

    // 0 lives
    // 1 timed
    private int gameMode; // easy, intermidiate, advanced
    private bool trigger;

    public enum Hand { Left, Center, Right };

    void Start () {

        kinectManager = KinectManager.Instance;

        // generates spawn points for the notes
        spawnPositions = new Vector2[3];
        spawnPositions[0] = new Vector2(-hitBoxCoordinate.x, hitBoxCoordinate.y + spawnFactor);
        spawnPositions[1] = new Vector2(0, hitBoxCoordinate.y + spawnFactor);
        spawnPositions[2] = new Vector2(hitBoxCoordinate.x, hitBoxCoordinate.y + spawnFactor);

        // generates the hitboxes
        innerLeftHitBox = createGameObject(-hitBoxCoordinate.x, hitBoxCoordinate.y, innerHitBoxPrefab).GetComponent<HitBox>();
        innerCenterHitBox = createGameObject(0, hitBoxCoordinate.y, innerHitBoxPrefab).GetComponent<HitBox>();
        innerRightHitBox = createGameObject(hitBoxCoordinate.x, hitBoxCoordinate.y, innerHitBoxPrefab).GetComponent<HitBox>();
        outerLeftHitBox = createGameObject(-hitBoxCoordinate.x, hitBoxCoordinate.y, outerHitBoxPrefab).GetComponent<HitBox>();
        outerCenterHitBox = createGameObject(0, hitBoxCoordinate.y, outerHitBoxPrefab).GetComponent<HitBox>();
        outerRightHitBox = createGameObject(hitBoxCoordinate.x, hitBoxCoordinate.y, outerHitBoxPrefab).GetComponent<HitBox>();

        // generates the controls
        centerTrigger = createGameObject(0, hitBoxCoordinate.y - triggerFactor, triggerPrefab).GetComponent<Trigger>();
        leftTrigger = createGameObject(-hitBoxCoordinate.x, hitBoxCoordinate.y - triggerFactor , triggerPrefab).GetComponent<Trigger>(); 
        rightTrigger = createGameObject(hitBoxCoordinate.x, hitBoxCoordinate.y - triggerFactor, triggerPrefab).GetComponent<Trigger>(); 
        leftCharge = createGameObject(-hitBoxCoordinate.x, hitBoxCoordinate.y - chargeFactor, chargePrefab).GetComponent<Charge>(); 
        rightCharge = createGameObject(hitBoxCoordinate.x, hitBoxCoordinate.y - chargeFactor, chargePrefab).GetComponent<Charge>(); 

        //leftHand.transform.position = new Vector2(-hitBoxCoordinate.x, -6f);
        //rightHand.transform.position = new Vector2(hitBoxCoordinate.x, -6f);

        trigger = true;
    }


    public IEnumerator noteTimer()
    {
        trigger = false;
        yield return new WaitForSeconds(waitTime);
        trigger = true;
    }


    void Update()
    {


        // if (trigger.isPressed && trigger.isEnabled)
        // if (inner detects fruit and outer doesnt --> function)


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

       
        */


        if (isPaused == false)
        {
            if (trigger == true)
            {
                createNote();
                StartCoroutine(noteTimer());
            }
            else
            {

            }
        }

        /*
        if (note.isPartiallyInHitZone == false && note.isFullyInHitZone == true)
        {
            // full points
            Debug.Log("FULL");
            score = score + 100;
            Destroy(note.gameObject);
        }
        else if (note.isPartiallyInHitZone == true && note.isFullyInHitZone == true)
        {
            // partial points
            Debug.Log("PARTIAL");
            score = score + 50;
            Destroy(note.gameObject);
        }
        else
        {
            pauseQueue.Enqueue(note);
        }
        */

        // trigger is clicked
        if (rightTrigger.getIsDetected())
        {
            // only valid if trigger is charged
            if (rightCharge.getIsCharged())
            {
                rightCharge.setNotCharged();
                rightTrigger.setTriggered();

                if (innerRightHitBox.getNoteIsTouching() && !outerRightHitBox.getNoteIsTouching()){
                    Debug.Log("FULL");
                    Destroy(innerRightHitBox.getNoteObject());
                }
                else if (innerRightHitBox.getNoteIsTouching() && outerRightHitBox.getNoteIsTouching())
                {
                    Debug.Log("PARTIAL");
                    Destroy(outerRightHitBox.getNoteObject());
                }

            }
        }

        // if uncharged, charge again
        if (rightCharge.getIsDetected() == true)
        {
            rightCharge.setCharged();
            rightTrigger.setNotTriggered();

        }

        if (leftTrigger.getIsDetected())
        {

        }

    }

    private GameObject createGameObject(float x, float y, GameObject prefab){
        Vector2 newPosition = new Vector2(x, y);
        GameObject newPrefab = (GameObject)Instantiate(prefab, newPosition, prefab.transform.rotation);
        return newPrefab;
    }


    private void createNote()
    {
        int hitBoxCoordinatePosition = Random.Range(0, spawnPositions.Length);
        Vector2 tempSpawnCoordinate = spawnPositions[hitBoxCoordinatePosition];

        Vector2 newNotePosition = new Vector2(tempSpawnCoordinate.x, tempSpawnCoordinate.y); // based on a random spawn point
        GameObject newNoteObject = (GameObject)Instantiate(notePrefab, newNotePosition, notePrefab.transform.rotation);
        Note newNote = new Note(newNoteObject, hitBoxCoordinatePosition);
        noteQueue.Enqueue(newNote);

        newNoteObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    public static void removeNote(){

        Note note = null;

        if(pauseQueue.Count > 0){
            note = pauseQueue.Dequeue().getNoteObject().GetComponent<Note>();
        }
        else{
            note = noteQueue.Dequeue().getNoteObject().GetComponent<Note>();
        }




        if (note.isPartiallyInHitZone == false && note.isFullyInHitZone == true)
        {
            // full points
            Debug.Log("FULL");
            score = score + 100;
            Destroy(note.gameObject);
        }
        else if (note.isPartiallyInHitZone == true && note.isFullyInHitZone == true)
        {
            // partial points
            Debug.Log("PARTIAL");
            score = score + 50;
            Destroy(note.gameObject);
        }
        else
        {
            pauseQueue.Enqueue(note);
        }

       

    }

    public static void deQueue(){
        if(pauseQueue.Count > 0){
            pauseQueue.Dequeue();
        }
        else{
            noteQueue.Dequeue();
        }
    }

    public void pauseGame(){

        isPaused = true;

        while(noteQueue.Count > 0){
            Note n = noteQueue.Dequeue();
            n.getNoteObject().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            pauseQueue.Enqueue(n);
        }
    }

    public void resumeGame(){

        isPaused = false;

        while (pauseQueue.Count > 0){
            Note n = pauseQueue.Dequeue();
            n.getNoteObject().GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            noteQueue.Enqueue(n);
        }

        StartCoroutine(noteTimer());
    }
}
