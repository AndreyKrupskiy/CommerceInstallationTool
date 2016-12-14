using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceManager
{
  public class DebugLogger
  {
    public static void PrintToConsole(string message)
    {
      if(Program.IsDebug)
      Console.WriteLine("DEBUG: "+message);
    }
  }
}
