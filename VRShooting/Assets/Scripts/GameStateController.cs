using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] GameObject gameReady;  
    [SerializeField] RemainTimer timer;     
    [SerializeField] GameObject gameStart;  
    [SerializeField] GameObject gameOver;   
    [SerializeField] GameObject result;     
    [SerializeField] GameObject player;     
    [SerializeField] GameObject spawners;   

    
    abstract class BaseState
    {
        public GameStateController Controller { get; set; }

        public enum StateAction
        {
            STATE_ACTION_WAIT,
            STATE_ACTION_NEXT
        }

        public BaseState(GameStateController c) { Controller = c; }

        public virtual void OnEnter() { }
        public virtual StateAction OnUpdate() { return StateAction.STATE_ACTION_NEXT; }
        public virtual void OnExit() { }
    }

    
    class ReadyState : BaseState
    {
        float timer;

        public ReadyState(GameStateController c) : base(c) { }
        public override void OnEnter()
        {
            
            Controller.gameReady.SetActive(true);
        }
        public override StateAction OnUpdate()
        {
            timer += Time.deltaTime;
            
            if (timer > 5.0f)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void OnExit()
        {
            
            Controller.gameReady.SetActive(false);
        }
    }

    
    class StartState : BaseState
    {
        float timer;

        public StartState(GameStateController c) : base(c) { }
        public override void OnEnter()
        {
            
            Controller.timer.gameObject.SetActive(true);

            
            Controller.gameStart.SetActive(true);

            
            Controller.player.SetActive(true);

           
            Controller.spawners.SetActive(true);
        }
        public override StateAction OnUpdate()
        {
            timer += Time.deltaTime;
           
            if (timer > 1.8f)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void OnExit()
        {
            
            Controller.gameStart.SetActive(false);
        }
    }

    
    class PlayingState : BaseState
    {
        public PlayingState(GameStateController c) : base(c) { }
        public override StateAction OnUpdate()
        {
           
            if (!Controller.timer.IsCountingDown())
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }

        public override void OnExit()
        {
            
            Controller.player.SetActive(false);

            
            Controller.spawners.SetActive(false);
        }
    }

    
    class GameOverState : BaseState
    {
        float timer;
        public GameOverState(GameStateController c) : base(c) { }
        public override void OnEnter()
        {
           
            Controller.gameOver.SetActive(true);
        }
        public override StateAction OnUpdate()
        {
            timer += Time.deltaTime;
            
            if (timer > 2.0f)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void OnExit()
        {
            
            Controller.gameOver.SetActive(false);
        }
    }

    
    class ResultState : BaseState
    {
        public ResultState(GameStateController c) : base(c) { }
        public override void OnEnter()
        {
            
            Controller.result.SetActive(true);
        }
        public override StateAction OnUpdate() { return StateAction.STATE_ACTION_WAIT; }
    }

    
    List<BaseState> state;

    
    int currentState;

    void Start()
    {
        
        state = new List<BaseState> {
            new ReadyState(this),
            new StartState(this),
            new PlayingState(this),
            new GameOverState(this),
            new ResultState(this),
        };

        
        state[currentState].OnEnter();
    }

    void Update()
    {
       
        var stateAction = state[currentState].OnUpdate();

        
        if (stateAction == BaseState.StateAction.STATE_ACTION_NEXT)
        {
            
            state[currentState].OnExit();
           
            ++currentState;
            
            state[currentState].OnEnter();
        }
    }
}