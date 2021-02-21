using Reactive.Bindings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriTrigger.Model
{
    public interface ITwitterAgent
    {
        public ReactivePropertySlim<string> SearchWord { get; }
        public ReactivePropertySlim<List<TweetData>> Tweets { get; }
        public Task SearchAsync();
    }
}
