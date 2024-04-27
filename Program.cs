using System;
using System.Net;
using System.Threading;
using System.IO;

class Program
{
    static string fileLocation = @"C:\Users\beta laptop\Desktop\hw4-files";
    static int completedDownloads = 0;

    public static void Main(string[] args)
    {
        // Console.WriteLine("Please Enter to downloads files.");

        string[] urls = new string[]
        {
            "https://purepng.com/public/uploads/large/purepng.com-moonmoonskyrealisticnightblack-and-white-591521584248xjtz1.png",
            "https://freepngimg.com/download/sea/28979-8-sea-transparent-image.png",
            "https://irsv.upmusics.com/Downloads/Musics/Mohsen%20Chavoshi%20-%20Mariz%20Hali%20(320).mp3"
        };

        foreach (string url in urls)
        {
            string name = GetFileNameFromUrl(url);
            DownloadFile(url, Path.Combine(fileLocation, name));
        }

        while (completedDownloads < urls.Length)
        {
            // Wait for all downloads to complete
        }

        Console.WriteLine("The files have been successfully downloaded!");
        Console.WriteLine("Please enter to exit.");
        Console.ReadLine();
    }

    public static void DownloadFile(string url, string fileName)
    {
        Thread thread = new Thread(() => download(url, fileName));
        thread.Start();
    }

    public static void download(string url, string name)
    {
        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(new Uri(url), name);
                client.DownloadProgressChanged += (sender, e) =>
                {
                    //Console.WriteLine($"Downloading '{name}': {e.ProgressPercentage}");
                };
            }
            Console.WriteLine($"Download of '{name}' completed!");
            Interlocked.Increment(ref completedDownloads);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred for '{url}'! Error message: {e.Message}");
        }
    }

    public static string GetFileNameFromUrl(string url)
    {
        Uri uri = new Uri(url);
        string fileName = Path.GetFileName(uri.LocalPath);
        return fileName;
    }
}



/*     public static void download(string url, string fileName)
 {
       using (WebClient client = new WebClient())
       {
           client.DownloadProgressChanged += (sender, e) =>
           {
               Console.WriteLine($"Downloading '{Path.GetFileName(fileName)}': {e.ProgressPercentage}%");
           };

           client.DownloadFileCompleted += (sender, e) =>
           {
               Console.WriteLine($"Download of '{Path.GetFileName(fileName)}' completed!");
               Interlocked.Increment(ref completedDownloads);
           };

           client.DownloadFileAsync(new Uri(url), fileName);
       }
   }*/
