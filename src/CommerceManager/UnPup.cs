using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;

namespace CommerceManager
{
  public class UnPup
  {
    public static void Process(string siteName, string pupFolderPath)
    {
      //C:\Program Files (x86)\Commerce Server 11\PuP.exe
      System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
      pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Commerce Server 11\PuP.exe";
      string pupFileLocation = System.IO.Path.Combine(pupFolderPath, "SolutionStorefront.pup");
      string pupIniPath = System.IO.Path.Combine(pupFolderPath, PrepareIni(pupFolderPath));
      pProcess.StartInfo.Arguments = string.Format("/u /s:{0} /f:{1} /i:{2}", siteName, pupFileLocation, pupIniPath); //argument
      pProcess.StartInfo.UseShellExecute = false;
      pProcess.StartInfo.RedirectStandardOutput = true;
      pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
      pProcess.Start();
      string output = pProcess.StandardOutput.ReadToEnd(); //The output result
      pProcess.WaitForExit();
    }

    protected static string PrepareIni(string pupFolderPath)
    {
      var r = new Random();
      string filename = "CSSini" + r.Next()+".ini";
      using (FileStream fs = File.Create(Path.Combine(pupFolderPath, filename)))
      {
        StreamWriter sw = new StreamWriter(fs);
        using (FileStream fo = File.OpenRead(Path.Combine(pupFolderPath, "SolutionStorefront.ini")))
        {
          StreamReader sr = new StreamReader(fo);
          string str;
          while ((str = sr.ReadLine()) != null)
          {
            str =
              str.Replace("$($PROJECT_NAME)", "CSReferenceStorefront")
                .Replace("$($CS_SQL_Server)", Environment.MachineName)
                .Replace("$($CS_WEBSERVICE_IIS_SITE_NAME)", "CSServices")
                .Replace("$($CS_WEB_SERVER)", Environment.MachineName);
            sw.WriteLine(str);
          }
          sw.Flush();
          sw.Dispose();
          sr.Dispose();
        }
      }
      return filename;
    }
  }
}
