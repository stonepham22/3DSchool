public abstract class StateMachine
{
    protected BaseState currentState;
    public BaseState CurrentState => currentState;

    protected virtual void Initialize(BaseState state)
    {
        currentState = state;
        state.Enter();
    }

    public virtual void TransitionTo(BaseState nextState)
    {
        if(nextState == currentState) return;
        currentState?.Exit();
        currentState = nextState;
        nextState.Enter();
    }

}
