using Prism.Mvvm;
using PriTrigger.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;

using System.Reactive.Disposables;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PriTrigger.ViewModels
{
    public class TweetItemViewModel : BindableBase
    {
        public ReadOnlyReactivePropertySlim<string> Name { get; }
        public ReadOnlyReactivePropertySlim<string> ScreenName { get; }
        public ReadOnlyReactivePropertySlim<string> IconUrl { get; }
        public ReadOnlyReactivePropertySlim<string> Text { get; }

        public ReactivePropertySlim<ImageSource> IconImage { get; }


        private CompositeDisposable disposables = new CompositeDisposable();
        private readonly TweetData tweetData;

        public TweetItemViewModel(TweetData tweetData)
        {
            this.tweetData = tweetData;

            this.IconUrl = this.tweetData.IconUrl
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this.disposables);

            this.ScreenName = this.tweetData.ScreenName
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this.disposables);

            this.Name = tweetData.Name
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this.disposables);

            this.Text = tweetData.Text
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this.disposables);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(this.IconUrl.Value);
            bitmapImage.EndInit();
            IconImage = new ReactivePropertySlim<ImageSource>( );
            IconImage.Value = bitmapImage;

        }
    }
}