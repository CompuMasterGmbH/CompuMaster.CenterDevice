﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace CenterDevice.Rest.Clients.User
{
    public class BaseUserData : UserData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        [DeserializeAs(Name = RestApiConstants.TECHNICAL_USER)]
        public bool? TechnicalUser { get; set; }
    }
}
