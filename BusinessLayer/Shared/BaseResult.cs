using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Shared
{
    public class BaseResult
    {
        public bool IsSuccess { get; set; }

        /// <summary>
        /// success or error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// record id if any
        /// </summary>
        public long Id { get; set; }
    }
}
