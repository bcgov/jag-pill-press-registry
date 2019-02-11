
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class CertificateDynamicsExtensions
    {

        public static MicrosoftDynamicsCRMbcgovCertificate GetCertificateByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            return system.GetCertificateByIdWithChildren(id.ToString());
        }

        public static MicrosoftDynamicsCRMbcgovCertificate GetCertificateByIdWithChildren(this IDynamicsClient system, string id)
        {
            MicrosoftDynamicsCRMbcgovCertificate result;
            try
            {
                List<string> expand = new List<string>()
                {
                    "bcgov_certificate_bcgov_certificateapprovedproduct_CertificateId","bcgov_certificate_bcgov_certificatetermsandconditions_Certificate"
                };
                // fetch from Dynamics.
                result = system.Certificates.GetByKey(bcgovCertificateid: id, expand: expand);                               
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            
            return result;
        }

    }
}
