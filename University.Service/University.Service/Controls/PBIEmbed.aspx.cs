using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;

using University.Api.Common;
using University.Dto.Zystem;
using University.Dao.Zystem;

namespace University.Api.Controls
{
    public partial class PBIEmbed : System.Web.UI.Page
    {
        string strTenantId = ConfigurationManager.AppSettings["tenantId"];
        string strApplicationId = ConfigurationManager.AppSettings["ApplicationId"];
        string strApplicationObject = ConfigurationManager.AppSettings["ApplicationObject"];
        string strClientSecret = ConfigurationManager.AppSettings["clientKey"];
        string WorkspaceId = ConfigurationManager.AppSettings["workspaceId"];

        string strAuthEndpoint = $"https://login.microsoftonline.com/organizationId/";
        string strScopeEndpoint = $"https://analysis.windows.net/powerbi/api/.default";
        string strPowerBIApiUrl = $"https://api.powerbi.com/";

        private string ReportTable = "";
        private string ReportId = "";
        private string ReportFilter = "";
        private string ReportEnableNavPane = "true";
        private string ReportSection = "0";

        private string accessToken = "";
        private string embedUrl = "";
        private string embedReportId = "";

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strAuthEndpoint = strAuthEndpoint.Replace("organizationId", strTenantId);

                if (Request["wid"] != null)
                    WorkspaceId = Request["wid"];

                if (Request["rid"] != null)
                    ReportId = Request["rid"];

                if (Request["sid"] != null)
                    ReportSection = Request["sid"];

                if (Request["tbno"] != null)
                    ReportTable = Request["tbno"];

                if (Request["filter"] != null)
                    ReportFilter = Request["filter"];

                if (Request["enablenavpane"] != null)
                    ReportEnableNavPane = Request["enablenavpane"];

