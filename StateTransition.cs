using System;

namespace SharedScripts.StateMachine
{
    /// <summary>
    /// Represents a transition from one state to another, triggered by a condition.
    /// </summary>
    public class StateTransition<TState> where TState : System.Enum
    {
        /// <summary>
        /// The condition that must be true for the transition to occur.
        /// </summary>
        public Func<bool> Condition { get; private set; }

        /// <summary>
        /// The state to transition to when the condition is met.
        /// </summary>
        public TState TargetState { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StateTransition class.
        /// </summary>
        /// <param name="condition">The condition required for the transition.</param>
        /// <param name="state">The target state to transition to.</param>
        public StateTransition(Func<bool> condition, TState state)
        {
            Condition = condition;
            TargetState = state;
        }
    }
}