using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;

namespace CommerceManager
{
  public class Directory
  {
    public static string CreateSiteDirectory(string siteName)
    {
      string path = System.IO.Path.Combine(@"C:\inetpub\", siteName);
      System.IO.Directory.CreateDirectory(path);
      return path;
    }
  }

  public class ServerManager
  {
    private static Microsoft.Web.Administration.ServerManager serverManager = new Microsoft.Web.Administration.ServerManager();

    public static void CreateSite(string name, string webRootPath)
    {
      long siteId;
      ApplicationPool appPool = serverManager.ApplicationPools.Add(name);
      appPool.ManagedRuntimeVersion = "v4.0";
      appPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
      appPool.ProcessModel.PingingEnabled = false;

      appPool.Enable32BitAppOnWin64 = false;
      appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;

      appPool.ProcessModel.UserName = "Administrator";
      appPool.ProcessModel.Password = "Password12345";

      serverManager.CommitChanges();
      Site site = null;

      string bindingInformation = ":" + "80" + ":" + name;
      if (site == null)
      {
        site = serverManager.Sites.Add(name, "http", bindingInformation, webRootPath);
      }

      site.ApplicationDefaults.ApplicationPoolName = name;
      serverManager.CommitChanges();
    }

    public static ApplicationPool CreateApplicationPool(string name)
    {
      ApplicationPool appPool = serverManager.ApplicationPools.Add(name);
      appPool.ManagedRuntimeVersion = "v4.0";
      appPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
      appPool.ProcessModel.PingingEnabled = false;

      appPool.Enable32BitAppOnWin64 = false;
      appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;

      appPool.ProcessModel.UserName = "Administrator";
      appPool.ProcessModel.Password = "Password12345";
      
      serverManager.CommitChanges();return appPool;
    }

    public static string CreateCSServicesDirectory()
    {
      return Directory.CreateSiteDirectory(@"CSServices");
    }

    public static void CreateCSServicesSite()
    {
      var name = "CSServices";
      ServerManager.CreateSite(name, Directory.CreateSiteDirectory(name));
    }
  }
}
