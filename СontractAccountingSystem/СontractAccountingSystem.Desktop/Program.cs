using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core;

namespace СontractAccountingSystem.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main(params string[] args)
        {
            Salazki.Platform.Cef.Platform.StartApplication(new ContractApp(), "https://localhost:7266/", new CoreModule());
        }
    }
}
