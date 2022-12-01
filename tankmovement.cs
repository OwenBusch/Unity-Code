using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class tankmovement : MonoBehaviour
{
    public GameObject shotgungun;
    public GameObject missilegun;
    public GameObject lasergun;
    public GameObject machinegun;
    public GameObject minegun;
    public GameObject explosivegun;
    public GameObject gun;
    public GameObject menu;
    public GameObject tankBody;
    public GameObject WheelR;
    public GameObject WheelL;
    public GameObject TredR;
    public GameObject TredL;
    public static float speed = 0.1f;
    public GameObject pauseMenu;
    public bool isPaused;
    public string mainMenuScene = "TitleScreen";
    public static float rot = 2f;
    public static int ammo;
    public static int playerCount;
    public Text P1AmmoText;
    public Material cheeseMat;
    public Material Red;
    public Material Green;
    public Material Blue;
    public Material Yellow;
    public AudioSource ammoCrateSource;
    public Rigidbody r;

    private void Awake()
    {
        menu.SetActive(false);
        tankDie.Lives = 1;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        
        
        playerCount = 0;
        if (MainMenu.menuActive == true)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }

        if(LightSpeedGameMode.lightSpeedActive == true)
        {
            speed = 0.4f;
            rot = 8f;
        }
        else
        {
            speed = 0.1f;
            rot = 2f;
        }
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string bigMap = currentScene.name;

        isPaused = false;
        HideGuns();
        gun.SetActive(true);
        ammo = 5;
        P1AmmoText.text = "" + ammo;
        if (bigMap == "BigMap")
        {
            ammo = 20;
        }
        else
        {
            ammo = 5;
        }
    }
    
    void Update()
    {
        P1AmmoText.text = "" + ammo;
        if (menu.activeSelf == false)
        {
            if(pauseMenu.activeSelf == false)
            {
                if (tankDie.Lives == 1)
                {

                    {
                        //PUT THESE BACK TO NORMAL
                         
                        if (Input.GetAxis("Vertical") > 0)
                        {
                            transform.Translate(0, 0, speed);
                        }
                        if (Input.GetAxis("Vertical") < 0)
                        {
                            transform.Translate(0, 0, -speed);
                        }




                        if (Input.GetAxis("Horizontal") > 0)
                        {
                            transform.Rotate(0, rot, 0);
                        }
                        if (Input.GetAxis("Horizontal") < 0)
                        {
                            transform.Rotate(0, -rot, 0);
                        }

                    }
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }

        }

        if(ammo >= 99)
        {
            ammo = 99;
        }
        if (SecretCodeSystem.cheeseActive == true)
        {
            TredR.GetComponent<MeshRenderer>().material = cheeseMat;
            TredL.GetComponent<MeshRenderer>().material = cheeseMat;
            tankBody.GetComponent<MeshRenderer>().material = cheeseMat;
            WheelR.GetComponent<MeshRenderer>().material = cheeseMat;
            WheelL.GetComponent<MeshRenderer>().material = cheeseMat;
            gun.GetComponent<MeshRenderer>().material = cheeseMat;
        }
        
        if (SkinSelect.redActive == true)
        {
            TredR.GetComponent<MeshRenderer>().material = Red;
            TredL.GetComponent<MeshRenderer>().material = Red;
            tankBody.GetComponent<MeshRenderer>().material = Red;
            WheelR.GetComponent<MeshRenderer>().material = Red;
            WheelL.GetComponent<MeshRenderer>().material = Red;
            gun.GetComponent<MeshRenderer>().material = Red;
        }
        
        if (SkinSelect.greenActive == true)
        {
            TredR.GetComponent<MeshRenderer>().material = Green;
            TredL.GetComponent<MeshRenderer>().material = Green;
            tankBody.GetComponent<MeshRenderer>().material = Green;
            WheelR.GetComponent<MeshRenderer>().material = Green;
            WheelL.GetComponent<MeshRenderer>().material = Green;
            gun.GetComponent<MeshRenderer>().material = Green;
        }

        if (SkinSelect.blueActive == true)
        {
            TredR.GetComponent<MeshRenderer>().material = Blue;
            TredL.GetComponent<MeshRenderer>().material = Blue;
            tankBody.GetComponent<MeshRenderer>().material = Blue;
            WheelR.GetComponent<MeshRenderer>().material = Blue;
            WheelL.GetComponent<MeshRenderer>().material = Blue;
            gun.GetComponent<MeshRenderer>().material = Blue;
        }

        if (SkinSelect.yellowActive == true)
        {
            TredR.GetComponent<MeshRenderer>().material = Yellow;
            TredL.GetComponent<MeshRenderer>().material = Yellow;
            tankBody.GetComponent<MeshRenderer>().material = Yellow;
            WheelR.GetComponent<MeshRenderer>().material = Yellow;
            WheelL.GetComponent<MeshRenderer>().material = Yellow;
            gun.GetComponent<MeshRenderer>().material = Yellow;
        }

        transform.Rotate(0, 0, 0);

        Scene currentScene = SceneManager.GetActiveScene();
        string bigMap = currentScene.name;

        if (bigMap == "BigMap")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(ammo <= 19)
                {
                    ammoCrateSource.Play();
                }
                
            }
        }
        


    }

    void OnCollisionEnter(Collision collectable)
    {     
        if(collectable.gameObject.tag == "ammoCrate")
        {
            ammoCrateSource.Play();
            Destroy(collectable.gameObject);
            ammo = ammo + 5;
        }

    }

    void HideGuns()
    {
        shotgungun.SetActive(false);
        missilegun.SetActive(false);
        lasergun.SetActive(false);
        machinegun.SetActive(false);
        minegun.SetActive(false);
        explosivegun.SetActive(false);
        gun.SetActive(false);
    }

    public void TwoPlayerSlect()
    {
        playerCount = 2;
        menu.SetActive(false);
        Time.timeScale = 1;
        MainMenu.menuActive = false;

    }

    public void ThreePlayerSlect()
    {
        playerCount = 3;
        menu.SetActive(false);
        Time.timeScale = 1;
        MainMenu.menuActive = false;

    }

    public void ResumeUI()
    {
        pauseMenu.SetActive(false);
    }

    public void OptionsUI()
    {
        //I need to actually think of something to put in here before I actually make it.
    }

    public void MainMenuUI()
    {
        SceneManager.LoadScene(mainMenuScene);
        tank2Die.Score = 0;
        tankDie.Score = 0;
        MapRotator.randomizerActive = false;
    }
}