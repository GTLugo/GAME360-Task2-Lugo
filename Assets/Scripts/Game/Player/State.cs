namespace Game.Player {
  public abstract class State {
    internal PlayerController player;

    public virtual void Enter() { }

    public abstract void Update(PlayerInput input);
  }
}