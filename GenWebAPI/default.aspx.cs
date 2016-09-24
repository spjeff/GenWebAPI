using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.IO.Compression;

namespace GenWebAPI
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Generate_Click(object sender, EventArgs e)
        {
            string WebAPIName = ProjectName.Text;

            Guid assemblyGuid = Guid.NewGuid();
            string baseFolder = Server.MapPath("~/") + "ZIPTEMP\\" + assemblyGuid + "\\";
            if (Directory.Exists(baseFolder))
            {
                Directory.Delete(baseFolder, true);
            }
            Directory.CreateDirectory(baseFolder);
            ZipFile.ExtractToDirectory(Server.MapPath("~/") + "HelloTime.zip", baseFolder);

            //base download folder
            string baseDownload = HttpContext.Current.Server.MapPath("~/") + "download" + "\\";
            if (!Directory.Exists(baseDownload))
            {
                Directory.CreateDirectory(baseDownload);
            }

            //inventory
            string[] replaceFiles = {
                @"HelloTime.sln",
                @"HelloTime\App_Start\RouteConfig.cs",
                @"HelloTime\Global.asax.cs",
                @"HelloTime\Global.asax",
                @"HelloTime\Properties\PublishProfiles\WD.pubxml",
                @"HelloTime\App_Start\WebApiConfig.cs",
                @"HelloTime\Controllers\HelloController.cs",
                @"HelloTime\HelloTime.csproj",
                @"HelloTime\Properties\AssemblyInfo.cs",
                @"HelloTime\readme.aspx.cs",
                @"HelloTime\readme.aspx",
                @"HelloTime\readme.aspx.designer.cs",
                @"HelloTime\App_Start\FilterConfig.cs" };

            foreach (string r in replaceFiles)
            {
                string file = baseFolder + r;
                string txt = File.ReadAllText(file);
                txt = txt.Replace("HelloTime", WebAPIName);
                if (file.Contains("AssemblyInfo"))
                {
                    txt = txt.Replace("bf297e58-ada2-483c-bea4-9afe8a8e8c61", assemblyGuid.ToString());
                }
                File.WriteAllText(file, txt);
            }

            File.Move(baseFolder + "HelloTime.sln", baseFolder + WebAPIName + ".sln");
            File.Move(baseFolder + "HelloTime.v12.suo", baseFolder + WebAPIName + ".v12.suo");
            File.Move(baseFolder + "HelloTime\\HelloTime.csproj", baseFolder + "HelloTime\\" + WebAPIName + ".csproj");
            File.Move(baseFolder + "HelloTime\\HelloTime.csproj.user", baseFolder + "HelloTime\\" + WebAPIName + ".csproj.user");

            Directory.Move(baseFolder + "HelloTime", (baseFolder + WebAPIName));
            string downloadZip = baseDownload + WebAPIName + ".zip";

            using (FileStream stream = new FileStream(downloadZip, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    foreach (string path in Directory.GetFiles(baseFolder, "*.*", SearchOption.AllDirectories))
                    {
                        string dir = Path.GetDirectoryName(path);
                        string newdir = dir.Replace(baseFolder, "");
                        if (newdir.Substring(1, 1) != ":")
                        {
                            Console.WriteLine(newdir);
                            archive.CreateEntry(newdir + "\\", CompressionLevel.Fastest);
                        }
                    }

                    foreach (string path in Directory.GetFiles(baseFolder, "*.*", SearchOption.AllDirectories))
                    {
                        byte[] bytes = File.ReadAllBytes(path);
                        ZipArchiveEntry entry = archive.CreateEntry(path.Replace(baseFolder, ""), CompressionLevel.Fastest);
                        using (Stream content = entry.Open())
                        {
                            content.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }

            //download to HTTP client
            HttpContext.Current.Response.ContentType = "application/zip";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + WebAPIName + ".zip");
            HttpContext.Current.Response.TransmitFile(downloadZip);
            HttpContext.Current.Response.End();

            //clean up
            if (Directory.Exists(baseFolder))
            {
                Directory.Delete(baseFolder, true);
            }
            string zipfile = WebAPIName + ".zip";
            if (File.Exists(zipfile))
            {
                File.Delete(zipfile);
            }

        }
    }
}