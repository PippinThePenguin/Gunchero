using System;

//Something that controls agent behaviour.
public interface IBehaviourController {
  public event Action<bool> OnStatusChange;
  public bool CanFire { get;}
  
}
