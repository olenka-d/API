//Для кожного відео з плейлиста виконати запит до API, який поверне повну інформацію про кожне відео окремо. 
//Знайти там параметри "viewCount" та "likeCount". І порахувати скільки сумарно було переглядів та лайків у всіх відео плейліста
//API було приховано
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

namespace YouTube_API_bonus
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
        public Statistics statistics { get; set; }
    }

    public class Localized
    {
        public string title { get; set; }
        public string description { get; set; }
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

    public class Root
    {
        public string kind { get; set; }
        public string etag { get; set; }
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
        public string categoryId { get; set; }
        public string liveBroadcastContent { get; set; }
        public Localized localized { get; set; }
    }

    public class Standard
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Statistics
    {
        public string viewCount { get; set; }
        public string likeCount { get; set; }
        public string favoriteCount { get; set; }
        public string commentCount { get; set; }
    }

    public class Thumbnails
    {
        public Default @default { get; set; }
        public Medium medium { get; set; }
        public High high { get; set; }
        public Standard standard { get; set; }
        public Maxres maxres { get; set; }
    }


    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&key={YOUR_API_KEY}&id=";
        async static Task Main(string[] args)
        {
            await getVideos();
            //Console.WriteLine("Hello, World!");
            
            async Task getVideos()
            {
                string[] arr_id_videos = new string[] { "sqVMhqSOSzo", "5Y-SGvb3e0Y", "eS-hnV-KwaQ", "9vPUGpJIzek", "_3sxgcVQx9o", "r0Wca9JCDeA" };
                //Console.WriteLine("Getting JSON...");
                int countLike = 0;
                int countView = 0;
                foreach (string i in arr_id_videos)
                {
                    var responseString = await client.GetStringAsync(url+i);
                    //Console.WriteLine("Parsing JSON...");
                    Root videos_yt = JsonSerializer.Deserialize<Root>(responseString);
                    
                    foreach (var video in videos_yt.items)
                    {
                        Console.WriteLine($"title - {video.snippet.title}\t");
                        Console.Write($"count 'Like' - {video.statistics.likeCount}\t");
                        Console.Write($"count 'View' - {video.statistics.viewCount}\t");
                        Console.WriteLine();
                        countLike += Int32.Parse(video.statistics.likeCount);
                        countView += Int32.Parse(video.statistics.viewCount);
                    }
                }
                Console.WriteLine($"Total like's - {countLike}, total view's - {countView}");
            }
            Console.ReadKey();
        }
    }
}
