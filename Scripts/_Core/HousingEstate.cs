using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingEstate : MonoBehaviour
{
    [Header("Prefabs")]
    public Peon[] villagerPrefabs;
    public Home[] housingPrefab;
    public Workshop workshopPrefab;

    [Header("Town Buildings")]
    public JobCenter jobCenter;
    public HolySite holySite;
    public Workshop workshop;
    public ResourceCentre wareHouse;
    public FoodStorage foodStorage;
    public NonFoodStorage nonFoodStorage;


    [Header("Current village info")]
    public int maxInhabitants;
    public int currentInhabitants;

    public Queue<Peon> waitingList = new Queue<Peon>();
    [SerializeField] private List<Home> houses = new List<Home>();

    void Awake()
    {
        jobCenter = GetComponentInChildren<JobCenter>();
        holySite = GetComponentInChildren<HolySite>();
        workshop = GetComponentInChildren<Workshop>();
        wareHouse = GetComponentInChildren<ResourceCentre>();
        foodStorage = GetComponentInChildren<FoodStorage>();
        nonFoodStorage = GetComponentInChildren<NonFoodStorage>();
    }
    private void Update()
    {
        AssignHome();
    }
    public void AddHome(Home home)
    {
        houses.Add(home);
        maxInhabitants += home.maxInhabitants;
    }
    private void AssignHome()
    {
        // if we have peons waiting and houses / spaces available
        if (waitingList.Count > 0 && (maxInhabitants - currentInhabitants) > 0)
        {
            Peon peon = waitingList.Peek(); // Look at peon 1 without modifying queue.

            // Check to see if peon is in need of a house.
            if (peon.home != null)
            {
                if ((peon.partner != null && peon.partner.home == peon.home) || peon.partner == null)
                {
                    waitingList.Dequeue();
                    return;
                }
            }

            if (peon.partner == null)
            {
                Home assignedHome = FindHome(1);
                if (assignedHome != null && assignedHome.MoveIn(peon))
                {
                    peon.home = assignedHome;
                    waitingList.Dequeue();
                    return;
                }
            }
            else if (peon.partner != null)
            {
                Home assignedHome = FindHome(2);
                if (assignedHome != null && assignedHome.MoveIn(peon))
                {
                    if (assignedHome.MoveIn(peon.partner))
                    {
                        peon.home = assignedHome;
                        peon.partner.home = assignedHome;
                        waitingList.Dequeue();
                        return;
                    }
                }
            }
        }
    }

    public Home FindHome(int spaceNeeded)
    {
        foreach (Home home in houses)
        {
            if ((home.maxInhabitants - home.inhabitants.Count) >= spaceNeeded)
            {

                return home;
            }
        }
        // if we get here no suitable home has been found
        return null;
    }

    public void AddToHousingWaitingList(Peon peon)
    {
        waitingList.Enqueue(peon);
    }

}

