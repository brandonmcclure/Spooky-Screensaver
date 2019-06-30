using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    public class screenSaverConfig : IConfigurationSection
    {
        string IConfiguration.this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        string IConfigurationSection.Key => throw new NotImplementedException();

        string IConfigurationSection.Path => throw new NotImplementedException();

        string IConfigurationSection.Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
        {
            throw new NotImplementedException();
        }

        IChangeToken IConfiguration.GetReloadToken()
        {
            throw new NotImplementedException();
        }

        IConfigurationSection IConfiguration.GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
