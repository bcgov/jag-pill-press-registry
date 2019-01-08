using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.PillPressRegistry.Public.Models
{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class CertificateExtensions
    {
        /// <summary>
        /// Convert a given Certificate to a ViewModel
        /// </summary>        
        public static ViewModels.Certificate ToViewModel(this MicrosoftDynamicsCRMbcgovCertificate certificate)
        {
            ViewModels.Certificate result = null;
            if (certificate != null)
            {
                result = new ViewModels.Certificate()
                {
                    Id = certificate.BcgovCertificateid,
                    Name = certificate.BcgovName,
                    ExpiryDateString = certificate.BcgovExpirydatelongdatestring,
                    ExpiryDate = certificate.BcgovExpirydate,
                    IssueDate = certificate.BcgovIssueddate,
                    IssueDateString = certificate.BcgovIssueddatelongdatestring,
                    ApprovedIntendedUse = certificate.BcgovApprovedintendeduse
                };                                
            }
            return result;
        }

        
    }
}
