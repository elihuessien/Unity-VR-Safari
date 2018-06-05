namespace SM
{
    public class StateMachine<S>
    {
        public State<S> currentstate { get; set; }
        public S owner;

        public StateMachine(S o)
        {
            owner = o;
            currentstate = null;
        }


        public void ChangeState(State<S> newState)
        {
            if (currentstate != null)
                currentstate.ExitState(owner);
            currentstate = newState;
            currentstate.EnterState(owner);
        }

        public void Update()
        {
            if(currentstate != null)
                currentstate.UpdateState(owner);
        }
    }


    public abstract class State<S>
    {
        public abstract void EnterState(S owner);
        public abstract void ExitState(S owner);
        public abstract void UpdateState(S owner);
    }
}
