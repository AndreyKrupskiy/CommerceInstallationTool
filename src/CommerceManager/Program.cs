using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CommerceManager
{
  class Program
  {
    static void Main(string[] args)
    {
      //RegistryManager.AddDwordValue(@"SOFTWARE\Microsoft\InetStp\Configuration", "MaxWebConfigFileSizeInKB", 500);
      Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\InetStp\Configuration").SetValue("MaxWebConfigFileSizeInKB", 0x000001f4, RegistryValueKind.DWord);
      Console.WriteLine("Is debug?");
      Program.IsDebug = false;
      string ans1 = Console.ReadLine();
      if (ans1.ToLowerInvariant() == "y") IsDebug = true;
      var webRoot = ServerManager.CreateCSServicesDirectory();
      ServerManager.CreateCSServicesSite();
      string sitename;
      string pupLocation;
      string iniLocation;
      Console.WriteLine("Enter sitename");
      sitename = Console.ReadLine();
      Console.WriteLine("Enter pup folder location");
      pupLocation = Console.ReadLine();
      UnPup.Process(sitename,pupLocation);
      
      AppPoolCreator.CreateAppPoolsForServices();
      Console.WriteLine("press any key");
      Console.ReadLine();

      AzManager.AddAdminToRoles();
      NTAccount f = new NTAccount("Administrator");
      SecurityIdentifier s = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
      String sidString = s.ToString();
      Console.WriteLine(sidString);
      Console.ReadLine();

      RegistryManager.AddMultistringValue(@"SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0", "BackConnectionHostNames", "CSServices");
      //RegistryManager.AddDwordValue(@"SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0", "BackConnectionHost", 0x000001f4);
    }



    public static bool IsDebug { get; set; }
  }


}
