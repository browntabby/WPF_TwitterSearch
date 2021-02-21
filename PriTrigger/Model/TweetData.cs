using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Windows.Media;

namespace PriTrigger.Model
{
    public class TweetData : IDisposable
    {
        protected CompositeDisposable disposables = new CompositeDisposable();
        private bool disposedValue = false;

        /// <summary>ScreenName（アカウント名）を取得・設定</summary>
        public ReactivePropertySlim<string> ScreenName { get; }
        /// <summary>Name（名前）を取得・設定</summary>
        public ReactivePropertySlim<string> Name { get; set; }

        public ReactivePropertySlim<string> IconUrl { get; set; }

        public ReactivePropertySlim<ImageSource> IconImage { get; set; }

        public ReactivePropertySlim<string> Text { get; set; }
        public TweetData() : this(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }
        public TweetData(string name, string screenName, string iconUrl, string text)
        {
            this.Name = new ReactivePropertySlim<string>(name)
                .AddTo(this.disposables);
            this.ScreenName = new ReactivePropertySlim<string>(screenName)
                .AddTo(this.disposables);
            this.IconUrl = new ReactivePropertySlim<string>(iconUrl)
                .AddTo(this.disposables);
            this.Text = new ReactivePropertySlim<string>(text)
                .AddTo(this.disposables);
            this.IconImage = new ReactivePropertySlim<ImageSource>()
                .AddTo(this.disposables);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.disposables.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}

