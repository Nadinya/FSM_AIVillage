using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Peon : MonoBehaviour
{
    [Header("Personal Info")]
    public string firstName;
    public string lastName;
    public Gender gender;
    public Home home;
    public Peon partner;

    public Peon parent1;
    public Peon parent2;
    public int age = 20;
    
    [Header("Job Info")]
    public bool beenKissed = false;
    public bool canBeBreeder = true;
    public bool canWorship = true;

    [Header("Current State")]
    public string stateString;
    public Jobs currentJob;
    public ResourceType resourceNeeded;
    public bool needNewJob = true;

    [Header("Prime Locations")]
    public HousingEstate housingEstate;

    [Header("Inventory")]
    public Item itemInHand;

    // Agent Components
    private NavMeshAgent agent;
    private Animator anim;

    //States
    public readonly Transitional_State transitionState = new Transitional_State();
    public readonly Gather_State gatherState = new Gather_State();
    public readonly ReturnResource_State returnState = new ReturnResource_State();

    public _State currentState;

    private void OnEnable()
    {
        DayNightCycle.newDay += Age;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (housingEstate == null)
        {
            Debug.Log("No Housing estate");
        }

        if (home == null)
        {
            housingEstate.AddToHousingWaitingList(this);
        }

        if (age >= 20)
        {
            housingEstate.jobCenter.AddWorkers(this);
        }
        else
        {
            TransitionToState(new Child_State());
        }


    }
    private void Age()
    {
        age++;

        if (age == 20)
        {
            housingEstate.jobCenter.AddWorkers(this);
            gameObject.transform.localScale = Vector3.one;
        }
    }


    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != null)
        {
            currentState.OnTriggerEnter(this, other);
        }
    }

    public void TransitionToState(_State state)
    {
        currentState = state;

        if (state != null)
        {
            currentState.EnterState(this);
        }

        if (!canBeBreeder)
        {
            StartCoroutine(GetReadyToBreed());
        }
        else if (!canWorship)
        {
            StartCoroutine(GetReadyForWorship());
        }
    }

    public void Birth()
    {
        int random = Random.Range(0, housingEstate.villagerPrefabs.Length);
        Peon child = Instantiate(housingEstate.villagerPrefabs[random], transform.parent);
        child.transform.position = transform.position + transform.forward;

        child.gender = random == 0 ? Gender.Male : Gender.Female;

        child.housingEstate = housingEstate;

        child.parent1 = this;
        child.parent2 = partner;
    }

    IEnumerator GetReadyForWorship()
    {
        yield return new WaitForSeconds(60);
        canWorship = true;
    }
    IEnumerator GetReadyToBreed()
    {
        yield return new WaitForSeconds(60);
        canBeBreeder = true;
    }

    public void TriggerAnimation(string animName)
    {
        anim.SetTrigger(animName);
    }
    public void MoveTo(Vector3 location)
    {
        agent.isStopped = false;
        anim.SetBool("isWalking", true);
        agent.SetDestination(location);
    }
    public void StopMoving()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
    }

    private void OnDisable()
    {
        DayNightCycle.newDay -= Age;
    }
}
