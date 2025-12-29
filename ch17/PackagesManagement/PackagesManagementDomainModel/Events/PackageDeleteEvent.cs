using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Events
{
    public class PackageDeleteEvent : IEventNotification
    {
        public PackageDeleteEvent(int id, long oldVersion)
        {
            PackageId = id;
            OldVersion = oldVersion;
        }
        public int PackageId { get; }
        public long OldVersion { get; }

    }
}
