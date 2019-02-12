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
    public static class CertificateProductExtensions
    {
        /// <summary>
        /// Convert a given voteQuestion to a ViewModel
        /// </summary>        
        public static ViewModels.CertificateApprovedProduct ToViewModel(this MicrosoftDynamicsCRMbcgovCertificateapprovedproduct certificateApprovedProduct)
        {
            ViewModels.CertificateApprovedProduct result = null;
            if (certificateApprovedProduct != null)
            {
                result = new ViewModels.CertificateApprovedProduct()
                {
                    id = certificateApprovedProduct.BcgovCertificateapprovedproductid,
                    name = certificateApprovedProduct.BcgovName
                };
            }
            return result;
        }        

    }
}
