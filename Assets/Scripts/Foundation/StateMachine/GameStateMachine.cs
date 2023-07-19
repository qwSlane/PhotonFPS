using System;
using System.Collections.Generic;
using DefaultNamespace.Services.Factory;

public class GameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(Services services)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(InitLobbyState)] = new InitLobbyState(this, services.Get<IFactory>()),
            [typeof(LoadGameState)] = new LoadGameState(services.Get<IFactory>(), services.Get<INetworkRunner>()),
            [typeof(GameLoopState)]  = new GameLoopState(),
            [typeof(LoadLobbyState)]  = new LoadLobbyState(),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        var state = ChangeState<TState>();
        state.Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
        var state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();
        var state = GetState<TState>();
        _currentState = state;
        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState  => _states[typeof(TState)] as TState;
}