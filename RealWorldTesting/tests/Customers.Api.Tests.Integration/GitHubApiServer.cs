using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Customers.Api.Tests.Integration;
public sealed class GitHubApiServer : IDisposable
{
    private WireMockServer _server = default!;
    public string Url => _server.Url!;
    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetupUser(string username)
    {
        _server.Given(Request.Create()
            .WithPath($"/users/{username}")
            .UsingGet())
            .RespondWith(Response.Create()
            .WithBody(GenerateGitHubUserResponseBody(username))
            .WithHeader("Content-Type", "application/json; chartset=utf-8")
            .WithStatusCode(200));
    }

    private static string GenerateGitHubUserResponseBody(string username)
    {
        return $@"{{""login"":""{username}"",""id"":83103843,""node_id"":""MDQ6VXNlcjgzMTAzODQz"",""avatar_url"":""https://avatars.githubusercontent.com/u/83103843?v=4"",""gravatar_id"":"""",""url"":""https://api.github.com/users/{username}"",""html_url"":""https://github.com/{username}"",""followers_url"":""https://api.github.com/users/{username}/followers"",""following_url"":""https://api.github.com/users/{username}/following{{/other_user}}"",""gists_url"":""https://api.github.com/users/{username}/gists{{/gist_id}}"",""starred_url"":""https://api.github.com/users/{username}/starred{{/owner}}{{/repo}}"",""subscriptions_url"":""https://api.github.com/users/{username}/subscriptions"",""organizations_url"":""https://api.github.com/users/{username}/orgs"",""repos_url"":""https://api.github.com/users/{username}/repos"",""events_url"":""https://api.github.com/users/{username}/events{{/privacy}}"",""received_events_url"":""https://api.github.com/users/{username}/received_events"",""type"":""User"",""user_view_type"":""public"",""site_admin"":false,""name"":""Taner Saydam"",""company"":""{username}"",""blog"":""www.tanersaydam.net"",""location"":""Kayseri/Turkey"",""email"":null,""hireable"":null,""bio"":""https://www.udemy.com/user/{username}/"",""twitter_username"":""TanerSayda3308"",""public_repos"":262,""public_gists"":0,""followers"":445,""following"":8,""created_at"":""2021-04-24T02:35:38Z"",""updated_at"":""2024-10-09T16:20:54Z""}}";
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
