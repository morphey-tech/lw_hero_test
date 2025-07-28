using System;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    [UsedImplicitly]
    public sealed class HeroStatsPresenter : IInitializable, IDisposable
    {
        [Inject]
        private HeroStatsPresenter()
        {
            
        }
        
        void IInitializable.Initialize()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}