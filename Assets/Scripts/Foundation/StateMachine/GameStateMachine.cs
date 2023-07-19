using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Services _services;
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public GameStateMachine(Services services)
    {
        _services = services;
        _states = new Dictionary<Type, IState>()
        {
            [typeof(InitServicesState)] = new InitServicesState(),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        var state = ChangeState<TState>();
        state.Enter();
    }

    private IState ChangeState<TState>() where TState : class, IState
    {
        _currentState?.Exit();
        var state = GetState<TState>();
        _currentState = state;
        return state;
    }

    private IState GetState<TState>() where TState : class, IState => _states[typeof(TState)];
}