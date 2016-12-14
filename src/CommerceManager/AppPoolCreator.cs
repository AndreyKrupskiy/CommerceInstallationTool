using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceManager
{
  public class AppPoolCreator
  {
    public static void CreateAppPoolsForServices()
    {
      var serverManager = new Microsoft.Web.Administration.ServerManager();
      var site = serverManager.Sites.FirstOrDefault(aSite => aSite.Name == "CSServices");
      if (site != null)
        foreach (var app in site.Applications)
        {
          
          DebugLogger.PrintToConsole(string.Format("path is\"{0}\"", app.Path));
          var name = app.Path.Trim(@" /\".ToCharArray());
          if (name.Length > 0)
          {
            DebugLogger.PrintToConsole(string.Format("app pool name is\"{0}\"", name));
            ServerManager.CreateApplicationPool(name);

            app.ApplicationPoolName = name;
            serverManager.CommitChanges();
          }
        }
    }

  }
}
