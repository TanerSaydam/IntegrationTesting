using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

var wiremockServer = WireMockServer.Start();

Console.WriteLine($"Wiremock is now running on: {wiremockServer.Url}");

wiremockServer.Given(Request.Create()
    .WithPath("/users/tanersaydam")
    .UsingGet())
    .RespondWith(Response.Create()
    .WithBodyAsJson(@"{""login"":""TanerSaydam"",""id"":83103843,""node_id"":""MDQ6VXNlcjgzMTAzODQz"",""avatar_url"":""https://avatars.githubusercontent.com/u/83103843?v=4"",""gravatar_id"":"""",""url"":""https://api.github.com/users/TanerSaydam"",""html_url"":""https://github.com/TanerSaydam"",""followers_url"":""https://api.github.com/users/TanerSaydam/followers"",""following_url"":""https://api.github.com/users/TanerSaydam/following{/other_user}"",""gists_url"":""https://api.github.com/users/TanerSaydam/gists{/gist_id}"",""starred_url"":""https://api.github.com/users/TanerSaydam/starred{/owner}{/repo}"",""subscriptions_url"":""https://api.github.com/users/TanerSaydam/subscriptions"",""organizations_url"":""https://api.github.com/users/TanerSaydam/orgs"",""repos_url"":""https://api.github.com/users/TanerSaydam/repos"",""events_url"":""https://api.github.com/users/TanerSaydam/events{/privacy}"",""received_events_url"":""https://api.github.com/users/TanerSaydam/received_events"",""type"":""User"",""user_view_type"":""public"",""site_admin"":false,""name"":""Taner Saydam"",""company"":""Taner Saydam"",""blog"":""www.tanersaydam.net"",""location"":""Kayseri/Turkey"",""email"":null,""hireable"":null,""bio"":""https://www.udemy.com/user/taner-saydam/"",""twitter_username"":""TanerSayda3308"",""public_repos"":262,""public_gists"":0,""followers"":445,""following"":8,""created_at"":""2021-04-24T02:35:38Z"",""updated_at"":""2024-10-09T16:20:54Z""}")
    .WithHeader("content-type", "application/json; charset=utf-8")
    .WithStatusCode(200)
    );

Console.ReadKey();
wiremockServer.Dispose();