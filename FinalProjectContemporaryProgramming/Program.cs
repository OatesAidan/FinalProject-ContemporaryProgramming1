using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectContemporaryProgramming
{
    //must be mdf file input opening/startup
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            MessageBox.Show("Select .mdf File");
            if (Prompt())
                CreateHostBuilder(args).Build().Run();
        }
        private static bool Prompt()
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "mdf files (*.mdf)|*.mdf|All files (*.*)|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(ofd.FileName).ToLower() == ".mdf")
                {
                    DBContext.MdfFileLocation = ofd.FileName;
                    return true;
                }
                else
                {
                    if (MessageBox.Show("File Must Be An .mdf File", "Invalid File Type", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.Retry)
                        return Prompt();
                    return false;
                }
            }
            else
                return false;
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
