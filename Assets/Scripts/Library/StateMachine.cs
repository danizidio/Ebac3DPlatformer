using System.Collections.Generic;

namespace StateMachines
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> states;

        StateBase _currentState;
        public StateBase currentState { get { return _currentState; } }

        //public StateMachine(T state)
        //{
        //    StartStateMachine();
        //    SwitchState(state);
        //}

       public void Update()
        {
            _currentState?.OnStateStay();
        }

        public void StartStateMachine()
        {
            states = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            states.Add(typeEnum, state);
        }

        public void SwitchState(T state, object o = null)
        {
            if (_currentState == states[state]) return;
            _currentState?.OnStateExit();
            
            _currentState = states[state];
            _currentState.OnStateEnter(o);
        }
    }
}