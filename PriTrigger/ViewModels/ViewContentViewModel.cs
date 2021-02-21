using Prism.Mvvm;
using Prism.Navigation;
using PriTrigger.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace PriTrigger.ViewModels
{
    public class ViewContentViewModel : BindableBase, IDestructible
    {
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        /// <summary>ListBoxのItemsSource</summary>
        public ReadOnlyReactiveCollection<TweetItemViewModel> Tweets { get; set; }
        /// <summary>ListBoxの選択中アイテム</summary>
        public ReactiveProperty<TweetItemViewModel> SelectedItem { get; set; }

        private ObservableCollection<TweetData> ListTweetDatas { get; set; }
        public ReactivePropertySlim<List<TweetData>> TweetDatas { get; }

        readonly ITwitterAgent twitterAgent;
        public ViewContentViewModel(ITwitterAgent twitterAgent)
        {
            this.twitterAgent = twitterAgent;
            TweetDatas = this.twitterAgent.Tweets
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(this.disposables);
            TweetDatas.Subscribe(x => this.OnChangeTweetData(x));

        }

        void OnChangeTweetData(List<TweetData> tweetDatas)
        {
            if (this.ListTweetDatas == null)
            {
                this.ListTweetDatas = new ObservableCollection<TweetData>(TweetDatas.Value);
            }
            else
            {
                this.ListTweetDatas.Clear();
                this.ListTweetDatas.AddRange<TweetData>(tweetDatas);
            }
            this.Tweets = this.ListTweetDatas
                .ToReadOnlyReactiveCollection(c => new TweetItemViewModel(c));

        }

        public void Destroy()
            => this.disposables.Dispose();
    }
}
