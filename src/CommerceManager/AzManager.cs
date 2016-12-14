using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Interop.Security.AzRoles;

namespace CommerceManager
{
  public class AzManager
  {
    public static void AddAdminToRoles()
    {
      NTAccount f = new NTAccount("Administrator");
      SecurityIdentifier s = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
      String sidString = s.ToString();
      UpdateCatalogWebService(sidString);
      UpdateMarketingWebService(sidString);
      UpdateOrderWebService(sidString);
      UpdateProfileWebService(sidString);
    }

    protected static void UpdateProfileWebService(string adminSidString)
    {
      string connectionString = @"msxml://C:\inetpub\CSServices\CSReferenceStorefront_ProfilesWebService\ProfilesAuthorizationStore.xml";

      AzAuthorizationStoreClass azStore = new AzAuthorizationStoreClass();
      azStore.Initialize(0, connectionString, null);

      IAzApplication2 azApplication = azStore.OpenApplication2("ProfileSystem", null);
      foreach (IAzRole role in azApplication.Roles)
      {
        if (role.Name == "ProfileAdministrator")
        {
          role.AddMember(adminSidString, null);
          role.Submit(0, null);
        }
      }
    }
    protected static void UpdateCatalogWebService(string adminSidString)
    {
      string connectionString = @"msxml://C:\inetpub\CSServices\CSReferenceStorefront_CatalogWebService\CatalogAuthorizationStore.xml";

      AzAuthorizationStoreClass azStore = new AzAuthorizationStoreClass();
      azStore.Initialize(0, connectionString, null);

      IAzApplication2 azApplication = azStore.OpenApplication2("CatalogandInventorySystem", null);
      foreach (IAzRole role in azApplication.Roles)
      {
        if (role.Name == "Administrator" || role.Name=="CatalogAdministrator")
        {
          role.AddMember(adminSidString, null);
          role.Submit(0, null);
        }
      }
    }
    protected static void UpdateMarketingWebService(string adminSidString)
    {
      string connectionString = @"msxml://C:\inetpub\CSServices\CSReferenceStorefront_MarketingWebService\MarketingAuthorizationStore.xml";

      AzAuthorizationStoreClass azStore = new AzAuthorizationStoreClass();
      azStore.Initialize(0, connectionString, null);

      IAzApplication2 azApplication = azStore.OpenApplication2("MarketingSystem", null);
      foreach (IAzRole role in azApplication.Roles)
      {
        if (role.Name == "MarketingAdministrator")
        {
          role.AddMember(adminSidString, null);
          role.Submit(0, null);
        }
      }
    }
    protected static void UpdateOrderWebService(string adminSidString)
    {
      string connectionString = @"msxml://C:\inetpub\CSServices\CSReferenceStorefront_OrdersWebService\OrdersAuthorizationStore.xml";

      AzAuthorizationStoreClass azStore = new AzAuthorizationStoreClass();
      azStore.Initialize(0, connectionString, null);

      IAzApplication2 azApplication = azStore.OpenApplication2("OrderSystem", null);
      foreach (IAzRole role in azApplication.Roles)
      {
        if (role.Name == "OrdersAdministrator")
        {
          Console.WriteLine("Role found!");
          role.AddMember(adminSidString, null);
          role.Submit(0, null);
        }
      }
    }
  }
}
