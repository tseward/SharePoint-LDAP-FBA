using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Security;

namespace Nauplius.ADLDS.UserProfiles.Features.LoggingFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("f213eb5a-b819-46a3-a7f6-c3e1d20f3b47")]
    public class LoggingFeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            RegisterLogging(properties, true);
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            RegisterLogging(properties, false);
        }

        static void RegisterLogging(SPFeatureReceiverProperties properties, bool register)
        {
            SPFarm farm = properties.Definition.Farm;

            if (farm != null)
            {
                Logging log = Logging.Local;

                if (register)
                {
                    if (log == null)
                    {
                        log = new Logging();
                        log.Update();

                        if (log.Status != SPObjectStatus.Online)
                        {
                            log.Provision();
                        }
                    }
                }
                else if (!register)
                {
                    if (log != null)
                    {
                        log.Delete();
                    }
                }
            }
        }

        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
