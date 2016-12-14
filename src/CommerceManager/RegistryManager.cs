using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CommerceManager
{
  public class RegistryManager
  {
    public static void AddMultistringValue(string path, string name, string value)
    {
      Microsoft.Win32.Registry.LocalMachine.CreateSubKey(path).SetValue(name, new string[] { value }, RegistryValueKind.MultiString);
      //CreateSubKey(@"/SYSTEM/CurrentControlSet/Control/Lsa/MSV1_0/BackConnectionHost1", RegistryKeyPermissionCheck.ReadWriteSubTree);
    }

    public static void AddDwordValue(string path, string name, int value)
    {
      Microsoft.Win32.Registry.LocalMachine.CreateSubKey(path).SetValue(name, value, RegistryValueKind.DWord);
    }


  }
}
