using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    [Serializable]
    public class IncorrectConfigurationException : Exception
    {
        public IncorrectConfigurationException(): this("Invalid Configuration detected")
        {
            
        }

        public IncorrectConfigurationException(string message)
           : base(message)
        {
        }
    }
}
