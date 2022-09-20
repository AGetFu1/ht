using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application
{
    public static class Setting
    {
        public static Uri Node
        {
            get
            {
                return new Uri("http://192.168.99.157:9200");
            }
        }
        //连接配置 
        public static ConnectionSettings ConnectionSettings
        {
            get
            {
                return new ConnectionSettings(Node).DefaultIndex("exception");
            }
        }

    }
}
