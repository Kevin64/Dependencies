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

        public const int Width = 1366;
        public const int Height = 768;

        public static readonly List<string> fopFileList = new List<string>() { "BCrypt.Net-Next.dll", "ConstantsDLL.dll", "INIFileParser.dll", "JsonFileReaderDLL.dll", "LogGeneratorDLL.dll", "Microsoft.Xaml.Behaviors.dll", "Newtonsoft.Json.dll", "TutorialDeUsoDaEstaçãoDeTrabalho.exe", "TutorialDeUsoDaEstaçãoDeTrabalho.exe.config" };
        public const string DidItRunAlready = "DidItRunAlready";
        public const string FOP = "FOP";
        public const string FopRegKey = @"Software\FOP";
        public const string win7imgDir = "\\img-windows7\\";
        public const string win10imgDir = "\\img-windows10\\";
        public const string win11imgDir = "\\img-windows11\\";
        public const string FopRunOnceKey = @"Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string FOPx86 = "C:\\Program Files (x86)\\FOP\\Rever tutorial de uso do computador.lnk";
        public const string FOPx64 = "C:\\Program Files\\FOP\\Rever tutorial de uso do computador.lnk";
        public const string imgExt = ".png";
        public static string resolutionWarning = "Resolução insuficiente. Este programa suporta apenas resoluções iguais ou superiores a " + Width + "x" + Height + ".";
        public const string finaleScreen = "Finalização";
        public const string finishText = "Finalizar";
        public const string introScreen = "Introdução";
        public const string nextText = "Próximo";
        public const string waitText = "Aguarde ";
        public const string cancelExecution = "Apagar dados de entrega e cancelar\n       execução no próximo boot";
        public const string cancelExecutionResError = "Apagar dados de entrega";
        public const string doExecution = "Enviar dados de entrega e \nexecutar no próximo boot";
        public const string doExecutionResError = "Enviar dados de entrega";
        public const string fillForm = "Preencha os campos necessários!";
        public const string win7ntMajor = "6";
        public const string win7ntMinor = "1";
        public const string win7ntBuild = "7601";
        public const string win7ntFullVersion = "6.1.7601";
        public const string win10ntMajor = "10";
        public const string win10ntMinor = "0";
        public const string win10ntBuild = "19041";
        public const string win10ntFullVersion = "10.0.19041";
        public const string win11ntMajor = "10";
        public const string win11ntMinor = "0";
        public const string win11ntBuild = "22000";
        public const string win11ntFullVersion = "10.0.22000";
        public const string LOG_FILENAME_FOP = "FOP";
        public const double FADE_TIME = 0.2d;
        public const string LOG_IMG_NOTFOUND = "Diretório de imagens não encontrado";
        public const string LOG_IMG_FOUND = "Diretório de imagens encontrado";
        public const string LOG_RUNNING = "Executando janela principal";
        public const string LOG_CLOSING = "Fechando programa";
        public const string LOG_SCHEDULING = "Criando entrada de inicialização";
        public const string LOG_NOTSCHEDULING = "Excluindo registro no banco de dados";
        public const string LOG_EMPLOYEEAWAY = "Funcionário ausente";
        public const string LOG_EMPLOYEEPRESENT = "Funcionário presente";
        public const string LOG_REGISTERING_DELIVERY = "Registrando entrega";
        public const string LOG_FILLFORM = fillForm;
        public const string LOG_SERVER_NOT_FOUND = "";
        public const string LOG_ADDING_REG = "Adicionando chaves de registro";
        public const string LOG_NOT_ADDING_REG = "Não adicionar chaves de registro";
        public const string LOG_REMOVING_REG = "Desfazendo chaves de registro";
        public const string LOG_RESOLUTION_PASSED = "Resolução adequada para exibição";
        public const string LOG_RESOLUTION_FAILED = "Resolução abaixo do requisito";
        public const string LOG_RESOLUTION_ERROR = "Erro de resolução";
        public const string LOG_DISABLE_BOOT = "Não ativar inicialização em boot";
        public const string LOG_DETECTING_OS = "Detectando sistema operacional";
        public const string LOG_ENUM_FILES = "Reunindo arquivos de imagem";
        public const string LOG_SERVICE_TYPE = "Tipo de serviço";
        public const string LOG_FORMAT_SERVICE = "Formatação";
        public const string LOG_MAINTENANCE_SERVICE = "Manutenção";
        public const string LOG_PATR_NUM = "Número de patrimônio";

        public const string INI_SECTION_1_16 = "Logo1URL";
        public const string INI_SECTION_1_17 = "Logo2URL";
        public const string INI_SECTION_1_18 = "Logo3URL";

        /**
         * Code exclusive for FeaturesOverlayPresentation application
         * End
        */

        /**
         * Code exclusive for OfflineDriverInstallerOOBE application
         * Start
        */

        public const string resChangeFailed = "Falha ao alterar a resolução";
        public const string resChangeSuccess = "Êxito em alterar a resolução";
        public const string resChangeReboot = "Necessário reinicialização para alterar a resolução";
        public const string PARAMETER_ERROR = "Um ou mais parâmetros do arquivo INI estão mal formados";
        public const string KEY_FINISH = "Pressione Enter para fechar...";
        public const string HW_MODEL = "Modelo do hardware";
        public const string OS_VERSION = "Versão do sistema";
        public const string OS_ARCH = "Arquitetura do sistema";
        public const string FIRMWARE_TYPE = "Tipo de firmware";
        public const string ADDING_INSTALLING = "Adicionando e instalando drivers";
        public const string ADDING = "Apenas adicionando drivers";
        public const string INSTALL_FINISHED = "Instalação finalizada";
        public const string INI_SECTION_1_1 = "InstallDrivers";
        public const string INI_SECTION_1_2 = "CleanGarbage";
        public const string INI_SECTION_1_3 = "ChangeResolution";
        public const string INI_SECTION_1_4 = "ResolutionWidth";
        public const string INI_SECTION_1_5 = "ResolutionHeight";
        public const string INI_SECTION_1_6 = "DriverPath";
        public const string INI_SECTION_1_7 = "RebootAfterFinished";
        public const string INI_SECTION_1_8 = "PauseAfterFinished";
        public const string INI_SECTION_1_10 = "AddDrivers";
        public const string SHUTDOWN_CMD_1 = "shutdown";
        public const string SHUTDOWN_CMD_2 = "/r /f /t 0";
        public const string CHANGING_RESOLUTION = "Alterando resolução";
        public const string CHANGING_RESOLUTION_SUCCESSFUL = "Resolução alterada com sucesso";
        public const string CHECKING_RESOLUTION = "Verificando resolução atual";
        public const string CHECKING_AVAILABLE_RESOLUTIONS = "Verificando resoluções disponíveis";
        public const string FAILED_CHANGING_RESOLUTION = "Falha ao alterar a resolução";
        public const string REBOOT_CHANGING_RESOLUTION = "Reinicialização necessária para alterar a resolução";
        public const string ERASING_GARBAGE = "Apagando drivers não utilizados";
        public const string ERASING_SUCCESSFUL = "Drivers não utilizados apagados com êxito";
        public const string NOT_ERASING_GARBAGE = "Ignorando exclusão de drivers não utilizados";
        public const string NOT_INSTALLING_DRIVERS = "Ignorando a adição e instalação de drivers";
        public const string NOT_CHANGING_RESOLUTION = "Ignorando alteração da resolução";
        public const string NOT_REBOOTING = "Ignorando reinicialização";
        public const string CHANGING_RESOLUTION_UNNECESSARY = "Sem necessidade de alterar resolução";
        public const string LOG_FILENAME_OOBE = "OfflineDriverInstallerOOBE";
        public const string LOG_DEFFILE_NOT_FOUND = "Arquivo de definição não encontrado";
        public const string LOG_APPFILE_NOT_FOUND = "Um ou mais arquivos de dependência ausentes";
        public const string LOG_APPFILE_FOUND = "Arquivos de dependência encontrados";
        public const string LOG_DEFFILE_FOUND = "Arquivo de definições encontrado";
        public const string DEF_FOUND = "Definição encontrada";
        public const string DRIVER_INSTALL_FAILED = "Falha na instalação dos drivers";
        public const string DIRECTORY_DO_NOT_EXIST = "Repositório de drivers não encontrado";
        public const string UNSUPPORTED_OS = "Sistema não suportado";

        /**
         * Code exclusive for OfflineDriverInstallerOOBE application
         * End
        */
    }
}
