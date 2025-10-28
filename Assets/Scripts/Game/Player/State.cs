using UnityEngine;

public abstract class State
{
  protected PlayerController player;

  public State(PlayerController player) => this.player = player;

  public virtual void Enter() {}

  public abstract void Update(PlayerInput input);
}
