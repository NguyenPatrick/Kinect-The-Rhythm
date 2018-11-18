using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Text scoreText;
    public Text difficultyText;
    public Text comboText;

    private PanelCenter panelCenter;
    private KinectManager kinectManager;

    public static int score;
    private int totalNumberOfNotes;
    private int numberOfNotesHit;

    public GameObject notePrefab; // scripted note object
    public GameObject hitBoxPrefab; // scripted hitbox object
    public static Queue<Note> noteQueue = new Queue<Note>();
    public static Queue<Note> pauseQueue = new Queue<Note>();

    public Transform leftTrail;
    public Transform rightTrail;

    // apex points for the hit boxes, dependant on user
    private Vector2 spawnCoordinate = new Vector2(7.5f, 2.5f);
    private const float spawnFactor = 10f;

    public Vector2[] spawnPositions; // possible spawn positions of the notes
    private float waitTime = 1f;
    private float speed = -8f; // temp value

    private bool isPaused;

    // 0 lives
    // 1 timed
    private int gameMode; // easy, intermidiate, advanced
    private bool trigger; 

    void Start () {

        kinectManager = KinectManager.Instance;

        // generates the four hitboxes
        createHitBox(-spawnCoordinate.x, spawnCoordinate.y);
        createHitBox(0, spawnCoordinate.y);
        createHitBox(spawnCoordinate.x, spawnCoordinate.y);

        // generates the spawn points for the notes
        spawnPositions = new Vector2[3];
        spawnPositions[0] = new Vector2(-spawnCoordinate.x, spawnCoordinate.y + spawnFactor);
        spawnPositions[1] = new Vector2(0, spawnCoordinate.y + spawnFactor);
        spawnPositions[2] = new Vector2(spawnCoordinate.x, spawnCoordinate.y + spawnFactor);
      
        trigger = true;
    }


    public IEnumerator noteTimer()
    {
        trigger = false;
        yield return new WaitForSeconds(waitTime);

        if(isPaused == true){
            yield return new WaitForSeconds(waitTime);
        }

        trigger = true;
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

        if(isPaused == false){

            if (trigger == true)
            {
                createNote();
                StartCoroutine(noteTimer());
            }
            else
            {

            }
        }
    }

    private void createHitBox(float x, float y){
        Vector2 newHitBoxPosition = new Vector2(x, y);
        GameObject newHitBoxPreab = (GameObject)Instantiate(hitBoxPrefab, newHitBoxPosition, hitBoxPrefab.transform.rotation);
    }


    private void createNote()
    {
        int spawnCoordinatePosition = Random.Range(0, spawnPositions.Length);
        Vector2 tempSpawnCoordinate = spawnPositions[spawnCoordinatePosition];

        Vector2 newNotePosition = new Vector2(tempSpawnCoordinate.x, tempSpawnCoordinate.y); // based on a random spawn point
        GameObject newNoteObject = (GameObject)Instantiate(notePrefab, newNotePosition, notePrefab.transform.rotation);
        Note newNote = new Note(newNoteObject);
        noteQueue.Enqueue(newNote);


        newNoteObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    public static void removeNote()
    {

        Note n = noteQueue.Dequeue();

        if (n.isPartiallyInHitZone == false && n.isFullyInHitZone == true)
        {
            // full points
            score = score + 100;
        }
        else if (n.isPartiallyInHitZone == true && n.isFullyInHitZone == true)
        {
            // partial points
            score = score + 50;
        }
        else
        {
            // no points
        }

        // destroy the Note
        Destroy(n.getNoteObject());

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

        while (pauseQueue.Count > 0)
        {
            Note n = pauseQueue.Dequeue();
            n.getNoteObject().GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            noteQueue.Enqueue(n);
        }
    }


   
}
