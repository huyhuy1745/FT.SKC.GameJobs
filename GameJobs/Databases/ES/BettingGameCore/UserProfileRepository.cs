using System;
using System.Linq;
using Elasticsearch.Net;
using FT.NH88.GameJobs.Models;
using Nest;
using Newtonsoft.Json;

namespace FT.NH88.GameJobs.Databases.ES.BettingGameCore
{
    public class UserProfileRepository
    {
        private ElasticClient _esClient;
        

        private static readonly Lazy<UserProfileRepository> _instance = new Lazy<UserProfileRepository>(() => new UserProfileRepository());

        public static UserProfileRepository Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string url, string username, string password)
        {
            var uri = new Uri(url);
            var pool = new SingleNodeConnectionPool(uri);
            var settings = new ConnectionSettings(pool);
            settings.BasicAuthentication(username, password);
            settings.MaximumRetries(10);
            settings.MaxRetryTimeout(TimeSpan.FromSeconds(1));
            _esClient = new ElasticClient(settings);
        }

        public UserProfileRepository()
        {
        }

        public UserProfile GetById(long userID)
        {
            
            var response = _esClient.LowLevel.Search<SearchResponse<UserProfile>>(
                "bettinggamecore_userprofiles",
                PostData.String("{\"from\":0,\"size\":1000,\"query\":{\"bool\":{\"filter\":[{\"bool\":{\"must\":[{\"match_phrase\":{\"userid\":{\"query\":\""+userID+"\",\"slop\":0,\"zero_terms_query\":\"NONE\",\"boost\":1}}}],\"adjust_pure_negative\":true,\"boost\":1}}],\"adjust_pure_negative\":true,\"boost\":1}}}"));
            return response.Documents.FirstOrDefault();
        }

    //     public UserProfile GetById(long userID)
    //     {
    //         IRestResponse response = null;
    //         string query = $"SELECT * FROM bettinggamecore_userprofiles where userid = {userID}";
    //         try
    //         {
    //             var client = new RestClient($"{_url}/_nlpcn/sql");
    //             client.Timeout = -1;
    //             client.Authenticator = new HttpBasicAuthenticator(_username, _password);
    //             var request = new RestRequest(Method.POST);
    //             request.AddHeader("Connection", "keep-alive");
    //             request.AddHeader("Accept", "application/json, text/plain, */*");
    //             request.AddHeader("Content-Type", "application/json;charset=UTF-8");
    //             request.AddHeader("Accept-Language", "en-US,en;q=0.9,vi;q=0.8");
    //             request.AddParameter("application/json;charset=UTF-8", query,  ParameterType.RequestBody);
    //             response = client.Execute(request);
    //             UserProfileResponse userProfileRepository = JsonConvert.DeserializeObject<UserProfileResponse>(response.Content);
    //
    //
    //             if (userProfileRepository.Hits?.HitsHits == null)
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - ip: [{_url}] - query: [{query}] - response.Content: [{response.Content}]");
    //                 return null;
    //             }
    //             if (userProfileRepository.Hits.HitsHits.Length == 0)
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - ip: [{_url}] - query: [{query}] - response.Content: [{response.Content}]");
    //                 return null;
    //             }
    //         
    //             return userProfileRepository.Hits.HitsHits[0].Source;
    //         }
    //         catch (Exception e)
    //         {
    //             if (response != null)
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - ip: [{_url}] - query: [{query}] - response.Content: [{response.Content}]",e);
    //
    //             }
    //             else
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - ip: [{_url}] - query: [{query}]",e);
    //             }
    //             return null;
    //         }
    //     }
    // }
    
    // public UserProfile GetById(long userID)
    //     {
    //         IRestResponse response = null;
    //         string url = $"{_url}/bettinggamecore_userprofiles/_search";
    //         string query =
    //             "{\"from\":0,\"size\":1000,\"query\":{\"bool\":{\"filter\":[{\"bool\":{\"must\":[{\"match_phrase\":{\"userid\":{\"query\":" +
    //             userID +
    //             ",\"slop\":0,\"zero_terms_query\":\"NONE\",\"boost\":1}}}],\"adjust_pure_negative\":true,\"boost\":1}}],\"adjust_pure_negative\":true,\"boost\":1}}}";
    //         
    //         try
    //         {
    //             var client = new RestClient(url);
    //             client.Authenticator = new HttpBasicAuthenticator(_username, _password);
    //             var request = new RestRequest(Method.POST);
    //             request.AddHeader("Content-Type", "application/json");
    //             
    //             request.AddParameter("application/json", query,  ParameterType.RequestBody);
    //             response = client.Execute(request);
    //
    //             if (response.ErrorException != null)
    //             {
    //                 throw response.ErrorException;
    //             }
    //             
    //             UserProfileResponse userProfileRepository = JsonConvert.DeserializeObject<UserProfileResponse>(response.Content);
    //             
    //             if (userProfileRepository?.Hits?.HitsHits == null)
    //             {
    //                 Console.WriteLine("--------------------------------------------");
    //                 Console.WriteLine(url);
    //                 Console.WriteLine(query);
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - url: [{_url}] - response.Content: [{response.Content}]");
    //                 return null;
    //             }
    //             if (userProfileRepository.Hits.HitsHits.Length == 0)
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - url: [{_url}] - response.Content: [{response.Content}]");
    //                 return null;
    //             }
    //         
    //             return userProfileRepository.Hits.HitsHits[0].Source;
    //         }
    //         catch (Exception e)
    //         {
    //             Console.WriteLine("--------------------------------------------");
    //             Console.WriteLine(url);
    //             Console.WriteLine(query);
    //             if (response != null)
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - url: [{_url}] - response.Content: [{response.Content}]",e);
    //
    //             }
    //             else
    //             {
    //                 Logger.Error($"UserProfileRepository.GetById - userID: [{userID}] - url: [{_url}]",e);
    //             }
    //             return null;
    //         }
    //     }
    }
    
    public class UserProfileResponse
    {
        [JsonProperty("took")]
        public long Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public Shards Shards { get; set; }

        [JsonProperty("hits")]
        public UserProfileHits Hits { get; set; }
    }

    public class UserProfileHits
    {
        [JsonProperty("total")]
        public Total Total { get; set; }

        [JsonProperty("max_score")]
        public long? MaxScore { get; set; }

        [JsonProperty("hits")]
        public UserProfileHit[] HitsHits { get; set; }
    }

    public class UserProfileHit
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("_score")]
        public long Score { get; set; }

        [JsonProperty("_source")]
        public UserProfile Source { get; set; }
    }

    public class Total
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }
    }

    public class Shards
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("successful")]
        public long Successful { get; set; }

        [JsonProperty("skipped")]
        public long Skipped { get; set; }

        [JsonProperty("failed")]
        public long Failed { get; set; }
    }



}