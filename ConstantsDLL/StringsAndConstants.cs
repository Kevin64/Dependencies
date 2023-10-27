using ConstantsDLL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConstantsDLL
{
    public enum ExitCodes
    {
        SUCCESS,
        WARNING,
        ERROR
    }

    /// <summary> 
    /// Class that keeps some of the program constants
    /// </summary>
    public static class StringsAndConstants
    {
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Code exclusive for AssetInformationAndRegistration application - START
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        public const string CLI_SERVER_IP_SWITCH = "serverIP";
        public const string CLI_SERVER_PORT_SWITCH = "serverPort";
        public const string CLI_SERVICE_TYPE_SWITCH = "serviceType";
        public const string CLI_ASSET_NUMBER_SWITCH = "assetNumber";
        public const string CLI_SEAL_NUMBER_SWITCH = "sealNumber";
        public const string CLI_ROOM_NUMBER_SWITCH = "roomNumber";
        public const string CLI_BUILDING_SWITCH = "building";
        public const string CLI_AD_REGISTERED_SWITCH = "adRegistered";
        public const string CLI_STANDARD_SWITCH = "standard";
        public const string CLI_SERVICE_DATE_SWITCH = "serviceDate";
        public const string CLI_BATTERY_CHANGE_SWITCH = "batteryChanged";
        public const string CLI_TICKET_NUMBER_SWITCH = "ticketNumber";
        public const string CLI_IN_USE_SWITCH = "inUse";
        public const string CLI_TAG_SWITCH = "tag";
        public const string CLI_HW_TYPE_SWITCH = "hwType";
        public const string CLI_USERNAME_SWITCH = "username";
        public const string CLI_PASSWORD_SWITCH = "password";
        public const string CLI_HELP_SWITCH = "help";
        public const string CLI_DEFAULT_UNCHANGED = "same";
        public const string CLI_DEFAULT_SERVICE_TYPE = "m";
        public const string CLI_DEFAULT_SERVICE_DATE = "today";
        public const string CLI_EMPLOYEE_TYPE_0 = "e";
        public const string CLI_EMPLOYEE_TYPE_1 = "s";
        public const string CLI_SERVICE_TYPE_0 = "f";
        public const string CLI_SERVICE_TYPE_1 = "m";
        public const string LIST_YES_ABBREV = "y";
        public const string LIST_NO_ABBREV = "n";
        public const string MANDATORY = "MANDATORY";
        public const string OPTIONAL = "Optional";

        //CLI switch text
        public const string CLI_HELP_TEXT_SERVER_IP = "APCS server IP address (Ex.: 192.168.1.100, 10.0.0.10, localhost, etc. Modify INI file for fixed configuration. The first IP in the file will be chosen if the parameter is absent) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_SERVER_PORT = "APCS server port (Ex.: 80, 8080, etc. Modify INI file for fixed configuration. The first port in the file will be chosen if the parameter is absent) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_SERVICE_TYPE = "Type of service performed (Possible values: \'" + CLI_SERVICE_TYPE_1 + "\' for maintenance (default), \'" + CLI_SERVICE_TYPE_0 + "\' for formatting) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_ASSET_NUMBER = "Equipment's asset number (Ex.: 123456). If the parameter is absent, it will be collected by hostname PC-123456 (default) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_SEAL_NUMBER = "Equipament's seal number (if exists) (Ex.: 12345678, or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_ROOM_NUMBER = "Room where the equipment will be located (Ex.: 1234, or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_BUILDING = "Building where the equipment will be located (Possible values: see APCS for a list of building names, or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_AD_REGISTERED = "Registered in Active Directory (Possible values: \'" + LIST_YES_ABBREV + "\' (Yes), \'" + LIST_NO_ABBREV + "\' (No), or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_STANDARD = "Type of image deployed (Possible values: \'" + CLI_EMPLOYEE_TYPE_1 + "\' for student, \'" + CLI_EMPLOYEE_TYPE_0 + "\' for employee, or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_SERVICE_DATE = "Date of service performed (Possible values: \'" + CLI_DEFAULT_SERVICE_DATE + "\' (default), or specify a date in the 'yyyy-mm-dd' format, ex.: 2020-12-25) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_BATTERY_CHANGE = "CMOS Battery replaced? (Possible values: \'" + LIST_YES_ABBREV + "\' (Yes), \'" + LIST_NO_ABBREV + "\' (No)) - " + MANDATORY;
        public const string CLI_HELP_TEXT_TICKET_NUMBER = "Ticket number (Ex.: 123456) - " + MANDATORY;
        public const string CLI_HELP_TEXT_IN_USE = "Equipment in use? (Possible values: \'" + LIST_YES_ABBREV + "\' (Yes), \'" + LIST_NO_ABBREV + "\' (No), or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_TAG = "Does the equipment have a label? (Possible value: \'" + LIST_YES_ABBREV + "\' (Yes), \'" + LIST_NO_ABBREV + "\' (No), or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_HW_TYPE = "Equipment category (Possible values: see APCS for a list of categories, or \'" + CLI_DEFAULT_UNCHANGED + "\' (default) to keep unchanged) - " + OPTIONAL;
        public const string CLI_HELP_TEXT_USERNAME = "Login username - " + MANDATORY;
        public const string CLI_HELP_TEXT_PASSWORD = "Login password - " + MANDATORY;

        //Parameters list
        public static readonly List<string> LIST_MODE_CLI = new List<string>() { CLI_SERVICE_TYPE_0, CLI_SERVICE_TYPE_1 };
        public static readonly List<string> LIST_MODE_GUI = new List<string>() { UIStrings.LIST_MODE_GUI_FORMAT, UIStrings.LIST_MODE_GUI_MAINTENANCE };
        public static readonly List<string> LIST_ACTIVE_DIRECTORY_CLI = new List<string>() { LIST_YES_ABBREV, LIST_NO_ABBREV };
        public static readonly List<string> LIST_ACTIVE_DIRECTORY_GUI = new List<string>() { UIStrings.LIST_YES_0, UIStrings.LIST_NO_0 };
        public static readonly List<string> LIST_STANDARD_CLI = new List<string>() { CLI_EMPLOYEE_TYPE_0, CLI_EMPLOYEE_TYPE_1 };
        public static readonly List<string> LIST_STANDARD_GUI = new List<string>() { UIStrings.LIST_STANDARD_GUI_EMPLOYEE, UIStrings.LIST_STANDARD_GUI_STUDENT };
        public static readonly List<string> LIST_IN_USE_CLI = new List<string>() { LIST_YES_ABBREV, LIST_NO_ABBREV };
        public static readonly List<string> LIST_IN_USE_GUI = new List<string>() { UIStrings.LIST_YES_0, UIStrings.LIST_NO_0 };
        public static readonly List<string> LIST_TAG_CLI = new List<string>() { LIST_YES_ABBREV, LIST_NO_ABBREV };
        public static readonly List<string> LIST_TAG_GUI = new List<string>() { UIStrings.LIST_YES_0, UIStrings.LIST_NO_0 };
        public static readonly List<string> LIST_BATTERY_CLI = new List<string>() { LIST_YES_ABBREV, LIST_NO_ABBREV };
        public static readonly List<string> LIST_BATTERY_GUI = new List<string>() { UIStrings.LIST_YES_0, UIStrings.LIST_NO_0 };
        public static readonly List<string> LIST_STATES = new List<string>() { UIStrings.NOT_SUPPORTED, UIStrings.DEACTIVATED, UIStrings.ACTIVATED };
        public static readonly List<string> LIST_THEME_GUI = new List<string>() { "Auto", "Light", "Dark" };

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
        public static readonly Color DARK_SUBTLE_LIGHTCOLOR = Color.Silver;
        public static readonly Color LIGHT_SUBTLE_DARKCOLOR = Color.DarkSlateGray;
        public static readonly Color BLUE_FOREGROUND = SystemColors.Highlight;
        public static readonly Color INACTIVE_SYSTEM_BUTTON_COLOR = Color.FromArgb(204, 204, 204);
        public static readonly Color HIGHLIGHT_LABEL_COLOR = Color.FromArgb(127, 127, 127);
        public static readonly Color PRESSED_STRIP_BUTTON = Color.FromArgb(128, 128, 128);
        public static readonly Color ROTATING_CIRCLE_COLOR = SystemColors.MenuHighlight;
        public static readonly Color ROTATING_CIRCLE_BACKCOLOR = Color.Transparent;
        public static readonly Color LIGHT_INACTIVE_CAPTION_COLOR = SystemColors.InactiveCaption;
        public static readonly Color DARK_INACTIVE_CAPTION_COLOR = SystemColors.WindowFrame;

        public const ConsoleColor MISC_CONSOLE_COLOR = ConsoleColor.DarkCyan;

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Code exclusive for AssetInformationAndRegistration application - END
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Code exclusive for FeaturesOverlayPresentation application - START
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        public const double FADE_TIME = 0.2d;

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        // Code exclusive for FeaturesOverlayPresentation application - END
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
    }
}
