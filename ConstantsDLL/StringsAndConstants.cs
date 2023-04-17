using ConstantsDLL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConstantsDLL
{
    public static class StringsAndConstants
    {
        /**
         * Common code
         * Start
        */

        public static readonly string modelFilePath = System.IO.Path.GetTempPath() + Resources.fileBios;
        public static readonly string configFilePath = System.IO.Path.GetTempPath() + Resources.fileConfig;
        public static readonly string credentialsFilePath = System.IO.Path.GetTempPath() + Resources.fileLogin;
        public static readonly string assetFilePath = System.IO.Path.GetTempPath() + Resources.filePC;
        public static readonly string webView2filePath = System.IO.Path.GetTempPath() + Resources.webview2file;

        /**
         * Common code
         * End
        */

        /**
         * Code exclusive for AssetInformationAndRegistration application
         * Start
        */

        public const string cliServerIPSwitch = "serverIP";
        public const string cliServerPortSwitch = "serverPort";
        public const string cliServiceTypeSwitch = "serviceType";
        public const string cliAssetNumberSwitch = "assetNumber";
        public const string cliSealNumberSwitch = "sealNumber";
        public const string cliRoomNumberSwitch = "roomNumber";
        public const string cliBuildingSwitch = "building";
        public const string cliAdRegisteredSwitch = "adRegistered";
        public const string cliStandardSwitch = "standard";
        public const string cliServiceDateSwitch = "serviceDate";
        public const string cliBatteryChangeSwitch = "batteryChanged";
        public const string cliTicketNumberSwitch = "ticketNumber";
        public const string cliInUseSwitch = "inUse";
        public const string cliTagSwitch = "tag";
        public const string cliHwTypeSwitch = "hwType";
        public const string cliUsernameSwitch = "username";
        public const string cliPasswordSwitch = "password";
        public const string cliHelpSwitch = "help";

        public const string cliDefaultUnchanged = "same";
        public const string cliDefaultServiceType = "m";
        public const string cliDefaultServiceDate = "today";

        public const string cliEmployeeType0 = "e";
        public const string cliEmployeeType1 = "s";

        public const string cliServiceType0 = "f";
        public const string cliServiceType1 = "m";

        public const string listYesAbbrev = "y";
        public const string listNoAbbrev = "n";

        public const string mandatory = "MANDATORY";
        public const string optional = "Optional";

        //CLI switch text
        public const string cliHelpTextServerIP = "Asset system server IP address (Ex.: 192.168.1.100, 10.0.0.10, localhost, etc. Modify INI file for fixed configuration. The first IP in the file will be chosen if the parameter is absent) - " + optional;
        public const string cliHelpTextServerPort = "Asset system server port (Ex.: 80, 8080, etc. Modify INI file for fixed configuration. The first port in the file will be chosen if the parameter is absent) - " + optional;
        public const string cliHelpTextServiceType = "Type of service performed (Possible values: \'" + cliServiceType1 + "\' for maintenance (default), \'" + cliServiceType0 + "\' for formatting) - " + optional;
        public const string cliHelpTextAssetNumber = "Equipment's asset number (Ex.: 123456). If the parameter is absent, it will be collected by hostname PC-123456 (default) - " + optional;
        public const string cliHelpTextSealNumber = "Equipament's seal number (if exists) (Ex.: 12345678, or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextRoomNumber = "Room where the equipment will be located (Ex.: 1234, or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextBuilding = "Building where the equipment will be located (Possible values: see asset system for a list of building names, or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextAdRegistered = "Registered in Active Directory (Possible values: \'" + listYesAbbrev + "\' (Yes), \'" + listNoAbbrev + "\' (No), or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextStandard = "Type of image deployed (Possible values: \'" + cliEmployeeType1 + "\' for student, \'" + cliEmployeeType0 + "\' for employee, or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextServiceDate = "Date of service performed (Possible values: \'" + cliDefaultServiceDate + "\' (default), or specify a date in the 'yyyy-mm-dd' format, ex.: 2020-12-25) - " + optional;
        public const string cliHelpTextBatteryChange = "CMOS Battery replaced? (Possible values: \'" + listYesAbbrev + "\' (Yes), \'" + listNoAbbrev + "\' (No)) - " + mandatory;
        public const string cliHelpTextTicketNumber = "Ticket number (Ex.: 123456) - " + mandatory;
        public const string cliHelpTextInUse = "Equipment in use? (Possible values: \'" + listYesAbbrev + "\' (Yes), \'" + listNoAbbrev + "\' (No), or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextTag = "Does the equipment have a label? (Possible value: \'" + listYesAbbrev + "\' (Yes), \'" + listNoAbbrev + "\' (No), or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextHwType = "Equipment category (Possible values: see asset system for a list of categories, or \'" + cliDefaultUnchanged + "\' (default) to keep unchanged) - " + optional;
        public const string cliHelpTextUsername = "Login username - " + mandatory;
        public const string cliHelpTextPassword = "Login password - " + mandatory;

        //Parameters list
        public static readonly List<string> listModeCLI = new List<string>() { cliServiceType0, cliServiceType1 };
        public static readonly List<string> listModeGUI = new List<string>() { Strings.listModeGUIFormat, Strings.listModeGUIMaintenance };
        public static readonly List<string> listActiveDirectoryCLI = new List<string>() { listYesAbbrev, listNoAbbrev };
        public static readonly List<string> listActiveDirectoryGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listStandardCLI = new List<string>() { cliEmployeeType0, cliEmployeeType1 };
        public static readonly List<string> listStandardGUI = new List<string>() { Strings.listStandardGUIEmployee, Strings.listStandardGUIStudent };
        public static readonly List<string> listInUseCLI = new List<string>() { listYesAbbrev, listNoAbbrev };
        public static readonly List<string> listInUseGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listTagCLI = new List<string>() { listYesAbbrev, listNoAbbrev };
        public static readonly List<string> listTagGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listBatteryCLI = new List<string>() { listYesAbbrev, listNoAbbrev };
        public static readonly List<string> listBatteryGUI = new List<string>() { Strings.listYes0, Strings.listNo0 };
        public static readonly List<string> listStates = new List<string>() { Strings.notSupported, Strings.deactivated, Strings.activated };
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
         * Code exclusive for AssetInformationAndRegistration application
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
