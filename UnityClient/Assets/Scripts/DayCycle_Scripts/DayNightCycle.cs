using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour, DataPersistanceInterface
{
    [SerializeField] private Animator anim;
    private PlayerEnergy playerEnergy;

    [Header("Time Settings")]
    public int startHour = 6;
    public float minutesPerSecond = 1f;
    public int day = 1; 

    [Header("UI References")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText; 
    public Image middleArrow;
    public Image topArrow;
    public Image downArrow;

    [Header("Night Settings")]
    public int nightStartHour = 20; 
    public int morningHour = 6; 

    [Header("Lighting Settings")]
    public Light2D sunLight; 
    public Gradient sunColor; 

    private float elapsedTime;
    private int currentHour;
    private int currentMinute;
    private bool newDayTriggered = false;
    private bool isSleeping = false;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("TimeManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergy>();
        UpdateTimeUI();
        UpdateLighting();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MinesForest"|| SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameObject sun = GameObject.FindGameObjectWithTag("Sun");
            sunLight= sun.GetComponent<Light2D>();
            //sunLight = GameObject.FindObjectOfType<Light2D>();
            anim = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        }

        if (!isSleeping)
        {
            AdvanceTime();
            UpdateTimeUI();
            UpdateLighting();        
        }
    }

    void AdvanceTime()
    {
        elapsedTime += Time.deltaTime * (minutesPerSecond / 60f);

        float totalMinutes = elapsedTime * 60;
        currentHour = Mathf.FloorToInt(totalMinutes / 60f) % 24;
        currentMinute = Mathf.FloorToInt(totalMinutes) % 60;

        if (currentHour == 0 && currentMinute == 0)
        {
            if (!newDayTriggered) 
            {
                day++;
                newDayTriggered = true;
            }
        }
        else
        {
            newDayTriggered = false;
        }
    }

    void UpdateTimeUI()
    {
        string time = currentHour.ToString("D2") + ":";
        if(currentMinute<30)
        {
            time += "00";
        }
        else
        {
            time += "30";
        }
        timeText.text = time;
        dayText.text = day.ToString();

        if (currentHour >= 0 && currentHour < 12)
        {
            middleArrow.enabled = false;
            downArrow.enabled = false;
            topArrow.enabled = true;
        } else if(currentHour >= 12 && currentHour < 20)
        {
            middleArrow.enabled = true;
            downArrow.enabled = false;
            topArrow.enabled = false;
        } else if(currentHour >= 20 && currentHour < 24)
        {
            middleArrow.enabled = false;
            downArrow.enabled = true;
            topArrow.enabled = false;
        }
    }

    void UpdateLighting()
    {
        if (sunLight != null)
        {
            float dayProgress = (currentHour + currentMinute / 60f) / 24f;
            sunLight.color = sunColor.Evaluate(dayProgress);
            
        }
    }

    public void Sleep()
    {
        if(IsNight() == true)
        {
            anim.SetTrigger("Enter");
            isSleeping = true; // Blocca l'avanzamento del tempo
            day++;
            elapsedTime = startHour * 60f / minutesPerSecond;
            currentHour = startHour;
            currentMinute = 0;

            StartCoroutine(WakeUp());

            playerEnergy.UpdateEnergy(5);
        }
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(1f);
        isSleeping = false;
        UpdateTimeUI();
        UpdateLighting();
        anim.SetTrigger("Exit");
    }

    public bool IsNight()
    {
        return currentHour >= nightStartHour || currentHour < morningHour;
    }

  public void LoadData(GameData data)
    {
        this.day= data.day;
        this.currentHour = data.hour; 
        this.currentMinute= data.minute;
        this.elapsedTime = data.elapsedTime;
    }
    public void SaveData(ref GameData data)
    {
        data.day = this.day;
        data.hour = this.currentHour;
        data.minute = this.currentMinute;
        data.elapsedTime = this.elapsedTime;
    }
}


