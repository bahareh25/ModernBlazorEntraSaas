using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Application.Common.Enums
{
   
        public enum DeleteLinkStatus
        {
            Success,
            UserNotFound,
            LinkNotFound,
            Unauthorized
        }

}
