using DDD.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDomainServices.DTOs;

namespace WorkerMApplicationServices.Commands
{
    public class PurchaseCommand(PurchaseInfoDTO purchase): ICommand
    {
        public PurchaseInfoDTO Purchase => purchase;
    }
}
