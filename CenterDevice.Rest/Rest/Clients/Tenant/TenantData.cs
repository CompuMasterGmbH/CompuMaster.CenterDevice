﻿using CenterDevice.Model.Tenant;

namespace CenterDevice.Rest.Clients.Tenant
{
    public class TenantData : TenantBaseData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public bool Expired { get; set; }

        public ExtendedUserData User { get; set; }

        public bool IsSyncable()
        {
            return !Expired && !User.IsGuest() && !User.IsBlocked() && !User.IsDeleted();
        }
    }
}
