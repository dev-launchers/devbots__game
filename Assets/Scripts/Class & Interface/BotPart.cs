using UnityEngine;
/// <summary>
/// This class is used as a base class for the different types of botparts
/// </summary>
public abstract class BotPart : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    protected float _timer;
    
    public abstract void BotPartUpdate();
    abstract public void SetState(State state);
    //[SerializeField] abstract private bool isRunning;

    public float GetCoolDownTimer(){ 
        return _timer;
    }

    public float GetCoolDown() {
        return _coolDown;
    }

    public bool IsPartCoolingDown() {
        return _timer > 0;
    }

    private void AdvanceCooldownTimer() {
        _timer -= Time.deltaTime;
    }

    public void ResetCooldownTimer() {
        _timer = _coolDown;
    }

    private void Update() {
        AdvanceCooldownTimer();
        BotPartUpdate();
    }


}
