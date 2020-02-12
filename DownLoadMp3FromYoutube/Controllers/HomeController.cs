using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoLibrary;
using MediaToolkit;

namespace DownLoadMp3FromYoutube.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string v ) //default one id video youtube = r_aT7oYfnU8 --> mp3 file
        {
            if (!string.IsNullOrEmpty(v))
            {
                //Get video youtube from  VideoLibrary

                string pathLocal = HttpContext.Server.MapPath("~/App_Data") + "\\";  //path to save file

                var youtubeClient = YouTube.Default;
                var video = youtubeClient.GetVideo("https://www.youtube.com/watch?v=" + v);

                //Save video to local 
                System.IO.File.WriteAllBytes(pathLocal + video.FullName, video.GetBytes());


                //Convert video to mp3 use MediaToolkit

                var inputFile = new MediaToolkit.Model.MediaFile { Filename = pathLocal + video.FullName };
                var outputFile = new MediaToolkit.Model.MediaFile { Filename = $"{pathLocal + video.FullName}.mp3"};

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);

                }

                System.IO.File.Delete(inputFile.Filename);

                //Create action to dowload file mp3 
                ViewBag.FileNameDownload = video.FullName + ".mp3";
            }

            return View();
        }

        public ActionResult Download( string filename)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + filename);

            return File(fileBytes,System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}