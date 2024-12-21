namespace Game
{
    using System;
    using System.Collections.Generic;

    public static class GameStateFactory
    {
        private static readonly Dictionary<GameStateType, IGameState> _stateCache = new Dictionary<GameStateType, IGameState>();

        public static IGameState GetState(GameStateType stateType)
        {
            if (_stateCache.TryGetValue(stateType, out var existingState))
            {
                return existingState;
            }


            var stateInstance = CreateStateInstance(stateType) ?? throw new ArgumentException($"GameStateFactory: Could not create state for type {stateType}");
            _stateCache[stateType] = stateInstance;
            return stateInstance;
        }

        private static IGameState CreateStateInstance(GameStateType stateType)
        {
            string className = $"Game.{stateType}State";

            Type stateClassType = Type.GetType(className) ?? throw new ArgumentException($"GameStateFactory: Class not found for {stateType}. Ensure the class name is correct and in the proper namespace.");

            if (Activator.CreateInstance(stateClassType) is not IGameState stateInstance)
            {
                throw new InvalidCastException($"GameStateFactory: Unable to cast {className} to IGameState. Ensure it implements the IGameState interface.");
            }

            return stateInstance;
        }

    }

}