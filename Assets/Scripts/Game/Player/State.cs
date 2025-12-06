namespace Game.Player {
  public abstract class State {
    protected PlayerController Player { get; private set; }

    public virtual void Enter() { }

    public abstract void Update(PlayerInput input);

    protected void Transition<T>() where T : State, new() {
      var newState = new T {
        Player = this.Player
      };
      this.Player.State = newState;
      newState.Enter();
    }

    internal static State Init(PlayerController player) {
      return new IdleState {
        Player = player
      };
    }
  }
}