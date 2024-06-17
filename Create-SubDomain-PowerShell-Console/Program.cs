using System.Diagnostics;

class Program
{
    static void Main()
    {
        // تنظیمات مربوط به Host جدید
        string zoneName = "zoneName.ir";  // نام منطقه (Zone)
        string hostname = "examplessss";  // نام Host
        string ipAddress = "Public Server Ip";  // IP آدرس

        // اجرای دستور PowerShell برای ایجاد رکورد A در DNS به عنوان Administrator
        string script = $"Add-DnsServerResourceRecordA -ZoneName {zoneName} -Name {hostname} -IPv4Address {ipAddress}";
        RunPowerShellScriptAsAdministrator(script);

        Console.WriteLine("رکورد با موفقیت ایجاد شد.");
        Console.ReadKey();
    }

    static void RunPowerShellScriptAsAdministrator(string script)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Verb = "runas",  // اجرا به عنوان Administrator
            Arguments = $"-NoProfile -ExecutionPolicy unrestricted -Command \"{script}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = processStartInfo })
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            // نمایش خروجی یا خطا
            if (!string.IsNullOrEmpty(output))
            {
                Console.WriteLine(output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine($"Error: {error}");
            }
        }
    }
}
