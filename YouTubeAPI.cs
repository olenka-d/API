//Отримати JSON з YouTube API який дасть список відео з цього плейліста:
//https://www.youtube.com/watch?v=sqVMhqSOSzo&list=PLSN6qXliOioz5lnckfofNcLJ3CnZJvEJO
//API було приховано

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace YouTube_API
{
    public class Default
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class High
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
    }
    public class Maxres
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class Medium
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }
    public class ResourceId
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }
    public class Root
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public List<Item> items { get; set; }
        public PageInfo pageInfo { get; set; }
    }
    public class Snippet
    {
        public DateTime publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string channelTitle { get; set; }
        public string playlistId { get; set; }
        public int position { get; set; }
        public ResourceId resourceId { get; set; }
        public string videoOwnerChannelTitle { get; set; }
        public string videoOwnerChannelId { get; set; }
    }
    public class Standard
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class Thumbnails
    {
        public Default @default { get; set; }
        public Medium medium { get; set; }
        public High high { get; set; }
        public Standard standard { get; set; }
        public Maxres maxres { get; set; }
    }

    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=PLSN6qXliOioz5lnckfofNcLJ3CnZJvEJO&key={YOUR_API_KEY}&maxResults=6";
        async static Task Main(string[] args)
        {
            await getVideos();
            //Console.WriteLine("Hello, World!");

            async Task getVideos()
            {
                Console.WriteLine("Getting JSON...");
                var responseString = await client.GetStringAsync(url);
                Console.WriteLine("Parsing JSON...");
                Root videos_yt = JsonSerializer.Deserialize<Root>(responseString);
                foreach (var video in videos_yt.items)
                {
                    Console.WriteLine(video.snippet.title);
                }
            }
            Console.ReadKey();
        }
    }
}
