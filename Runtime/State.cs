using System.Collections.Generic;
using System;

namespace Shared.StateMachine
{
    /// <summary>
    /// A generic state machine that manages states and transitions between them.
    /// </summary>
    public class StateMachine<T> where T : System.Enum
    {
        /// <summary>
        /// Dictionary that maps state identifiers to their corresponding State objects.
        /// </summary>
        private Dictionary<T, State<T>> _dictionaryState;

        /// <summary>
        /// The current active state.
        /// </summary>
        private State<T> _currentState;

        /// <summary>
        /// The enum value representing the current state.
        /// </summary>
        private T _currentTypeState;

        /// <summary>
        /// Gets the current active state object.
        /// </summary>
        public State<T> CurrentState
        {
            get { return _currentState; }
        }

        /// <summary>
        /// Gets the enum identifier of the current state.
        /// </summary>
        public T CurrentTypeState
        {
            get { return _currentTypeState; }
        }

        /// <summary>
        /// Gets the dictionary of all registered states.
        /// </summary>
        public Dictionary<T, State<T>> DictionaryStates
        {
            get { return _dictionaryState; }
        }

        /// <summary>
        /// Initializes a new instance of the StateMachine class.
        /// </summary>
        public StateMachine()
        {
            _dictionaryState = new Dictionary<T, State<T>>();
        }

        /// <summary>
        /// Registers a new state with the state machine.
        /// </summary>
        /// <param name="state">The enum value representing the state.</param>
        /// <param name="stateBase">The state object to associate with the enum value.</param>
        public void RegisterState(T state, State<T> stateBase)
        {
            _dictionaryState.TryAdd(state, stateBase);
        }

        /// <summary>
        /// Unregisters a state from the state machine.
        /// </summary>
        /// <param name="state">The enum value representing the state to remove.</param>
        public void UnregisterState(T state)
        {
            _dictionaryState.Remove(state);
        }

        /// <summary>
        /// Switches to the specified state, triggering exit and enter hooks.
        /// </summary>
        /// <param name="state">The target state to switch to.</param>
        public void SwitchState(T state)
        {
            if (_currentState != null)
            {
                _currentState.OnStateExit();
            }

            if (_dictionaryState.TryGetValue(state, out _currentState))
            {
                _currentTypeState = state;
                _currentState.OnStateEnter();
            }
        }
    }
}