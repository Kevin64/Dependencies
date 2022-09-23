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

        public const string fileLogin = "login.json";
        public static string loginPath = System.IO.Path.GetTempPath() + fileLogin;
        public const string fileShaLogin = "login-checksum.txt";
        public const string supplyLoginData = "forneceDadosUsuario.php";
        public const string INTRANET_REQUIRED = "É necessário conexão com a intranet.";
        public const string ERROR_WINDOWTITLE = "Erro";
        public const string NO_AUTH = "Preencha suas credenciais.";
        public const string SERVER_NOT_FOUND_ERROR = "Servidor não encontrado. Selecione um servidor válido!";
        public const string unknown = "Desconhecido";
        public const string ToBeFilledByOEM = "To Be Filled By O.E.M.";

        /**
         * Common code
         * End
        */

        /**
         * Code exclusive for HardwareInformation application
         * Start
        */
        public const string cliHelpTextServer = "Servidor do sistema de patrimônio (Ex.: 192.168.76.103, localhost, etc)";
        public const string cliHelpTextPort = "Porta do sistema de patrimônio (Ex.: 8081, 80, etc)";
        public const string cliHelpTextMode = "Tipo de serviço realizado (Valores possíveis: F/f para formatação, M/m para manutenção)";
        public const string cliHelpTextPatrimony = "Patrimônio do equipamento (Ex.: 123456)";
        public const string cliHelpTextSeal = "Lacre do equipamento (se houver) (Ex.: 12345678)";
        public const string cliHelpTextRoom = "Sala onde o equipamento estará localizado (Ex.: 1234)";
        public const string cliHelpTextBuilding = "Prédio onde o equipamento estará localizado (Valores possíveis: 21, 67, 74A, 74B, 74C, 74D, AR)";
        public const string cliHelpTextActiveDirectory = "Cadastrado no Active Directory (Valores possíveis: Sim, Não)";
        public const string cliHelpTextStandard = "Padrão da imagem implantado no equipamento (Valores possíveis: F/f para funcionário, A/a para aluno)";
        public const string cliHelpTextDate = "Data do serviço realizado (Valores possíveis: hoje, ou especificar data, ex.: 12/12/2020)";
        public const string cliHelpTextBattery = "Realizada troca de pilha? (Valores possíveis: Sim, Não)";
        public const string cliHelpTextTicket = "Número do chamado aberto (Ex.: 123456)";
        public const string cliHelpTextInUse = "Equipamento em uso? (Valores possíveis: Sim, Não)";
        public const string cliHelpTextTag = "Equipamento possui etiqueta? (Valores possíveis: Sim, Não)";
        public const string cliHelpTextType = "Categoria do equipamento (Valores possíveis: Desktop, Notebook, Tablet)";
        public const string cliHelpTextUser = "Usuário de login";
        public const string cliHelpTextPassword = "Senha de login";
        public const string today = "hoje";
        public const string ok = "OK", activated = "Ativado", deactivated = "Desativado";
        public const string notSupported = "Não suportado", notDetermined = "Não determinado", notExistant = "Não existente";
        public const string tb = "TB", gb = "GB", mb = "MB", predFail = "Pred Fail";
        public const string ahci = "AHCI", nvme = "NVMe", ide = "IDE/Legacy ou RAID", sata = "SATA", raid = "RAID";
        public const string frequency = "MHz";
        public const string ddr2 = "DDR2", ddr3 = "DDR3", ddr3smbios = "24", ddr3memorytype = "24", ddr4 = "DDR4", ddr4smbios = "26";
        public const string systemRom = "SYSTEM ROM", arch32 = "32", arch64 = "64";
        public const string windows10 = "10", windows8_1 = "8.1", windows8 = "8", windows7 = "7";
        public const string bios = "BIOS", uefi = "UEFI";
        public const string hdd = "HDD", ssd = "SSD";
        public const string build = "build";
        public const string offlineModeUser = "test";
        public const string offlineModePassword = "test";
        public const string employee = "Funcionário";
        public const string student = "Aluno";
        public const string replacedBattery = "C/ troca de pilha";
        public const string sameBattery = "S/ troca de pilha";
        public static readonly List<string> defaultServerIP = new List<string>() { "192.168.76.103", "localhost" };
        public static readonly List<string> defaultServerPort = new List<string>() { "8081", "80" };
        public static readonly List<string> listBuilding = new List<string>() { "21", "67", "74A", "74B", "74C", "74D", "AR" };
        public static readonly List<string> listMode = new List<string>() { "F", "f", "M", "m" };
        public static readonly List<string> listActiveDirectory = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listStandard = new List<string>() { "Funcionário", "Aluno" };
        public static readonly List<string> listInUse = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listTag = new List<string>() { "Sim", "Não" };
        public static readonly List<string> listType = new List<string>() { "Desktop", "Notebook", "Tablet" };
        public static readonly List<string> listBattery = new List<string>() { "Sim", "Não" };        
        public static string biosPath = System.IO.Path.GetTempPath() + fileBios;
        public const string THEME_REG_PATH = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";
        public const string HWINFO_REG_PATH = @"Software\HardwareInformation";
        public const string THEME_REG_KEY = "AppsUseLightTheme";
        public const string lastInstall = "LastInstallation", lastMaintenance = "LastMaintenance";
        public const string fileBios = "bios.json";        
        public const string fileShaBios = "bios-checksum.txt";
        public const string formatURL = "recebeDadosFormatacao", maintenanceURL = "recebeDadosManutencao", supplyBiosData = "forneceDadosBIOS.php";
        //public const string nonAHCImodel1 = "7057", nonAHCImodel2 = "8814", nonAHCImodel3 = "6078", nonAHCImodel4 = "560s", nonAHCImodel5 = "945GCM-S2C", nonAHCImodel6 = "G41M-VS3";
        //public const string nvmeModel1 = "A315-56";
        public const string nonSecBootGPU1 = "210", nonSecBootGPU2 = "430";
        public const string WEBVIEW2_PATH = "runtimes\\win-x86";
        public const string SMART_FAIL = " (Drive com falha iminente)";
        public const string ONLINE = "ONLINE";
        public const string OFFLINE = "OFFLINE";
        public const string FETCHING = "Coletando...";
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
        public const string PENDENCY_ERROR = "Resolva as pendencias exibidas em vermelho!";
        public const string MANDATORY_FIELD = "Preencha os campos obrigatórios";
        public const string DAYS_PASSED_TEXT = " dias desde a última ";
        public const string FORMAT_TEXT = "formatação";
        public const string MAINTENANCE_TEXT = "manutenção";
        public const string SINCE_UNKNOWN = "(Não foi possível determinar a data do último serviço)";
        public const string ALREADY_REGISTERED_TODAY = "Serviço já registrado para esta dia. Caso seja necessário outro registro, escolha outra data.";
        public const string OFFLINE_MODE_ACTIVATED = "Modo OFFLINE!";
        public const string FIX_PROBLEMS = "Corrija o problemas a seguir antes de prosseguir:";
        public const string ARGS_ERROR = "Um ou mais argumentos contém erros! Saindo do programa...";
        public const string AUTH_ERROR = "Erro de autenticação! Saindo do programa...";
        public const string AUTH_INVALID = "Credenciais inválidas. Tente novamente.";
        public const int TIMER_INTERVAL = 1000;
        public const int MAX_SIZE = 100;
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
        public static Color BLUE_FOREGROUND = SystemColors.Highlight;

        /**
         * Code exclusive for HardwareInformation application
         * End
        */

        /**
         * Code exclusive for FeaturesOverlayPresentation application
         * Start
        */

        public const string DidItRunAlready = "DidItRunAlready";
        public const string FOP = "FOP";
        public const string FopRegKey = @"Software\FOP";
        public const string win7imgDir = "\\img-windows7\\";
        public const string win10imgDir = "\\img-windows10\\";
        public const string FopRunOnceKey = @"Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string FOPx86 = "C:\\Program Files (x86)\\FOP\\Rever tutorial de uso do computador.lnk";
        public const string FOPx64 = "C:\\Program Files\\FOP\\Rever tutorial de uso do computador.lnk";
        public const string imgExt = ".png";
        public const string resolutionWarning = "Resolução insuficiente. Este programa suporta apenas resoluções iguais ou superiores a 1280x720.";
        public const string finaleScreen = "Finalização";
        public const string finishText = "Finalizar";
        public const string introScreen = "Introdução";
        public const string nextText = "Próximo";
        public const string waitText = "Aguarde ";
        public const string cancelExecution = "Apagar dados de entrega e cancelar\n       execução no próximo boot";
        public const string cancelExecutionResError = "Apagar dados de entrega";
        public const string doExecution = "Enviar dados de entrega e \nexecutar no próximo boot";
        public const string doExecutionResError = "Enviar dados de entrega";
        public const string serverNotFound = "Servidor não encontrado. Selecione um servidor válido!";
        public const string fillForm = "Preencha os campos necessários!";
        public const string win7nt = "6.1";
        public const int Width = 1280;
        public const int Height = 720;

        /**
         * Code exclusive for FeaturesOverlayPresentation application
         * End
        */

        /**
         * Code exclusive for OfflineDriverInstallerOOBE application
         * Start
        */

        public const string defFile = "definitions.ini";        
        public const string resChangeFailed = "Falha ao alterar a resolução";
        public const string resChangeSuccess = "Êxito em alterar a resolução";
        public const string resChangeReboot = "Necessário reinicialização para alterar a resolução";

        /**
         * Code exclusive for OfflineDriverInstallerOOBE application
         * End
        */
    }
}