                if (!string.IsNullOrEmpty(ReportId) && !string.IsNullOrEmpty(WorkspaceId))
                {
                    //Save User Menu Log
                    string strMENO = (!string.IsNullOrEmpty(Request["prid"]) ? Request["prid"] : string.Empty);

                    if (CookiesHelper.CONO != null)
                    {
                        //ZLUMDto objZN = new ZLUMDto();
                        //objZN.ZNCONO = CookiesHelper.CONO;
                        //objZN.ZNBRNO = CookiesHelper.BRNO;
                        //objZN.ZNAPNO = CookiesHelper.APNO;
                        //objZN.ZNMENO = strMENO;
                        //objZN.ZNUSNO = CookiesHelper.USNO;

                        //ZLUMDao daoZN = new ZLUMDao();
                        //string strResult = daoZN.Save(objZN);
                    }

                    await EmbedReportAsync();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pbiemb", "PowerBiEmbeded('" + accessToken + "', '" + embedUrl + "', '" + embedReportId + "', '" + Request["t"] + "'," + (ReportEnableNavPane.Equals("true") ? "1" : "0") + ", '" + ReportSection + "', '" + ReportTable + "', '" + ReportFilter + "');", true);
                }

            }
        }

        async Task EmbedReportAsync()
        {
            Guid workspaceId = new Guid(WorkspaceId);
            Guid reportId = new Guid(ReportId);

            try
            {
                using (PowerBIClient pbiClient = await GetPowerBiClient())
                {
                    // Get report info
                    Report pbiReport = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);

                    /*
                    Check if dataset is present for the corresponding report
                    If no dataset is present then it is a RDL report 
                    */
                    bool isRDLReport = String.IsNullOrEmpty(pbiReport.DatasetId);


                    EmbedToken embedToken;

                    if (isRDLReport)
                    {
                        // Get Embed token for RDL Report
                        embedToken = GetEmbedTokenForRDLReport(pbiClient, workspaceId, reportId);
                    }
                    else
                    {
                        // Create list of dataset
                        List<Guid> datasetIds = new List<Guid>()
                        {
                            // Add dataset associated to the report
                            Guid.Parse(pbiReport.DatasetId),
                        };

                        // Append additional dataset to the list to achieve dynamic binding later
                        //if (additionalDatasetId != Guid.Empty)
                        //{
                        //    datasetIds.Add(additionalDatasetId);
                        //}

                        // Get Embed token multiple resources
                        embedToken = GetEmbedToken(pbiClient, reportId, datasetIds, workspaceId);
                    }

                    accessToken = embedToken.Token;
                    embedUrl = pbiReport.EmbedUrl;
                    embedReportId = pbiReport.Id.ToString();
                }

            }
            catch (Exception ex)
            {
                string strMessage = ex.Message;
                throw;
            }
        }

        async Task<PowerBIClient> GetPowerBiClient()
        {
            IConfidentialClientApplication clientApp = ConfidentialClientApplicationBuilder
                                                                                .Create(strApplicationId)
                                                                                .WithClientSecret(strClientSecret)
                                                                                .WithAuthority(strAuthEndpoint)
                                                                                .Build();

            Microsoft.Identity.Client.AuthenticationResult authenticationResult = await clientApp.AcquireTokenForClient(strScopeEndpoint.Split(';')).ExecuteAsync();

            TokenCredentials tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");
            PowerBIClient pbiClient = new PowerBIClient(new Uri(strPowerBIApiUrl), tokenCredentials);

            return pbiClient;
        }

        EmbedToken GetEmbedTokenForRDLReport(PowerBIClient pbiClient, Guid targetWorkspaceId, Guid reportId, string accessLevel = "view")
        {
            // Generate token request for RDL Report
            var generateTokenRequestParameters = new GenerateTokenRequest(
                accessLevel: accessLevel
            );

            // Generate Embed token
            var embedToken = pbiClient.Reports.GenerateTokenInGroup(targetWorkspaceId, reportId, generateTokenRequestParameters);

            return embedToken;
        }

        EmbedToken GetEmbedToken(PowerBIClient pbiClient, Guid reportId, IList<Guid> datasetIds, [Optional] Guid targetWorkspaceId)
        {
            // Create a request for getting Embed token 
            // This method works only with new Power BI V2 workspace experience

            Dataset dataset = pbiClient.Datasets.GetDatasetInGroup(targetWorkspaceId, datasetIds[0].ToString());
            bool IsEffectiveIdentityRequired = dataset.IsEffectiveIdentityRequired.Value;
            bool IsEffectiveIdentityRolesRequired = dataset.IsEffectiveIdentityRolesRequired.Value;

            GenerateTokenRequestV2 tokenRequest = new GenerateTokenRequestV2(

                reports: new List<GenerateTokenRequestV2Report>() { new GenerateTokenRequestV2Report(reportId) },

                datasets: datasetIds.Select(datasetId => new GenerateTokenRequestV2Dataset(datasetId.ToString())).ToList(),

                targetWorkspaces: targetWorkspaceId != Guid.Empty ? new List<GenerateTokenRequestV2TargetWorkspace>() { new GenerateTokenRequestV2TargetWorkspace(targetWorkspaceId) } : null,

                identities: IsEffectiveIdentityRequired ? new List<EffectiveIdentity>() { new EffectiveIdentity(strApplicationObject, datasets: datasetIds.Select(datasetId => datasetId.ToString()).ToList()) } : null

                );



            // Generate Embed token
            EmbedToken embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest);

            return embedToken;
        }

        //async Task<ReportEmbedConfig> GetEmbedParams(Guid workspaceId, IList<Guid> reportIds, [Optional] IList<Guid> additionalDatasetIds)
        //{
        //    // Note: This method is an example and is not consumed in this sample app

        //    using (var pbiClient = await GetPowerBiClient())
        //    {
        //        // Create mapping for reports and Embed URLs
        //        var embedReports = new List<EmbedReport>();

        //        // Create list of datasets
        //        var datasetIds = new List<Guid>();

        //        // Get datasets and Embed URLs for all the reports
        //        foreach (var reportId in reportIds)
        //        {
        //            // Get report info
        //            var pbiReport = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);

        //            // Append to existing list of datasets to achieve dynamic binding later
        //            datasetIds.Add(Guid.Parse(pbiReport.DatasetId));

        //            // Add report data for embedding
        //            embedReports.Add(new EmbedReport { ReportId = pbiReport.Id, ReportName = pbiReport.Name, EmbedUrl = pbiReport.EmbedUrl });
        //        }

        //        // Append to existing list of datasets to achieve dynamic binding later
        //        if (additionalDatasetIds != null)
        //        {
        //            datasetIds.AddRange(additionalDatasetIds);
        //        }

        //        // Get Embed token multiple resources
        //        var embedToken = await GetEmbedToken(reportIds, datasetIds, workspaceId);

        //        // Capture embed params
        //        var embedParams = new ReportEmbedConfig
        //        {
        //            EmbedReports = embedReports,
        //            EmbedToken = embedToken
        //        };

        //        return embedParams;
        //    }
        //}
    }
}