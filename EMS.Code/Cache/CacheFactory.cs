/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Code
{
    public class CacheFactory
    {
        public static ICache Cache()
        {
            return new Cache();
        }
    }
}
