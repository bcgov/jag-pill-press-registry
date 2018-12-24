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
    public static class ContactExtensions
    {
        public static bool HasValue (this ViewModels.Contact contact)
        {
            bool result = contact != null &&
                !(string.IsNullOrEmpty(contact.email) &&
                 string.IsNullOrEmpty(contact.firstName) &&
                 string.IsNullOrEmpty(contact.lastName) &&
                 string.IsNullOrEmpty(contact.id) &&
                 string.IsNullOrEmpty(contact.phoneNumber) &&
                 string.IsNullOrEmpty(contact.phoneNumberAlt) &&
                 string.IsNullOrEmpty(contact.title));
            return result;
        }
        /// <summary>
        /// Convert a given voteQuestion to a ViewModel
        /// </summary>        
        public static ViewModels.Contact ToViewModel(this MicrosoftDynamicsCRMcontact contact)
        {
            ViewModels.Contact result = null;
            if (contact != null)
            {
                result = new ViewModels.Contact();
                if (contact.Contactid != null)
                {
                    result.id = contact.Contactid;
                }
                result.title = contact.Jobtitle;
                result.email = contact.Emailaddress1;
                result.firstName = contact.Firstname;
                result.lastName = contact.Lastname;
                result.phoneNumber = contact.Telephone1;
                result.phoneNumberAlt = contact.Telephone2;
            }
            return result;
        }

        public static void CopyHeaderValues(this MicrosoftDynamicsCRMcontact to, IHttpContextAccessor httpContextAccessor)
        {
            var headers = httpContextAccessor.HttpContext.Request.Headers;
            string smgov_useremail = headers["SMGOV_USEREMAIL"];
            string smgov_birthdate = headers["SMGOV_BIRTHDATE"];
            string smgov_sex = headers["SMGOV_SEX"];
            string smgov_streetaddress = headers["SMGOV_STREETADDRESS"];
            string smgov_city = headers["SMGOV_CITY"];
            string smgov_postalcode = headers["SMGOV_POSTALCODE"];
            string smgov_stateorprovince = headers["SMGOV_STATEORPROVINCE"];
            string smgov_country = headers["SMGOV_COUNTRY"];
            string smgov_givenname = headers["SMGOV_GIVENNAME"];
            string smgov_givennames = headers["SMGOV_GIVENNAMES"];
            string smgov_surname = headers["SMGOV_SURNAME"];



            if (!string.IsNullOrEmpty(smgov_useremail))
            {
                to.Emailaddress1 = smgov_useremail;
            }
            if (!string.IsNullOrEmpty(smgov_givenname))
            {
                to.Firstname = smgov_givenname;
            }
            if (!string.IsNullOrEmpty(smgov_givennames))
            {
                to.Middlename = smgov_givennames;
            }
            if (!string.IsNullOrEmpty(smgov_surname))
            {
                to.Lastname = smgov_surname;
            }
            if (!string.IsNullOrEmpty(smgov_streetaddress))
            {
                to.Address1Line1 = smgov_streetaddress;
            }
            if (!string.IsNullOrEmpty(smgov_postalcode))
            {
                to.Address1Postalcode = smgov_postalcode;
            }
            if (!string.IsNullOrEmpty(smgov_city))
            {
                to.Address1City = smgov_city;
            }
            if (!string.IsNullOrEmpty(smgov_stateorprovince))
            {
                to.Address1Stateorprovince = smgov_stateorprovince;
            }
            if (!string.IsNullOrEmpty(smgov_country))
            {
                to.Address1Country = smgov_country;
            }
        }


        /// <summary>
        /// Return a Dynamics gender code for the given string.
        /// </summary>
        /// <param name="genderCode"></param>
        /// <returns>
        /// 1 - M
        /// 2 - F
        /// 3 - Other
        /// </returns>
        static int? GetIntGenderCode(string genderCode)
        {
            // possible values:

            int? result = null;

            if (!string.IsNullOrEmpty(genderCode))
            {
                string upper = genderCode.ToUpper();
                if (upper.Equals("MALE") || upper.Equals("M"))
                {
                    result = 1;
                }
                else if (upper.Equals("FEMALE") || upper.Equals("F"))
                {
                    result = 2;
                }
                else
                {
                    result = 3;
                }
            }

            return result;
        }

        public static void CopyHeaderValues(this ViewModels.Worker to, IHeaderDictionary headers)
        {
            string smgov_useremail = headers["smgov_useremail"];
            // the following fields appear to just have a guid in them, not a driver's licence.
            string smgov_useridentifier = headers["smgov_useridentifier"];
            string smgov_useridentifiertype = headers["smgov_useridentifiertype"];

            // birthdate is YYYY-MM-DD
            string smgov_birthdate = headers["smgov_birthdate"];
            // Male / Female / Unknown. 
            string smgov_sex = headers["smgov_sex"];
            string smgov_givenname = headers["smgov_givenname"];
            string smgov_givennames = headers["smgov_givennames"];
            string smgov_surname = headers["smgov_surname"];

            if (!string.IsNullOrEmpty(smgov_givenname))
            {
                to.firstname = smgov_givenname;
            }

            if (!string.IsNullOrEmpty(smgov_givennames))
            {
                to.middlename = smgov_givennames.Replace(smgov_givenname, "").Trim();
            }

            if (!string.IsNullOrEmpty(smgov_surname))
            {
                to.lastname = smgov_surname;
            }
            if (!string.IsNullOrEmpty(smgov_useremail))
            {
                to.email = smgov_useremail;
            }


            if (!string.IsNullOrEmpty(smgov_birthdate) && DateTimeOffset.TryParse(smgov_birthdate, out DateTimeOffset tempDate))
            {
                to.dateofbirth = tempDate;
            }
            if (!string.IsNullOrEmpty(smgov_sex))
            {
                to.gender = (Gender)GetIntGenderCode(smgov_sex);
            }
        }



        public static void CopyHeaderValues(this ViewModels.Contact to, IHeaderDictionary headers)
        {
            string smgov_useremail = headers["smgov_useremail"];
            string smgov_birthdate = headers["smgov_birthdate"];
            string smgov_sex = headers["smgov_sex"];
            string smgov_streetaddress = headers["smgov_streetaddress"];
            string smgov_city = headers["smgov_city"];
            string smgov_postalcode = headers["smgov_postalcode"];
            string smgov_stateorprovince = headers["smgov_province"];
            string smgov_country = headers["smgov_country"];
            string smgov_givenname = headers["smgov_givenname"];
            string smgov_givennames = headers["smgov_givennames"];
            string smgov_surname = headers["smgov_surname"];

            // TODO add address support.
            /*
            to.address1_line1 = smgov_streetaddress;
            to.address1_postalcode = smgov_postalcode;
            to.address1_city = smgov_city;
            to.address1_stateorprovince = smgov_stateorprovince;
            to.address1_country = smgov_country;
            */

            if (!string.IsNullOrEmpty(smgov_givenname))
            {
                to.firstName = smgov_givenname;
            }

            if (!string.IsNullOrEmpty(smgov_surname))
            {
                to.lastName = smgov_surname;
            }
            if (!string.IsNullOrEmpty(smgov_useremail))
            {
                to.email = smgov_useremail;
            }

        }



        public static void CopyValues(this MicrosoftDynamicsCRMcontact to, ViewModels.Contact from)
        {
            
            to.Emailaddress1 = from.email;
            to.Firstname = from.firstName;

            to.Lastname = from.lastName;
           
            to.Telephone1 = from.phoneNumber;
        }

        public static MicrosoftDynamicsCRMcontact ToModel(this ViewModels.Contact contact)
        {
            MicrosoftDynamicsCRMcontact result = null;
            if (contact != null)
            {
                result = new MicrosoftDynamicsCRMcontact();
                if (!string.IsNullOrEmpty(contact.id))
                {
                    result.Contactid = contact.id;
                }
                
                result.Emailaddress1 = contact.email;
                result.Firstname = contact.firstName;
                result.Lastname = contact.lastName;
                result.Telephone1 = contact.phoneNumber;
                result.Telephone2 = contact.phoneNumberAlt;
                result.Jobtitle = contact.title;                

                if (string.IsNullOrEmpty(result.Fullname) && (!string.IsNullOrEmpty(result.Firstname) || !string.IsNullOrEmpty(result.Lastname)))
                {
                    result.Fullname = "";
                    if (!string.IsNullOrEmpty(result.Firstname))
                    {
                        result.Fullname += result.Firstname;
                    }
                    if (!string.IsNullOrEmpty(result.Lastname))
                    {
                        if (!string.IsNullOrEmpty(result.Fullname))
                        {
                            result.Fullname += " ";
                        }
                        result.Fullname += result.Lastname;
                    }
                }
            }
            return result;
        }
    }
}
