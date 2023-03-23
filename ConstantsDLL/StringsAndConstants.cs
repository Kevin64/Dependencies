using ConstantsDLL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConstantsDLL
{
    public static class StringsAndConstants
    {
        /**
         * Code exclusive for HardwareInformation application
         * Start
        */

        //CLI switch text
        public const string cliHelpTextServer = "Asset system server IP address (Ex.: 192.168.76.103, localhost, etc. Modify INI file for fixed configuration. The first IP in the list will be chosen if the parameter is absent) - Optional";
        public const string cliHelpTextPort = "Asset system server port (Ex.: 8081, 80, etc. Modify INI file for fixed configuration. The first port in the list will be chosen if the parameter is absent) - Optional";
        public const string cliHelpTextMode = "Type of service performed (Possible values: M/m for maintenance (default), F/f for formatting) - Optional";
        public const string cliHelpTextPatrimony = "Equipment's asset number (Ex.: 123456). If the parameter is absent, it will be collected by hostname PC-123456 (default) - Optional";
        public const string cliHelpTextSeal = "Equipament's seal number (if exists) (Ex.: 12345678, or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextRoom = "Room where the equipment will be located (Ex.: 1234, or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextBuilding = "Building where the equipment will be located (Possible values: see asset system for a list of building names, or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextActiveDirectory = "Registered in Active Directory (Possible values: Y/y (Yes), N/n (No), or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextStandard = "Type of image deployed (Possible values: S/s for student, E/e for employee, or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextDate = "Date of service performed (Possible values: today (default), or specify a date, ex.: 12/12/2020) - Optional";
        public const string cliHelpTextBattery = "CMOS Battery replaced? (Possible values: Y/y (Yes), N/n (No)) - MANDATORY";
        public const string cliHelpTextTicket = "Ticket number (Ex.: 123456) - MANDATORY";
        public const string cliHelpTextInUse = "Equipment in use? (Possible values: Y/y (Yes), N/n (Não), or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextTag = "Does the equipment have a label? (Possible value: Y/y (Yes), N/n (No), or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextType = "Equipment category (Possible values: see asset system for a list of categories, or 'same' (default) to keep unchanged) - Optional";
        public const string cliHelpTextUser = "Login username - MANDATORY";
        public const string cliHelpTextPassword = "Login password - MANDATORY";

        //Parameters list
        public static readonly List<string> listModeCLI = new List<string>() { "F", "f", "M", "m" };
        public static readonly List<string> listModeGUI = new List<string>() { Strings.listModeGUIFormat, Strings.listModeGUIMaintenance };
        public static readonly List<string> listActiveDirectoryCLI = new List<string>() { Strings.listYes1, Strings.listYes2, Strings.listNo1, Strings.listNo2 };
        public static readonly List<string> listActiveDirectoryGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listStandardCLI = new List<string>() { "F", "f", "S", "s" };
        public static readonly List<string> listStandardGUI = new List<string>() { Strings.listStandardGUIEmployee, Strings.listStandardGUIStudent };
        public static readonly List<string> listInUseCLI = new List<string>() { Strings.listYes1, Strings.listYes2, Strings.listNo1, Strings.listNo2 };
        public static readonly List<string> listInUseGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listTagCLI = new List<string>() { Strings.listYes1, Strings.listYes2, Strings.listNo1, Strings.listNo2 };
        public static readonly List<string> listTagGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listBatteryCLI = new List<string>() { Strings.listYes1, Strings.listYes2, Strings.listNo1, Strings.listNo2 };
        public static readonly List<string> listBatteryGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listThemeGUI = new List<string>() { "Auto", "Light", "Dark" };

        //UI colors
        public static readonly Color LIGHT_FORECOLOR = SystemColors.ControlText;
        public static readonly Color LIGHT_BACKCOLOR = SystemColors.Window;
        public static readonly Color LIGHT_ASTERISKCOLOR = Color.Red;
        public static readonly Color LIGHT_AGENT = Color.DarkCyan;
        public static readonly Color ALERT_COLOR = Color.Red;
        public static readonly Color OFFLINE_ALERT = Color.Red;
        public static readonly Color ONLINE_ALERT = Color.Lime;
        public static readonly Color DARK_FORECOLOR = SystemColors.ControlLightLight;
        public static readonly Color DARK_BACKCOLOR = Color.FromArgb(64, 64, 64);
        public static readonly Color DARK_ASTERISKCOLOR = Color.FromArgb(255, 200, 0);
        public static readonly Color DARK_AGENT = Color.DarkCyan;
        public static readonly Color LIGHT_DROPDOWN_BORDER = Color.FromArgb(122, 122, 122);
        public static readonly Color LIGHT_BACKGROUND = Color.FromArgb(248, 248, 248);
        public static readonly Color DARK_BACKGROUND = Color.FromArgb(32, 32, 32);
        public static readonly Color DARK_SUBTLE_LIGHTLIGHTCOLOR = Color.DimGray;
        public static readonly Color LIGHT_SUBTLE_DARKDARKCOLOR = Color.FromArgb(192, 192, 192);
        public static readonly Color DARK_SUBTLE_LIGHTCOLOR = Color.DarkSlateGray;
        public static readonly Color LIGHT_SUBTLE_DARKCOLOR = Color.Silver;
        public static readonly Color BLUE_FOREGROUND = SystemColors.Highlight;
        public static readonly Color INACTIVE_SYSTEM_BUTTON_COLOR = Color.FromArgb(204, 204, 204);
        public static readonly Color HIGHLIGHT_LABEL_COLOR = Color.FromArgb(127, 127, 127);
        public static readonly Color PRESSED_STRIP_BUTTON = Color.FromArgb(128, 128, 128);
        public static readonly Color rotatingCircleColor = SystemColors.MenuHighlight;

        public const ConsoleColor MISC_CONSOLE_COLOR = ConsoleColor.DarkCyan;

        /**
         * Code exclusive for HardwareInformation application
         * End
        */

        /**
         * Code exclusive for FeaturesOverlayPresentation application
         * Start
        */

        public static readonly List<string> fopFileList = new List<string>() { "BCrypt.Net-Next.dll", "ConstantsDLL.dll", "INIFileParser.dll", "JsonFileReaderDLL.dll", "LogGeneratorDLL.dll", "Microsoft.Xaml.Behaviors.dll", "Newtonsoft.Json.dll", "FeaturesOverlayPresentation.exe", "FeaturesOverlayPresentation.exe.config" };
        public const double FADE_TIME = 0.2d;

        /**
         * Code exclusive for FeaturesOverlayPresentation application
         * End
        */
    }
}
