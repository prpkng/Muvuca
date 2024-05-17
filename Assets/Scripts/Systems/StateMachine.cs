using System.Collections.Generic;
using UnityEngine;

namespace Muvuca.Systems
{
    public abstract class State
    {
        public StateMachine machine;
        public virtual void Enter(string[] data = null) { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }

    public class StateMachine
    {
        public Dictionary<string, State> states = new();

        public Object owner;
        public State currentState;
        public System.Action<string> stateChanged;

        public void AddState(string name, State state)
        {
            states.Add(name, state);
            state.machine = this;
        }

        public void ChangeState(string nextState, string[] data = null)
        {
            if (!states.ContainsKey(nextState))
            {
                Debug.LogError($"Key '{nextState}' not found in dictionary!");
                return;
            }
            stateChanged?.Invoke(nextState);
            currentState?.Exit();
            currentState = states[nextState];
            currentState?.Enter(data);
        }

        public void Update() { currentState?.Update(); }
        public void FixedUpdate() { currentState?.FixedUpdate(); }
    }
}