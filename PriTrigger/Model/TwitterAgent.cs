using Microsoft.Extensions.Configuration;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace PriTrigger.Model
{
    public class TwitterAgent : ITwitterAgent
    {
        public ReactivePropertySlim<string> SearchWord { get; } = new ReactivePropertySlim<string>();
        public ReactivePropertySlim<List<TweetData>> Tweets { get; } = new ReactivePropertySlim<List<TweetData>>();
        public TwitterAgent()
        {
            Tweets.Value = new List<TweetData>();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "settings.json")
                .AddUserSecrets<TwitterAgent>(optional: true)
                .Build();

            ApiKey = configuration["Twitter:ApiKey"];
            ApiKeySecret = configuration["Twitter:ApiKeySecret"];
            AccessToken = configuration["Twitter:AccessToken"];
            AccessTokenSecret = configuration["Twitter:AccessTokenSecret"];
        }

        async public Task SearchAsync()
        {
            var client = new TwitterClient(
                ApiKey,
                ApiKeySecret,
                AccessToken,
                AccessTokenSecret);
            try
            {
                var parameters = new SearchTweetsV2Parameters(SearchWord.Value);
                var response = await client.SearchV2.SearchTweetsAsync(parameters);
                Debug.WriteLine(response);

                var tmpList = new List<TweetData>();

                for (int i = 0; i < response.Tweets.Length; i++)
                {
                    var tweetDatas = response.Includes.Users
                      .Where(x => x.Id == response.Tweets[i].AuthorId)
                      .Select<UserV2, TweetData>(t =>
                         {
                             return new TweetData(t.Name,
                               t.Username, t.ProfileImageUrl, response.Tweets[i].Text);
                         });
                    foreach (var item in tweetDatas)
                    {
                        tmpList.Add(item);
                    }
                }
                Tweets.Value = tmpList;
            }
            catch (TwitterException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private string ApiKey { get; }
        private string ApiKeySecret { get; }
        private string AccessToken { get; }
        private string AccessTokenSecret { get; }
    }
}
