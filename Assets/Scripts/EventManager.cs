using System;
using UnityEngine;
public class EventManager : GenericSingleton<EventManager>
{
    public Action onChangedLevel;
    public Action onFinish;
    public Action onClick;
    public Action onScored;
    public Action<object> onCollectable;
    public Action onBackMenu;
    public Action onUIClick;
    public Action onStart;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnChangedLevel()
    {
        onChangedLevel?.Invoke();
    }
    public void OnClick()
    {
        onClick?.Invoke();
    }
    public void OnScored()
    {
        onScored?.Invoke();
    }
    public void OnFinish()
    {
        onFinish?.Invoke();
    }
    public void OnCollectable(object position) => onCollectable?.Invoke(position);
    public void OnBackMenu() => onBackMenu?.Invoke();
    public void OnUiClick() => onUIClick?.Invoke();
    public void OnStart() => onStart?.Invoke();
    
}
