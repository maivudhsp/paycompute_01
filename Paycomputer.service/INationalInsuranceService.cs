using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paycomputer.service
{
    public interface INationalInsuranceService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
