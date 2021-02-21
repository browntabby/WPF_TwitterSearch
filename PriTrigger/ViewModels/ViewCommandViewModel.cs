using Prism.Mvvm;
using Prism.Navigation;
using PriTrigger.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace PriTrigger.ViewModels
{
    public class ViewCommandViewModel : BindableBase, IDestructible
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        public ReactivePropertySlim<string> SearchWord { get; }
        public ReactiveCommand SearchButtonClick { get; }

        ITwitterAgent twitterAgent;
        public ViewCommandViewModel(ITwitterAgent twitterAgent)
        {
            this.twitterAgent = twitterAgent;
            SearchWord =  this.twitterAgent.SearchWord
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(this.disposables);
            this.SearchButtonClick = new ReactiveCommand()
                .WithSubscribe(() => this.OnSearchButton())
                .AddTo(this.disposables);

        }

        public void OnSearchButton()
        {
            twitterAgent.SearchAsync();
        }

        public void Destroy()
            => this.disposables.Dispose();
    }
}
