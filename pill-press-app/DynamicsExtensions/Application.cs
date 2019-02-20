
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class ApplicationDynamicsExtensions
    {

        public static string GetSharePointFolderName(this MicrosoftDynamicsCRMincident application)
        {

            string applicationIdCleaned = application.Incidentid.ToString().ToUpper().Replace("-", "");
            string folderName = $"{application.Title}_{applicationIdCleaned}";
            return folderName;
        }

        public static MicrosoftDynamicsCRMincident GetApplicationById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMincident result;
            try
            {
                // fetch from Dynamics.
                result = system.Incidents.GetByKey(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }


        public static MicrosoftDynamicsCRMincident GetApplicationByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            return system.GetApplicationByIdWithChildren(id.ToString());
        }


        public static MicrosoftDynamicsCRMincident GetApplicationByIdWithChildren(this IDynamicsClient system, string id)
        {
            MicrosoftDynamicsCRMincident result;
            try
            {
                List<string> expand = new List<string>()
                {
                    "bcgov_ApplicationTypeId","bcgov_incident_customproduct_RelatedApplication","customerid_account","bcgov_incident_businesscontact",
                    "bcgov_BCSellersAddress","bcgov_OutsideBCSellersAddress","bcgov_ImportersAddress","bcgov_OriginatingSellersAddress",
                    "bcgov_AddressofBusinessthathasGivenorLoaned","bcgov_AddressofBusinessthathasRentedorLeased","bcgov_EquipmentLocation", "bcgov_AddressofPersonBusiness",
                    "bcgov_incident_bcgov_certificate_Application", "bcgov_EquipmentRecord", "bcgov_AddressWhereEquipmentWasDestroyed", "bcgov_CivicAddressofPurchaser",
                    "bcgov_PurchasersCivicAddress", "bcgov_PurchasersBusinessAddress"
                };
                // fetch from Dynamics.
                result = system.Incidents.GetByKey(incidentid: id, expand: expand);               
                // expand only goes one level deep - we need the "location address"

                if (result.BcgovEquipmentLocation != null && result.BcgovEquipmentLocation._bcgovLocationaddressValue != null)
                {
                    result.BcgovEquipmentLocation.BcgovLocationAddress = system.GetCustomAddressById(result.BcgovEquipmentLocation._bcgovLocationaddressValue);
                }

                // get the full certificate data.  The initial request only has one level deep of data.

                if (result.BcgovIncidentBcgovCertificateApplication != null)
                {
                    foreach (var certificate in result.BcgovIncidentBcgovCertificateApplication)
                    {
                        // copy over child entities
                        var temp = system.GetCertificateByIdWithChildren(certificate.BcgovCertificateid);
                        if (temp != null)
                        {
                            certificate.BcgovCertificateBcgovCertificateapprovedproductCertificateId
                            = temp.BcgovCertificateBcgovCertificateapprovedproductCertificateId;
                            certificate.BcgovCertificateBcgovCertificatetermsandconditionsCertificate
                                = temp.BcgovCertificateBcgovCertificatetermsandconditionsCertificate;
                        }                        
                    }
                }

               
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            
            if (result != null && result.BcgovIncidentBusinesscontact != null)
            {
                for (int i = 0; i < result.BcgovIncidentBusinesscontact.Count; i++)
                {
                    try
                    {
                        result.BcgovIncidentBusinesscontact[i] = system.GetBusinessContactById(result.BcgovIncidentBusinesscontact[i].BcgovBusinesscontactid);
                    }
                    catch (OdataerrorException)
                    {
                        // ignore the exception.
                    }
                }
            }


            return result;
        }

    }
}
