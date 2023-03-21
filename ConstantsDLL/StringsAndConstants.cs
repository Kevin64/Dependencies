using ConstantsDLL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ConstantsDLL
{
    public static class StringsAndConstants
    {
        /**
         * Common code
         * Start
        */

        public const int LOG_INFO = 0;
        public const int LOG_WARNING = 1;
        public const int LOG_ERROR = 2;
        public const int LOG_MISC = 3;
        public const int RETURN_SUCCESS = 0;
        public const int RETURN_WARNING = 1;
        public const int RETURN_ERROR = 2;

        public const string limitedUserType = "Limitado";
        public const string supplyLoginData = "forneceDadosUsuario.php";
        public const string INTRANET_REQUIRED = "É necessário conexão com a intranet.";
        public const string ERROR_WINDOWTITLE = "Erro";
        public const string NO_AUTH = "Preencha suas credenciais.";
        public const string unknown = "Desconhecido";
        public const string pcNotRegistered = "Patrimônio não registrado inicialmente pelo software 'Coleta de hardware e Cadastro de Patrimônio'. Primeiramente realize o cadastro e depois tente novamente.";
        public const string ALREADY_RUNNING = "Já existe uma instância do programa em execução!";
        public const string LOG_INFO_ATTR = "<INFORMAÇÃO>";
        public const string LOG_WARNING_ATTR = "<AVISO>";
        public const string LOG_ERROR_ATTR = "<ERRO>";
        public const string LOG_ARGS_ERROR = ARGS_ERROR;
        public const string LOG_PC_NOT_REGISTERED = pcNotRegistered;

        public static readonly string loginPath = System.IO.Path.GetTempPath() + Resources.fileLogin;
        public static readonly string configPath = System.IO.Path.GetTempPath() + Resources.fileConfig;

        /**
         * Common code
         * End
        */

        /**
         * Code exclusive for HardwareInformation application
         * Start
        */
        public const bool consoleOutCLI = true;
        public const bool consoleOutGUI = false;

        public const int TIMER_INTERVAL = 1000;
        public const int MAX_SIZE = 100;

        public const string YES = "Sim";
        public const string UTF8_NO = "Não";
        public const string ANSI_NO = "Nao";
        public const string HOSTNAME_PATTERN = "PC-";

        //LoadingCircle for 350%
        public const int rotatingCircleNumberSpoke350 = 60;
        public const int rotatingCircleSpokeThickness350 = 5;
        public const int rotatingCircleInnerCircleRadius350 = 24;
        public const int rotatingCircleOuterCircleRadius350 = 35;

        //LoadingCircle for 300%
        public const int rotatingCircleNumberSpoke300 = 56;
        public const int rotatingCircleSpokeThickness300 = 5;
        public const int rotatingCircleInnerCircleRadius300 = 22;
        public const int rotatingCircleOuterCircleRadius300 = 32;

        //LoadingCircle for 250%
        public const int rotatingCircleNumberSpoke250 = 52;
        public const int rotatingCircleSpokeThickness250 = 5;
        public const int rotatingCircleInnerCircleRadius250 = 17;
        public const int rotatingCircleOuterCircleRadius250 = 24;

        //LoadingCircle for 225%
        public const int rotatingCircleNumberSpoke225 = 48;
        public const int rotatingCircleSpokeThickness225 = 5;
        public const int rotatingCircleInnerCircleRadius225 = 17;
        public const int rotatingCircleOuterCircleRadius225 = 24;

        //LoadingCircle for 200%
        public const int rotatingCircleNumberSpoke200 = 44;
        public const int rotatingCircleSpokeThickness200 = 5;
        public const int rotatingCircleInnerCircleRadius200 = 15;
        public const int rotatingCircleOuterCircleRadius200 = 22;

        //LoadingCircle for 175%
        public const int rotatingCircleNumberSpoke175 = 40;
        public const int rotatingCircleSpokeThickness175 = 5;
        public const int rotatingCircleInnerCircleRadius175 = 14;
        public const int rotatingCircleOuterCircleRadius175 = 18;

        //LoadingCircle for 150%
        public const int rotatingCircleNumberSpoke150 = 36;
        public const int rotatingCircleSpokeThickness150 = 5;
        public const int rotatingCircleInnerCircleRadius150 = 11;
        public const int rotatingCircleOuterCircleRadius150 = 15;

        //LoadingCircle for 125%
        public const int rotatingCircleNumberSpoke125 = 32;
        public const int rotatingCircleSpokeThickness125 = 5;
        public const int rotatingCircleInnerCircleRadius125 = 10;
        public const int rotatingCircleOuterCircleRadius125 = 12;

        //LoadingCircle for 100%
        public const int rotatingCircleNumberSpoke100 = 28;
        public const int rotatingCircleSpokeThickness100 = 4;
        public const int rotatingCircleInnerCircleRadius100 = 8;
        public const int rotatingCircleOuterCircleRadius100 = 9;

        //LoadingCircle Color and Speed
        public static readonly Color rotatingCircleColor = SystemColors.MenuHighlight;
        public const int rotatingCircleRotationSpeed = 1;

        //Path for icons and images
        public const string login_banner_light_path = "Resources\\header\\login_banner_light.png";
        public const string login_banner_dark_path = "Resources\\header\\login_banner_dark.png";
        public const string main_banner_light_path = "Resources\\header\\main_banner_light.png";
        public const string main_banner_dark_path = "Resources\\header\\main_banner_dark.png";
        public const string icon_user_light_path = "Resources\\icons\\icon_user_light.png";
        public const string icon_user_dark_path = "Resources\\icons\\icon_user_dark.png";
        public const string icon_port_light_path = "Resources\\icons\\icon_port_light.png";
        public const string icon_port_dark_path = "Resources\\icons\\icon_port_dark.png";
        public const string icon_password_light_path = "Resources\\icons\\icon_password_light.png";
        public const string icon_password_dark_path = "Resources\\icons\\icon_password_dark.png";
        public const string icon_log_light_path = "Resources\\icons\\icon_log_light.png";
        public const string icon_log_dark_path = "Resources\\icons\\icon_log_dark.png";
        public const string icon_about_light_path = "Resources\\icons\\icon_about_light.png";
        public const string icon_about_dark_path = "Resources\\icons\\icon_about_dark.png";
        public const string icon_autotheme_light_path = "Resources\\icons\\icon_autotheme_light.png";
        public const string icon_autotheme_dark_path = "Resources\\icons\\icon_autotheme_dark.png";
        public const string icon_lighttheme_light_path = "Resources\\icons\\icon_lighttheme_light.png";
        public const string icon_lighttheme_dark_path = "Resources\\icons\\icon_lighttheme_dark.png";
        public const string icon_darktheme_light_path = "Resources\\icons\\icon_darktheme_light.png";
        public const string icon_darktheme_dark_path = "Resources\\icons\\icon_darktheme_dark.png";
        public const string icon_brand_light_path = "Resources\\icons\\icon_brand_light.png";
        public const string icon_brand_dark_path = "Resources\\icons\\icon_brand_dark.png";
        public const string icon_model_light_path = "Resources\\icons\\icon_model_light.png";
        public const string icon_model_dark_path = "Resources\\icons\\icon_model_dark.png";
        public const string icon_serial_no_light_path = "Resources\\icons\\icon_serial_no_light.png";
        public const string icon_serial_no_dark_path = "Resources\\icons\\icon_serial_no_dark.png";
        public const string icon_cpu_light_path = "Resources\\icons\\icon_cpu_light.png";
        public const string icon_cpu_dark_path = "Resources\\icons\\icon_cpu_dark.png";
        public const string icon_ram_light_path = "Resources\\icons\\icon_ram_light.png";
        public const string icon_ram_dark_path = "Resources\\icons\\icon_ram_dark.png";
        public const string icon_disk_size_light_path = "Resources\\icons\\icon_disk_size_light.png";
        public const string icon_disk_size_dark_path = "Resources\\icons\\icon_disk_size_dark.png";
        public const string icon_hdd_light_path = "Resources\\icons\\icon_hdd_light.png";
        public const string icon_hdd_dark_path = "Resources\\icons\\icon_hdd_dark.png";
        public const string icon_ahci_light_path = "Resources\\icons\\icon_ahci_light.png";
        public const string icon_ahci_dark_path = "Resources\\icons\\icon_ahci_dark.png";
        public const string icon_gpu_light_path = "Resources\\icons\\icon_gpu_light.png";
        public const string icon_gpu_dark_path = "Resources\\icons\\icon_gpu_dark.png";
        public const string icon_windows_light_path = "Resources\\icons\\icon_windows_light.png";
        public const string icon_windows_dark_path = "Resources\\icons\\icon_windows_dark.png";
        public const string icon_hostname_light_path = "Resources\\icons\\icon_hostname_light.png";
        public const string icon_hostname_dark_path = "Resources\\icons\\icon_hostname_dark.png";
        public const string icon_mac_light_path = "Resources\\icons\\icon_mac_light.png";
        public const string icon_mac_dark_path = "Resources\\icons\\icon_mac_dark.png";
        public const string icon_ip_light_path = "Resources\\icons\\icon_ip_light.png";
        public const string icon_ip_dark_path = "Resources\\icons\\icon_ip_dark.png";
        public const string icon_bios_light_path = "Resources\\icons\\icon_bios_light.png";
        public const string icon_bios_dark_path = "Resources\\icons\\icon_bios_dark.png";
        public const string icon_bios_version_light_path = "Resources\\icons\\icon_bios_version_light.png";
        public const string icon_bios_version_dark_path = "Resources\\icons\\icon_bios_version_dark.png";
        public const string icon_secure_boot_light_path = "Resources\\icons\\icon_secure_boot_light.png";
        public const string icon_secure_boot_dark_path = "Resources\\icons\\icon_secure_boot_dark.png";
        public const string icon_patr_light_path = "Resources\\icons\\icon_patr_light.png";
        public const string icon_patr_dark_path = "Resources\\icons\\icon_patr_dark.png";
        public const string icon_seal_light_path = "Resources\\icons\\icon_seal_light.png";
        public const string icon_seal_dark_path = "Resources\\icons\\icon_seal_dark.png";
        public const string icon_room_light_path = "Resources\\icons\\icon_room_light.png";
        public const string icon_room_dark_path = "Resources\\icons\\icon_room_dark.png";
        public const string icon_building_light_path = "Resources\\icons\\icon_building_light.png";
        public const string icon_building_dark_path = "Resources\\icons\\icon_building_dark.png";
        public const string icon_server_light_path = "Resources\\icons\\icon_server_light.png";
        public const string icon_server_dark_path = "Resources\\icons\\icon_server_dark.png";
        public const string icon_standard_light_path = "Resources\\icons\\icon_standard_light.png";
        public const string icon_standard_dark_path = "Resources\\icons\\icon_standard_dark.png";
        public const string icon_service_light_path = "Resources\\icons\\icon_service_light.png";
        public const string icon_service_dark_path = "Resources\\icons\\icon_service_dark.png";
        public const string icon_letter_light_path = "Resources\\icons\\icon_letter_light.png";
        public const string icon_letter_dark_path = "Resources\\icons\\icon_letter_dark.png";
        public const string icon_in_use_light_path = "Resources\\icons\\icon_in_use_light.png";
        public const string icon_in_use_dark_path = "Resources\\icons\\icon_in_use_dark.png";
        public const string icon_sticker_light_path = "Resources\\icons\\icon_sticker_light.png";
        public const string icon_sticker_dark_path = "Resources\\icons\\icon_sticker_dark.png";
        public const string icon_type_light_path = "Resources\\icons\\icon_type_light.png";
        public const string icon_type_dark_path = "Resources\\icons\\icon_type_dark.png";
        public const string icon_VT_x_light_path = "Resources\\icons\\icon_VT_x_light.png";
        public const string icon_VT_x_dark_path = "Resources\\icons\\icon_VT_x_dark.png";
        public const string icon_who_light_path = "Resources\\icons\\icon_who_light.png";
        public const string icon_who_dark_path = "Resources\\icons\\icon_who_dark.png";
        public const string icon_smart_light_path = "Resources\\icons\\icon_smart_light.png";
        public const string icon_smart_dark_path = "Resources\\icons\\icon_smart_dark.png";
        public const string icon_tpm_light_path = "Resources\\icons\\icon_tpm_light.png";
        public const string icon_tpm_dark_path = "Resources\\icons\\icon_tpm_dark.png";
        public const string icon_cmos_battery_light_path = "Resources\\icons\\icon_cmos_battery_light.png";
        public const string icon_cmos_battery_dark_path = "Resources\\icons\\icon_cmos_battery_dark.png";
        public const string icon_ticket_light_path = "Resources\\icons\\icon_ticket_light.png";
        public const string icon_ticket_dark_path = "Resources\\icons\\icon_ticket_dark.png";

        //public const string cliHelpTextServer = "Servidor do sistema de patrimônio (Ex.: 192.168.76.103, localhost, etc. Modificar arquivo INI para configuração estática. Primeiro IP da lista será o escolhido caso o parâmetro esteja ausente) - Opcional";
        //public const string cliHelpTextPort = "Porta do sistema de patrimônio (Ex.: 8081, 80, etc. Modificar arquivo INI para configuração estática. Primeira porta da lista será a escolhida caso o parâmetro esteja ausente) - Opcional";
        //public const string cliHelpTextMode = "Tipo de serviço realizado (Valores possíveis: M/m para manutenção (padrão), F/f para formatação) - Opcional";
        //public const string cliHelpTextPatrimony = "Patrimônio do equipamento (Ex.: 123456). Caso o parâmetro esteja ausente, será coletado pelo hostname PC-123456 (padrão) - Opcional";
        //public const string cliHelpTextSeal = "Lacre do equipamento (se houver) (Ex.: 12345678, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextRoom = "Sala onde o equipamento estará localizado (Ex.: 1234, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextBuilding = "Prédio onde o equipamento estará localizado (Valores possíveis: 21, 67, 74A, 74B, 74C, 74D, AR, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextActiveDirectory = "Cadastrado no Active Directory (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextStandard = "Padrão da imagem implantado no equipamento (Valores possíveis: A/a para aluno, F/f para funcionário, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextDate = "Data do serviço realizado (Valores possíveis: hoje (padrão), ou especificar data, ex.: 12/12/2020) - Opcional";
        //public const string cliHelpTextBattery = "Realizada troca de pilha? (Valores possíveis: S/s (Sim), N/n (Não)) - OBRIGATÓRIO";
        //public const string cliHelpTextTicket = "Número do chamado aberto (Ex.: 123456) - OBRIGATÓRIO";
        //public const string cliHelpTextInUse = "Equipamento em uso? (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextTag = "Equipamento possui etiqueta? (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextType = "Categoria do equipamento (Valores possíveis: Desktop, Notebook, Tablet, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        //public const string cliHelpTextUser = "Usuário de login - OBRIGATÓRIO";
        //public const string cliHelpTextPassword = "Senha de login - OBRIGATÓRIO";
        //public const string today = "hoje";
        //public const string sameWord = "mesmo";

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
        public const string today = "today";
        public const string sameWord = "same";

        //public const string LOG_NO_INTRANET = INTRANET_REQUIRED;
        //public const string LOG_LOGIN_INCOMPLETE = NO_AUTH;
        //public const string LOG_PENDENCY_ERROR = "Pendências detectadas";
        //public const string LOG_HOSTNAME_ERROR = HOSTNAME_ALERT;
        //public const string LOG_MEDIAOP_ERROR = MEDIA_OPERATION_ALERT;
        //public const string LOG_SECBOOT_ERROR = SECURE_BOOT_ALERT;
        //public const string LOG_OFFLINE_ERROR = DATABASE_REACH_ERROR;
        //public const string LOG_BIOSVER_ERROR = BIOS_VERSION_ALERT;
        //public const string LOG_FIRMWARE_ERROR = FIRMWARE_TYPE_ALERT;
        //public const string LOG_NETWORK_ERROR = NETWORK_ERROR;
        //public const string LOG_VT_ERROR = VT_ALERT;
        //public const string LOG_SMART_ERROR = SMART_FAIL;
        //public const string LOG_TPM_ERROR = TPM_ERROR;
        //public const string LOG_MEMORYFEW_ERROR = NOT_ENOUGH_MEMORY;
        //public const string LOG_MEMORYMUCH_ERROR = TOO_MUCH_MEMORY;
        //public const string LOG_SERVER_UNREACHABLE = SERVER_NOT_FOUND_ERROR;
        //public const string LOG_MANDATORY_FIELD_ERROR = MANDATORY_FIELD;
        //public const string LOG_INCORRECT_REGISTER_DATE = INCORRECT_REGISTER_DATE;
        //public const string LOG_INCORRECT_FUTURE_REGISTER_DATE = INCORRECT_FUTURE_REGISTER_DATE;
        //public const string LOG_ALREADY_REGISTERED_TODAY = ALREADY_REGISTERED_TODAY;
        //public static readonly string LOG_PC_DROPPED = Properties.Strings.PC_DROPPED;

        public static string pcPath = System.IO.Path.GetTempPath() + Resources.filePC;
        public static string biosPath = System.IO.Path.GetTempPath() + Resources.fileBios;
        public static string webview2filePath = System.IO.Path.GetTempPath() + Resources.webview2file;

        public static readonly List<string> listModeCLI = new List<string>() { "F", "f", "M", "m" };
        public static readonly List<string> listModeGUI = new List<string>() { Strings.listModeGUIFormat, Strings.listModeGUIMaintenance };
        public static readonly List<string> listActiveDirectoryCLI = new List<string>() { "Y", "y", "N", "n" };
        public static readonly List<string> listActiveDirectoryGUI = new List<string>() { Strings.listActiveDirectoryGUIYes, Strings.listActiveDirectoryGUINo };
        public static readonly List<string> listStandardCLI = new List<string>() { "F", "f", "S", "s" };
        public static readonly List<string> listStandardGUI = new List<string>() { Strings.listStandardGUIEmployee, Strings.listStandardGUIStudent };
        public static readonly List<string> listInUseCLI = new List<string>() { "Y", "y", "N", "n" };
        public static readonly List<string> listInUseGUI = new List<string>() { Strings.listInUseGUIYes, Strings.listInUseGUINo };
        public static readonly List<string> listTagCLI = new List<string>() { "Y", "y", "N", "n" };
        public static readonly List<string> listTagGUI = new List<string>() { Strings.listTagGUIYes, Strings.listTagGUIYes };
        public static readonly List<string> listBatteryCLI = new List<string>() { "Y", "y", "N", "n" };
        public static readonly List<string> listBatteryGUI = new List<string>() { Strings.listBatteryGUIYes, Strings.listBatteryGUINo };
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
        public const string LOG_SERVER_NOT_FOUND = SERVER_NOT_FOUND_ERROR;
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
