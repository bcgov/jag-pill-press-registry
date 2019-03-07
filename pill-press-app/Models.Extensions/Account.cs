using System;
using Gov.Jag.PillPressRegistry.Interfaces.Models;


namespace Gov.Jag.PillPressRegistry.Public.Models
{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class AccountExtensions
    {

        /// <summary>
        /// Copy values from a ViewModel to a Dynamics Account.
        /// If parameter copyIfNull is false then do not copy a null value. Mainly applies to updates to the account.
        /// updateIfNull defaults to true
        /// </summary>
        /// <param name="toDynamics"></param>
        /// <param name="fromVM"></param>
        /// <param name="copyIfNull"></param>
        public static void CopyValues(this MicrosoftDynamicsCRMaccount toDynamics, ViewModels.Account fromVM)
        {
            bool copyIfNull = true;
            toDynamics.CopyValues(fromVM, copyIfNull);
        }

        /// <summary>
        /// Copy values from a ViewModel to a Dynamics Account.
        /// If parameter copyIfNull is false then do not copy a null value. Mainly applies to updates to the account.
        /// updateIfNull defaults to true
        /// </summary>
        /// <param name="toDynamics"></param>
        /// <param name="fromVM"></param>
        /// <param name="copyIfNull"></param>
        public static void CopyValues(this MicrosoftDynamicsCRMaccount toDynamics, ViewModels.Account fromVM, Boolean copyIfNull)
        {
            if (copyIfNull || fromVM.businessLegalName != null)
            {
                toDynamics.Name = fromVM.businessLegalName;
            }

            if (copyIfNull || fromVM.businessDBAName != null)
            {
                toDynamics.BcgovDoingbusinessasname = fromVM.businessDBAName;
            }

            if (copyIfNull || fromVM.businessNumber != null)
            {
                toDynamics.BcgovBusinessnumber = fromVM.businessNumber;
            }
            
            if (!string.IsNullOrEmpty(fromVM.businessType))
            {
                toDynamics.Businesstypecode = (int)Enum.Parse(typeof(ViewModels.BusinessTypeEnum), fromVM.businessType, true);
            }
            

            if (copyIfNull || fromVM.description != null)
            {
                toDynamics.Description = fromVM.description;
            }

            // copy the exernalId.
            if (copyIfNull || fromVM.externalId != null)
            {
                toDynamics.BcgovBceid = fromVM.externalId;
            }

            if (copyIfNull || fromVM.businessEmail != null)
            {
                toDynamics.Emailaddress1 = fromVM.businessEmail;
            }
            if (copyIfNull || fromVM.consentForEmailCommunication != null)
            {
                toDynamics.BcgovConsentforemailcommunication = fromVM.consentForEmailCommunication;
            }
            if (copyIfNull || fromVM.businessPhoneNumber != null)
            {
                toDynamics.Telephone1 = fromVM.businessPhoneNumber;
            }

            if (copyIfNull || fromVM.physicalAddressName != null)
            {
                toDynamics.Address1Name = fromVM.physicalAddressName;
            }

            if (copyIfNull || fromVM.physicalAddressLine1 != null)
            {
                toDynamics.Address1Line1 = fromVM.physicalAddressLine1;
            }

            if (copyIfNull || fromVM.physicalAddressLine2 != null)
            {
                toDynamics.Address1Line2 = fromVM.physicalAddressLine2;
            }

            if (copyIfNull || fromVM.physicalAddressCity != null)
            {
                toDynamics.Address1City = fromVM.physicalAddressCity;
            }

            if (copyIfNull || fromVM.physicalAddressCountry != null)
            {
                toDynamics.Address1Country = fromVM.physicalAddressCountry;
            }

            if (copyIfNull || fromVM.physicalAddressProvince != null)
            {
                toDynamics.Address1Stateorprovince = fromVM.physicalAddressProvince;
            }

            if (copyIfNull || fromVM.physicalAddressPostalCode != null)
            {
                toDynamics.Address1Postalcode = fromVM.physicalAddressPostalCode;
            }

            if (copyIfNull || fromVM.mailingAddressName != null)
            {
                toDynamics.Address2Name = fromVM.mailingAddressName;
            }

            if (copyIfNull || fromVM.mailingAddressLine1 != null)
            {
                toDynamics.Address2Line1 = fromVM.mailingAddressLine1;
            }

            if (copyIfNull || fromVM.mailingAddressLine2 != null)
            {
                toDynamics.Address2Line2 = fromVM.mailingAddressLine2;
            }

            if (copyIfNull || fromVM.mailingAddressCity != null)
            {
                toDynamics.Address2City = fromVM.mailingAddressCity;
            }

            if (copyIfNull || fromVM.mailingAddressCountry != null)
            {
                toDynamics.Address2Country = fromVM.mailingAddressCountry;
            }

            if (copyIfNull || fromVM.mailingAddressProvince != null)
            {
                toDynamics.Address2Stateorprovince = fromVM.mailingAddressProvince;
            }

            if (copyIfNull || fromVM.mailingAddressPostalCode != null)
            {
                toDynamics.Address2Postalcode = fromVM.mailingAddressPostalCode;
            }

            if (copyIfNull || fromVM.websiteAddress != null)
            {
                toDynamics.Websiteurl = fromVM.websiteAddress;
            }

            if (copyIfNull || fromVM.foippaconsent != null)
            {
                toDynamics.BcgovFoippaconsent = fromVM.foippaconsent;
            }

            if (copyIfNull || fromVM.declarationofcorrectinformation != null)
            {
                toDynamics.BcgovDeclarationofcorrectinformation = fromVM.declarationofcorrectinformation;
            }

            toDynamics.BcgovSubmitteddate = fromVM.SubmittedDate;

            // business type must be set only during creation, not in update (removed from copyValues() )
            //	toDynamics.AdoxioBusinesstype = (int)Enum.Parse(typeof(ViewModels.Adoxio_applicanttypecodes), fromVM.businessType, true);
        }


        /// <summary>
        /// Create a new ViewModel from a Dynamics Account
        /// </summary>
        /// <param name="account"></param>
        public static ViewModels.Account ToViewModel(this MicrosoftDynamicsCRMaccount account)
        {
            ViewModels.Account accountVM = null;
            if (account != null)
            {
                accountVM = new ViewModels.Account()
                {
                    description = account.Description,
                    businessLegalName = account.Name,
                    businessDBAName = account.BcgovDoingbusinessasname,
                    businessNumber = account.BcgovBusinessnumber,
                    businessEmail = account.Emailaddress1,
                    businessPhoneNumber = account.Telephone1,
                    consentForEmailCommunication = account.BcgovConsentforemailcommunication,
                    externalId = account.BcgovBceid,
                    physicalAddressName = account.Address1Name,
                    physicalAddressLine1 = account.Address1Line1,
                    physicalAddressLine2 = account.Address1Line2,
                    physicalAddressCity = account.Address1City,
                    physicalAddressCountry = account.Address1Country,
                    physicalAddressProvince = account.Address1Stateorprovince,
                    physicalAddressPostalCode = account.Address1Postalcode,
                    mailingAddressName = account.Address2Name,
                    mailingAddressLine1 = account.Address2Line1,
                    mailingAddressLine2 = account.Address2Line2,
                    mailingAddressCity = account.Address2City,
                    mailingAddressCountry = account.Address2Country,
                    mailingAddressProvince = account.Address2Stateorprovince,
                    mailingAddressPostalCode = account.Address2Postalcode,
                    websiteAddress = account.Websiteurl,
                    declarationofcorrectinformation = account.BcgovDeclarationofcorrectinformation,
                    foippaconsent = account.BcgovFoippaconsent,
                    AuthorizedOwnerAdministrativeHold = account.BcgovAoadministrativehold,
                    WaiverAdministrativeHold = account.BcgovWaiveradministrativehold,
                    RegisteredSellerAdministrativeHold = account.BcgovRegisteredselleradministrativehold,

                    SubmittedDate = account.BcgovSubmitteddate

                };
                if (account.Accountid != null)
                {
                    accountVM.id = account.Accountid.ToString();
                }

                // logic for businessType.
                if (account.Businesstypecode != null)
                {
                    // get the business type as a string.
                    ViewModels.BusinessTypeEnum bte = (ViewModels.BusinessTypeEnum)account.Businesstypecode;
                    accountVM.businessType = bte.ToString();
                }


                if (account.Primarycontactid != null)
                {
                    accountVM.primaryContact = account.Primarycontactid.ToViewModel();
                }

                if(account.BcgovAdditionalContact != null)
                {
                    accountVM.additionalContact = account.BcgovAdditionalContact.ToViewModel();
                }

                
            }

            return accountVM;
        }


    }
}
