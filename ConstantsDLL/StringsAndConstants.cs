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

        public const int LOG_INFO = 0;
        public const int LOG_WARNING = 1;
        public const int LOG_ERROR = 2;
        public const int LOG_MISC = 3;
        public const int RETURN_SUCCESS = 0;
        public const int RETURN_WARNING = 1;
        public const int RETURN_ERROR = 2;

        public const string limitedUserType = "limit";
        public const string defFile = "definitions.ini";
        public const string fileLogin = "login.json";
        public const string fileShaLogin = "login-checksum.txt";
        public const string supplyLoginData = "forneceDadosUsuario.php";
        public const string INTRANET_REQUIRED = "É necessário conexão com a intranet.";
        public const string ERROR_WINDOWTITLE = "Erro";
        public const string NO_AUTH = "Preencha suas credenciais.";
        public const string SERVER_NOT_FOUND_ERROR = "Servidor não encontrado. Selecione um servidor válido!";
        public const string unknown = "Desconhecido";
        public const string ToBeFilledByOEM = "To Be Filled By O.E.M.";
        public const string ALREADY_RUNNING = "Já existe uma instância do programa em execução!";
        public const string LOG_HEADER = "------------------------LOG DE EXECUÇÃO------------------------";
        public const string LOG_SEPARATOR_SMALL = "------------------------------------------------";
        public const string LOG_SEPARATOR = "____________________________________________________________________________________________________";
        public const string LOG_TIMESTAMP = "HH:mm:ss.ffffff";
        public const string LOG_FILE_EXT = ".log";
        public const string LOG_INFO_ATTR = "<INFORMAÇÃO>";
        public const string LOG_WARNING_ATTR = "<AVISO>";
        public const string LOG_ERROR_ATTR = "<ERRO>";
        public const string LOG_PASSWORD_PLACEHOLDER = "XXXXXXXXXXXXXXX";
        public const string LOG_ARGS_ERROR = ARGS_ERROR;
        public const string PROGRAMDATA_FOLDERNAME = "AppLog";
        public const string INI_SECTION_1 = "Definitions";
        public const string INI_SECTION_1_9 = "LogLocation";

        public static string loginPath = System.IO.Path.GetTempPath() + fileLogin;

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

        public const string login_banner_light_path = "Resources\\header\\login_banner_light.png";
        public const string login_banner_dark_path = "Resources\\header\\login_banner_dark.png";

        public const string main_banner_light_path = "Resources\\header\\main_banner_light.png";
        public const string main_banner_dark_path = "Resources\\header\\main_banner_dark.png";

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


        public const string statusBarTextForm1 = "Sistema desenvolvido pelo servidor Kevin Costa, SIAPE 1971957, para uso no serviço da Subdivisão de Tecnologia da Informação do CCSH - UFSM";
        public const string statusBarTextForm2 = "CCSH - UFSM";
        public const string formTitlebarText = "Coleta de hardware e cadastro de patrimônio / Subdivisão de Tecnologia da Informação do CCSH - UFSM";
        public const string cliHelpTextServer = "Servidor do sistema de patrimônio (Ex.: 192.168.76.103, localhost, etc. Modificar arquivo INI para configuração estática. Primeiro IP da lista será o escolhido caso o parâmetro esteja ausente) - Opcional";
        public const string cliHelpTextPort = "Porta do sistema de patrimônio (Ex.: 8081, 80, etc. Modificar arquivo INI para configuração estática. Primeira porta da lista será a escolhida caso o parâmetro esteja ausente) - Opcional";
        public const string cliHelpTextMode = "Tipo de serviço realizado (Valores possíveis: M/m para manutenção (padrão), F/f para formatação) - Opcional";
        public const string cliHelpTextPatrimony = "Patrimônio do equipamento (Ex.: 123456). Caso o parâmetro esteja ausente, será coletado pelo hostname PC-123456 (padrão) - Opcional";
        public const string cliHelpTextSeal = "Lacre do equipamento (se houver) (Ex.: 12345678, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextRoom = "Sala onde o equipamento estará localizado (Ex.: 1234, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextBuilding = "Prédio onde o equipamento estará localizado (Valores possíveis: 21, 67, 74A, 74B, 74C, 74D, AR, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextActiveDirectory = "Cadastrado no Active Directory (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextStandard = "Padrão da imagem implantado no equipamento (Valores possíveis: A/a para aluno, F/f para funcionário, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextDate = "Data do serviço realizado (Valores possíveis: hoje (padrão), ou especificar data, ex.: 12/12/2020) - Opcional";
        public const string cliHelpTextBattery = "Realizada troca de pilha? (Valores possíveis: S/s (Sim), N/n (Não)) - OBRIGATÓRIO";
        public const string cliHelpTextTicket = "Número do chamado aberto (Ex.: 123456) - OBRIGATÓRIO";
        public const string cliHelpTextInUse = "Equipamento em uso? (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextTag = "Equipamento possui etiqueta? (Valores possíveis: S/s (Sim), N/n (Não), ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextType = "Categoria do equipamento (Valores possíveis: Desktop, Notebook, Tablet, ou 'mesmo' (padrão) para manter inalterado) - Opcional";
        public const string cliHelpTextUser = "Usuário de login - OBRIGATÓRIO";
        public const string cliHelpTextPassword = "Senha de login - OBRIGATÓRIO";
        public const string today = "hoje";
        public const string sameWord = "mesmo";
        public const string ok = "OK", activated = "Ativado", deactivated = "Desativado";
        public const string notSupported = "Não suportado", notDetermined = "Não determinado", notExistant = "Não existente";
        public const string tb = "TB", gb = "GB", mb = "MB", predFail = "Pred Fail";
        public const string ahci = "AHCI", nvme = "NVMe", ide = "IDE/RAID", sata = "SATA", raid = "RAID";
        public const string frequency = "MHz";
        public const string ddr2 = "DDR2", ddr3 = "DDR3", ddr3smbios = "24", ddr3memorytype = "24", ddr4 = "DDR4", ddr4smbios = "26";
        public const string systemRom = "SYSTEM ROM", arch32 = "32", arch64 = "64";
        public const string windows11 = "11", windows10 = "10", windows8_1 = "8.1", windows8 = "8", windows7 = "7";
        public const string bios = "BIOS", uefi = "UEFI";
        public const string hdd = "HDD", ssd = "SSD";
        public const string build = "build";
        public const string offlineModeUser = "test";
        public const string offlineModePassword = "test";
        public const string employee = "Funcionário";
        public const string student = "Aluno";
        public const string replacedBattery = "C/ troca de pilha";
        public const string sameBattery = "S/ troca de pilha";
        public const string THEME_REG_PATH = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";
        public const string HWINFO_REG_PATH = @"Software\HardwareInformation";
        public const string THEME_REG_KEY = "AppsUseLightTheme";
        public const string lastInstall = "LastInstallation", lastMaintenance = "LastMaintenance";
        public const string fileBios = "bios.json";
        public const string fileShaBios = "bios-checksum.txt";
        public const string filePC = "pc.json";
        public const string fileShaPC = "pc-checksum.txt";
        public const string formatURL = "recebeDadosFormatacao", maintenanceURL = "recebeDadosManutencao", supplyBiosData = "forneceDadosBIOS.php", supplyPCData = "forneceDadosPC.php";
        public const string nonSecBootGPU1 = "210", nonSecBootGPU2 = "430";
        public const string webview2url = "https://go.microsoft.com/fwlink/p/?LinkId=2124703";
        public const string webview2file = "webview2installer.exe";
        public const string WEBVIEW2_PATH = "runtimes\\win-x86";
        public const string WEBVIEW2_REG_PATH_X64 = "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";
        public const string WEBVIEW2_REG_PATH_X86 = "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";
        public const string WEBVIEW2_SYSTEM_PATH_X64 = "C:\\Program Files (x86)\\Microsoft\\EdgeWebView\\Application\\";
        public const string WEBVIEW2_SYSTEM_PATH_X86 = "C:\\Program Files\\Microsoft\\EdgeWebView\\Application\\";
        public const string LOGFILE_NOTEXISTS = "Arquivo de log inexistente, criando novo arquivo";
        public const string LOGFILE_EXISTS = "Arquivo de log encontrado, acrescentando informações";
        public const string SMART_FAIL = " (Drive com falha iminente)";
        public const string ONLINE = "ONLINE";
        public const string OFFLINE = "OFFLINE";
        public const string FETCHING = "Coletando...";
        public const string DASH = "-";
        public const string REGISTERING = "Cadastrando / Atualizando, aguarde...";
        public const string FETCH_AGAIN = "Coletar Novamente";
        public const string REGISTER_AGAIN = "Cadastrar / Atualizar dados";
        public const string SERVER_PORT_ERROR = "Para acessar, selecione o servidor e a porta!";
        public const string DEFAULT_HOSTNAME = "MUDAR-NOME";
        public const string CLI_HOSTNAME_ALERT = "Hostname: ";
        public const string HOSTNAME_ALERT = " (Nome incorreto, alterar)";
        public const string MEDIA_OPERATION_NVME = "NVMe";
        public const string MEDIA_OPERATION_IDE_RAID = "IDE/Legacy ou RAID";
        public const string CLI_MEDIA_OPERATION_ALERT = "Modo de operação SATA/M.2: ";
        public const string MEDIA_OPERATION_ALERT = " (Modo de operação incorreto, alterar)";
        public const string CLI_SECURE_BOOT_ALERT = "Secure Boot: ";
        public const string SECURE_BOOT_ALERT = " (Ativar boot seguro)";
        public const string CLI_DATABASE_REACH_ERROR = "Conectividade com o banco de dados: ";
        public const string DATABASE_REACH_ERROR = "Erro ao contatar o banco de dados, verifique a sua conexão com a intranet e se o servidor web está ativo!";
        public const string CLI_BIOS_VERSION_ALERT = "Versão da BIOS/UEFI: ";
        public const string BIOS_VERSION_ALERT = " (Atualizar BIOS/UEFI)";
        public const string CLI_FIRMWARE_TYPE_ALERT = "Tipo de firmware: ";
        public const string FIRMWARE_TYPE_ALERT = " (Tipo de firmware incorreto)";
        public const string TPM_ERROR = " (TPM desativado/versão incorreta)";
        public const string CLI_TPM_ALERT = "Versão do módulo TPM: ";
        public const string NOT_ENOUGH_MEMORY = " (Memória insuficiente)";
        public const string CLI_MEMORY_ALERT = "Memória RAM e nº de slots: ";
        public const string TOO_MUCH_MEMORY = " (Memória em excesso)";
        public const string CLI_NETWORK_IP_ERROR = "Endereço IP: ";
        public const string CLI_NETWORK_MAC_ERROR = "Endereço MAC: ";
        public const string NETWORK_ERROR = "Computador sem conexão com a Intranet";
        public const string CLI_VT_ALERT = "Tecnologia de Virtualização: ";
        public const string VT_ALERT = " (Ativar Tecnologia de Virtualização na BIOS/UEFI)";
        public const string PENDENCY_ERROR = "Resolva as pendências exibidas em vermelho!";
        public const string MANDATORY_FIELD = "Preencha os campos obrigatórios";
        public const string DAYS_PASSED_TEXT = " dias desde a última ";
        public const string FORMAT_TEXT = "formatação";
        public const string MAINTENANCE_TEXT = "manutenção";
        public const string SINCE_UNKNOWN = "(Não foi possível determinar a data do último serviço)";
        public const string ALREADY_REGISTERED_TODAY = "Serviço já registrado para esta dia. Caso seja necessário outro registro, escolha outra data.";
        public const string PC_DROPPED = "Patrimônio baixado, cadastro cancelado. Reverta a baixa e tente novamente";
        public const string OFFLINE_MODE_ACTIVATED = "Modo OFFLINE!";
        public const string FIX_PROBLEMS = "Corrija os problemas a seguir antes de prosseguir:";
        public const string ARGS_ERROR = "Um ou mais argumentos contém erros! Saindo do programa";
        public const string AUTH_ERROR = "Erro de autenticação! Saindo do programa";
        public const string AUTH_INVALID = "Credenciais inválidas. Tente novamente.";
        public const string LOG_INIT = "Inicialização execução";
        public const string LOG_THEME = "Usando tema escuro";
        public const string LOG_OFFLINE_MODE = "Modo de execução offline";
        public const string LOG_DEBUG_MODE = "Executando em modo DEBUG";
        public const string LOG_RELEASE_MODE = "Executando em modo RELEASE";
        public const string LOG_CLOSING_MAINFORM = "Finalizando form principal";
        public const string LOG_CLOSING_LOGINFORM = "Finalizando form de login";
        public const string LOG_CLOSING_CLI = "Finalizando sequência do programa";
        public const string LOG_AUTOTHEME_CHANGE = "Selecionando tema automaticamente";
        public const string LOG_LIGHTMODE_CHANGE = "Mudando para modo claro";
        public const string LOG_DARKMODE_CHANGE = "Mudando para modo escuro";
        public const string LOG_OPENING_LOG = "Abrindo log de eventos";
        public const string LOG_START_THREAD = "Inicializando thread de procedimento de coleta";
        public const string LOG_LOGOUT = "Logout";
        public const string LOG_BM = "Marca";
        public const string LOG_MODEL = "Modelo";
        public const string LOG_SERIALNO = "Número Serial";
        public const string LOG_PROCNAME = "Processador e nº de núcleos";
        public const string LOG_PM = "Memória RAM e n° de slots";
        public const string LOG_HDSIZE = "Armazenamento (espaço total)";
        public const string LOG_SMART = "Status S.M.A.R.T.";
        public const string LOG_MEDIATYPE = "Tipo de armazenamento";
        public const string LOG_MEDIAOP = "Modo de operação SATA/M.2";
        public const string LOG_GPUINFO = "Placa de Vídeo e vRAM";
        public const string LOG_OS = "Sistema Operacional";
        public const string LOG_HOSTNAME = "Nome do Computador";
        public const string LOG_MAC = "Endereço MAC do NIC";
        public const string LOG_IP = "Endereço IP do NIC";
        public const string LOG_BIOSTYPE = "Tipo de firmware";
        public const string LOG_SECBOOT = "Secure Boot";
        public const string LOG_BIOS = "Versão da BIOS/UEFI";
        public const string LOG_VT = "Tecnologia de Virtualização";
        public const string LOG_TPM = "Versão do módulo TPM";
        public const string LOG_FILENAME_CP = "CadastroPatrimonial";
        public const string LOG_START_COLLECTING = "Coletando hardware";
        public const string LOG_END_COLLECTING = "Finalização da coleta de hardware";
        public const string LOG_INIT_REGISTRY = "Iniciando processo de registro";
        public const string LOG_INIT_LOGIN = "Autenticando usuário";
        public const string LOG_SERVER_DETAIL = "Servidor e porta";
        public const string LOG_NO_INTRANET = INTRANET_REQUIRED;
        public const string LOG_PINGGING_SERVER = "Verificando disponibilidade do servidor";
        public const string LOG_OFFLINE_SERVER = "Servidor está OFFLINE";
        public const string LOG_ONLINE_SERVER = "Servidor está ONLINE";
        public const string LOG_LOGIN_FAILED = "Falha na autenticação";
        public const string LOG_LOGIN_SUCCESS = "Autenticação realizada com êxito";
        public const string LOG_LOGIN_INCOMPLETE = NO_AUTH;
        public const string LOG_REGISTERING = "Registrando no servidor de controle patrimonial";
        public const string LOG_REGISTRY_FINISHED = "Registro finalizado";
        public const string LOG_START_LOADING_WEBVIEW2 = "Iniciando carregamento do WebView2 Runtime";
        public const string LOG_END_LOADING_WEBVIEW2 = "Carregamento do WebView2 Runtime finalizado";
        public const string LOG_VIEW_SERVER = "Acessando sistema de controle patrimonial";
        public const string LOG_FETCHING_BIOSFILE = "Obtendo arquivo com informações de modelo de hardware";
        public const string LOG_HARDWARE_PASSED = "Configuração sem pendências, pronto para registro";
        public const string LOG_RESETING_INSTALLDATE = "Recalculando data da última formatação";
        public const string LOG_RESETING_MAINTENANCEDATE = "Recalculando data da última manutenção";
        public const string LOG_CLI_MODE = "Argumentos detectados, iniciando em modo CLI. Lista de argumentos";
        public const string LOG_GUI_MODE = "Argumentos ausentes, iniciando em modo GUI";
        public const string LOG_WEBVIEW2_NOT_FOUND = "WebView2 Runtime não encontrado";
        public const string LOG_INSTALLING_WEBVIEW2 = "Instalando WebView2 Runtime";
        public const string LOG_WEBVIEW2_INSTALLED = "WebView2 Runtime instalado com êxito";
        public const string LOG_WEBVIEW2_INSTALL_FAILED = "Falha na instalação do WebView2 Runtime, tente novamente";
        public const string LOG_WEBVIEW2_ALREADY_INSTALLED = "WebView2 Runtime já está instalado";
        public const string LOG_CHECKING_WEBVIEW2 = "Verificando presença do WebView2 Runtime no sistema";
        public const string LOG_PENDENCY_ERROR = "Pendências detectadas";
        public const string LOG_SAMEWORD_NOFIRSTREGISTRY = "Um ou mais argumentos 'mesmo' detectados, porém não há registro existente para o patrimônio atual. Computador deve ser registrado primeiramente via GUI ou via CLI com todas as opções de linha de comando corretamente preenchidas";
        public const string LOG_SHOWING_HELP = "Exibindo lista de comandos";
        public const string LOG_HOSTNAME_ERROR = HOSTNAME_ALERT;
        public const string LOG_MEDIAOP_ERROR = MEDIA_OPERATION_ALERT;
        public const string LOG_SECBOOT_ERROR = SECURE_BOOT_ALERT;
        public const string LOG_OFFLINE_ERROR = DATABASE_REACH_ERROR;
        public const string LOG_BIOSVER_ERROR = BIOS_VERSION_ALERT;
        public const string LOG_FIRMWARE_ERROR = FIRMWARE_TYPE_ALERT;
        public const string LOG_NETWORK_ERROR = NETWORK_ERROR;
        public const string LOG_VT_ERROR = VT_ALERT;
        public const string LOG_SMART_ERROR = SMART_FAIL;
        public const string LOG_TPM_ERROR = TPM_ERROR;
        public const string LOG_MEMORYFEW_ERROR = NOT_ENOUGH_MEMORY;
        public const string LOG_MEMORYMUCH_ERROR = TOO_MUCH_MEMORY;
        public const string LOG_SERVER_UNREACHABLE = SERVER_NOT_FOUND_ERROR;
        public const string LOG_MANDATORY_FIELD_ERROR = MANDATORY_FIELD;
        public const string LOG_ALREADY_REGISTERED_TODAY = ALREADY_REGISTERED_TODAY;
        public const string LOG_PC_DROPPED = PC_DROPPED;

        public static string pcPath = System.IO.Path.GetTempPath() + filePC;
        public static string biosPath = System.IO.Path.GetTempPath() + fileBios;
        //public static string LOGFILE_LOCATION = "C:\\br.ufsm.ccsh.ti\\" + PROGRAMDATA_FOLDERNAME + "\\";
        public static string webview2filePath = System.IO.Path.GetTempPath() + webview2file;

        public static readonly List<string> listModeCLI = new List<string>() { "F", "f", "M", "m" };
        public static readonly List<string> listModeGUI = new List<string>() { "Formatação", "Manutenção" };
        public static readonly List<string> listActiveDirectoryCLI = new List<string>() { "S", "s", "N", "n" };
        public static readonly List<string> listActiveDirectoryGUI = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listStandardCLI = new List<string>() { "F", "f", "A", "a" };
        public static readonly List<string> listStandardGUI = new List<string>() { "Funcionário", "Aluno" };
        public static readonly List<string> listInUseCLI = new List<string>() { "S", "s", "N", "n" };
        public static readonly List<string> listInUseGUI = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listTagCLI = new List<string>() { "S", "s", "N", "n" };
        public static readonly List<string> listTagGUI = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listBatteryCLI = new List<string>() { "S", "s", "N", "n" };
        public static readonly List<string> listBatteryGUI = new List<string>() { "Sim", "Não" };
        public static readonly List<string> themeGUI = new List<string>() { "Auto", "Light", "Dark" };

        public const string INI_SECTION_1_11 = "ServerIP";
        public const string INI_SECTION_1_12 = "ServerPort";
        public const string INI_SECTION_1_13 = "Buildings";
        public const string INI_SECTION_1_14 = "HWTypes";
        public const string INI_SECTION_1_15 = "ThemeUI";

        public static Color LIGHT_FORECOLOR = SystemColors.ControlText;
        public static Color LIGHT_BACKCOLOR = SystemColors.ControlLight;
        public static Color LIGHT_ASTERISKCOLOR = Color.Red;
        public static Color LIGHT_AGENT = Color.DarkCyan;
        public static Color ALERT_COLOR = Color.Red;
        public static Color OFFLINE_ALERT = Color.Red;
        public static Color ONLINE_ALERT = Color.Lime;
        public static Color DARK_FORECOLOR = SystemColors.ControlLightLight;
        public static Color DARK_BACKCOLOR = Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
        public static Color DARK_ASTERISKCOLOR = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
        public static Color DARK_AGENT = Color.DarkCyan;
        public static Color LIGHT_BACKGROUND = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        public static Color DARK_BACKGROUND = Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
        public static Color DARK_SUBTLE_LIGHTLIGHTCOLOR = Color.DimGray;
        public static Color LIGHT_SUBTLE_DARKDARKCOLOR = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
        public static Color DARK_SUBTLE_LIGHTCOLOR = Color.DarkSlateGray;
        public static Color LIGHT_SUBTLE_DARKCOLOR = Color.Silver;
        public static Color BLUE_FOREGROUND = SystemColors.Highlight;
        public static Color INACTIVE_SYSTEM_BUTTON_COLOR = Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));

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

        public static readonly List<string> fopFileList = new List<string>() { "BCrypt.Net-Next.dll", "ConstantsDLL.dll", "INIFileParser.dll", "JsonFileReaderDLL.dll", "LogGeneratorDLL.dll", "Microsoft.Xaml.Behaviors.dll", "Newtonsoft.Json.dll","TutorialDeUsoDaEstaçãoDeTrabalho.exe", "TutorialDeUsoDaEstaçãoDeTrabalho.exe.config" };
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
        public const string STI_URL = "https://www.ufsm.br/unidades-universitarias/ccsh/unidade-de-tecnologia-da-informacao/";
        public const string UFSM_URL = "https://www.ufsm.br/";
        public const string CCSH_URL = "https://www.ufsm.br/unidades-universitarias/ccsh/";
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
