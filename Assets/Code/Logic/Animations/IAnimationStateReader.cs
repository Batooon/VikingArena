using System.Collections.Generic;

namespace Code.Logic.Animations
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash, int layerId);
        void ExitedState(int stateHash, int layerId);
        Dictionary<int, AnimatorState> StatePerLayer { get; }
    }
}