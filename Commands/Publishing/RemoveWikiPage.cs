﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Utilities;
using SharePointPnP.PowerShell.CmdletHelpAttributes;

namespace SharePointPnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Remove, "SPOWikiPage", ConfirmImpact = ConfirmImpact.High)]
    [CmdletHelp("Removes a wiki page",
        Category = CmdletHelpCategory.Publishing)]
    public class RemoveWikiPage : SPOWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0,ValueFromPipeline=true, ParameterSetName = "SERVER")]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "SITE")]
        public string SiteRelativePageUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "SITE")
            {
                var serverUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
                ServerRelativePageUrl = UrlUtility.Combine(serverUrl, SiteRelativePageUrl);
            }

            var file = SelectedWeb.GetFileByServerRelativeUrl(ServerRelativePageUrl);

            file.DeleteObject();

            ClientContext.ExecuteQueryRetry();
        }
    }
}
