﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{
    public class Contact
    {
        public string id { get; set; }

        public string name { get; set; }

        public string firstName { get; set; }

        public string middlename { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string address1_line1 { get; set; }

        public string address1_city { get; set; }

        public string address1_country { get; set; }

        public string address1_stateorprovince { get; set; }

        public string address1_postalcode { get; set; }

        public string address2_line1 { get; set; }

        public string address2_city { get; set; }

        public string address2_country { get; set; }

        public string address2_stateorprovince { get; set; }

        public string address2_postalcode { get; set; }

        public Boolean? adoxio_cansignpermanentchangeapplications { get; set; }

        public Boolean? adoxio_canattendeducationsessions { get; set; }

        public Boolean? adoxio_cansigntemporarychangeapplications { get; set; }

        public Boolean? adoxio_canattendcompliancemeetings { get; set; }

        public Boolean? adoxio_canobtainlicenceinfofrombranch { get; set; }

        public Boolean? adoxio_canrepresentlicenseeathearings { get; set; }

        public Boolean? adoxio_cansigngrocerystoreproofofsalesrevenue { get; set; }


    }
}
