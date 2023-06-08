using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNUnit
{
    public enum ReturnStatus
    {
        Success,
        DepositLessThanZero,
        NegativeAmount,
        NotEnoughBalance,
        AccountDoesNotExist

    }
}
