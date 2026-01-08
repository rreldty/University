using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace University.Api.Common
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string strClientID = "81E7C798-695E-4F95-BD0A-E1E951F33ABF";
            string strClientSecret = "FCC672AA-4E7F-4B70-901E-45F5C7DDB3EE";
            //basic ODFFN0M3OTgtNjk1RS00Rjk1LUJEMEEtRTFFOTUxRjMzQUJGOkZDQzY3MkFBLTRFN0YtNEI3MC05MDFFLTQ1RjVDN0REQjNFRQ==

            string clientId = string.Empty;
            string clientSecret = string.Empty;

            // The TryGetBasicCredentials method checks the Authorization header and
            // Return the ClientId and clientSecret
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.SetError("invalid_credential", "credential is not valid");
                return Task.Run(() => context.Rejected());
            }

            if (clientId.Equals(strClientID) && clientSecret.Equals(strClientSecret))
            {
                context.Validated(clientId);
                return Task.Run(() => context.Validated());
            }
            else
            {
                context.SetError("invalid_credential", "credential is not valid");
                return Task.Run(() => context.Rejected());
            }

            //ZAPCDao daoZAPC = new ZAPCDao();
            //ZAPCDto objZAPC = daoZAPC.Get(new ZAPCDto()
            //{
            //    Z1CONO = "",
            //    Z1BRNO = "",
            //    Z1ACID = clientId
            //});

            //if (objZAPC != null)
            //{
            //    if (objZAPC.Z1ACST.Equals(clientSecret))
            //    {
            //        decimal decCHDT = BaseBaseMethod.DateToNumeric(DateTime.Now);
            //        decimal decCHTM = BaseBaseMethod.TimeToNumeric(DateTime.Now);
            //        //Save Log
            //        ZAPLDto objZAPL = new ZAPLDto();
            //        objZAPL.Z2CONO = "";
            //        objZAPL.Z2BRNO = "";
            //        objZAPL.Z2ACID = clientId;
            //        objZAPL.Z2LGDT = decCHDT;
            //        objZAPL.Z2LGTM = decCHTM;
            //        objZAPL.Z2LGIP = context.Request.RemoteIpAddress;
            //        objZAPL.Z2REMA = "";
            //        objZAPL.Z2SYST = BaseBaseMethod.SystFinish;
            //        objZAPL.Z2STAT = BaseBaseMethod.StatDraft;
            //        objZAPL.Z2RCST = BaseBaseMethod.RecordStatusActive;
            //        objZAPL.Z2CRDT = decCHDT;
            //        objZAPL.Z2CRTM = decCHTM;
            //        objZAPL.Z2CRUS = "API";
            //        objZAPL.Z2CHDT = decCHDT;
            //        objZAPL.Z2CHTM = decCHTM;
            //        objZAPL.Z2CHUS = "API";

            //        ZAPLDao daoZAPL = new ZAPLDao();
            //        string strMsg = daoZAPL.Insert(objZAPL);

            //        context.OwinContext.Set("oauth:client", objZAPC);
            //        context.Validated(clientId);
            //        return Task.Run(() => context.Validated());
            //    }
            //    else
            //    {
            //        context.SetError("invalid_credential", "credential is not valid");
            //        return Task.Run(() => context.Rejected());
            //    }
            //}
            //else
            //{
            //    context.SetError("invalid_credential", "credential is not valid");
            //    return Task.Run(() => context.Rejected());
            //}

        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Validate user name and password
            //if (context.UserName.ToLower().Equals("aglis") && context.Password.ToLower().Equals("aglis"))
            //{
            //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //    //identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
   
            //    return Task.Run(() => context.Validated(identity));
            //}
            //else
            //{
            //    context.SetError("invalid_credential", "credential is not valid");
            //    return Task.Run(() => context.Rejected());
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            return Task.Run(() => context.Validated(identity));

        }
    }
}