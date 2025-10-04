using System.Collections.Generic;
using System;

namespace StateMachine
{
    /// <summary>
    /// Base class for defining a state with transitions and behavior hooks.
    /// </summary>
    public abstract class State<TState> where TState : System.Enum
    {
        /// <summary>
        /// The state machine this state belongs to.
        /// </summary>
        protected StateMachine<TState> StateMachine { get; private set; }

        /// <summary>
        /// List of transitions from this state to other states.
        /// </summary>
        protected List<StateTransition<TState>> transitions = new List<StateTransition<TState>>();

        /// <summary>
        /// Initializes a new instance of the State class.
        /// </summary>
        /// <param name="stateMachine">The state machine this state is part of.</param>
        public State(StateMachine<TState> stateMachine)
        {
            StateMachine = stateMachine;
        }

        /// <summary>
        /// Adds a new transition to another state with a given condition.
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="targetState">The state to transition to if the condition is true.</param>
        public virtual void AddTransition(Func<bool> condition, TState targetState)
        {
            transitions.Add(new StateTransition<TState>(condition, targetState));
        }

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public virtual void OnStateEnter()
        {
        }

        /// <summary>
        /// Called on every update while the state is active.
        /// Checks for transitions and switches state if a condition is met.
        /// </summary>
        public virtual void OnStateStayUpdate()
        {
            if (CheckTransitions(out var state))
            {
                StateMachine.SwitchState(state);
            }
        }

        /// <summary>
        /// Called on every fixed update while the state is active.
        /// </summary>
        public virtual void OnStateStatyFixedUpdate()
        {
        }

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public virtual void OnStateExit()
        {
        }

        /// <summary>
        /// Checks all transitions to determine if a state change should occur.
        /// </summary>
        /// <param name="state">The target state to transition to, if applicable.</param>
        /// <returns>True if a valid transition is found; otherwise, false.</returns>
        private bool CheckTransitions(out TState state)
        {
            foreach (var transition in transitions)
            {
                if (transition.Condition.Invoke())
                {
                    state = transition.TargetState;
                    return true;
                }
            }

            state = default;
            return false;
        }
    }
}