﻿using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Areas
{
    public interface IAreaDomainService:IDomainService
    {
        Area Get(int id);
    }
}
