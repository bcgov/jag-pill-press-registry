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
        public static void CopyValues(this MicrosoftDynamicsCRMaccount toDynamics, ViewModels.Account fromVM, Boolean copyIfNull)
        {
            if (copyIfNull || (!copyIfNull && fromVM.businessLegalName != null))
            {
                toDynamics.Name = fromVM.businessLegalName;
            }

            if (copyIfNull || (!copyIfNull && fromVM.doingBusinessAs != null))
            {
                toDynamics.BcgovDoingbusinessasname = fromVM.doingBusinessAs;
            }

            if (copyIfNull || (!copyIfNull && fromVM.businessNumber != null))
            {
                toDynamics.Accountnumber = fromVM.businessNumber;
            }

            if (!string.IsNullOrEmpty(fromVM.businessType))
            {
                toDynamics.Businesstypecode = (int)Enum.Parse(typeof(ViewModels.BusinessTypeEnum), fromVM.businessType, true);
            }

            if (copyIfNull || (!copyIfNull && fromVM.description != null))
            {
                toDynamics.Description = fromVM.description;
            }

            // copy the exernalId.

            if (copyIfNull || (!copyIfNull && fromVM.externalId != null))
            {
                toDynamics.BcgovBceid = fromVM.externalId;
            }



            if (copyIfNull || (!copyIfNull && fromVM.businessEmail != null))
            {
                toDynamics.Emailaddress1 = fromVM.businessEmail;
            }
            if (copyIfNull || (!copyIfNull && fromVM.businessPhone != null))
            {
                toDynamics.Telephone1 = fromVM.businessPhone;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddressName != null))
            {
                toDynamics.Address1Name = fromVM.mailingAddressName;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddressStreet != null))
            {
                toDynamics.Address1Line1 = fromVM.mailingAddressStreet;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddressCity != null))
            {
                toDynamics.Address1City = fromVM.mailingAddressCity;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddressCountry != null))
            {
                toDynamics.Address1County = fromVM.mailingAddressCountry;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddressCountry != null))
            {
                toDynamics.Address1Stateorprovince = fromVM.mailingAddressProvince;
            }
            if (copyIfNull || (!copyIfNull && fromVM.mailingAddresPostalCode != null))
            {
                toDynamics.Address1Postalcode = fromVM.mailingAddresPostalCode;
            }

            // business type must be set only during creation, not in update (removed from copyValues() )
            //	toDynamics.AdoxioBusinesstype = (int)Enum.Parse(typeof(ViewModels.Adoxio_applicanttypecodes), fromVM.businessType, true);
        }

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
                    clientId = account.Accountnumber,
                    businessLegalName = account.Name,
                    doingBusinessAs = account.BcgovDoingbusinessasname,
                    businessNumber = account.BcgovBusinessnumber,
                    businessEmail = account.Emailaddress1,
                    businessPhone = account.Telephone1,
                    mailingAddressName = account.Address1Name,
                    mailingAddressStreet = account.Address1Line1,
                    mailingAddressCity = account.Address1City,
                    mailingAddressCountry = account.Address1County,
                    mailingAddressProvince = account.Address1Stateorprovince,
                    mailingAddresPostalCode = account.Address1Postalcode

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

                if (account.BcgovCurrentBusinessPhysicalAddress != null)
                {
                    accountVM.physicalAddress = account.BcgovCurrentBusinessPhysicalAddress.ToViewModel();
                }

                if (account.BcgovCurrentBusinessMailingAddress != null)
                {
                    accountVM.mailingAddress = account.BcgovCurrentBusinessMailingAddress.ToViewModel();
                }
            }

            return accountVM;
        }


    }
}
