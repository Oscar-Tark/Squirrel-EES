/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using OpenTK;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Scorpion
{
    public partial class Form1
    {
        public void start_Analyzer_object()
        { vdb_analyzer = new GUI.VDB_Analyzer(this); return; }

        public int GUI_TEMPLATE_COUNT = 0;

        public Scorpion_IDE.Special_TextView spc;
        public reader readr;
        public Scorpion.GUI.VDB_Analyzer vdb_analyzer;
        public GameWindow game;
        public Dumper.Virtual_Dumper_System vds;
        public Scorpion.Crypto.Cryptographer crypto;
        public Hooking.Hooker hook;
        public Internetwork_File_Format.Internetwork_Video_File_Format iff;
        public Memory_Security.Secure_Memory mmsec;
        public FTP.ftp_server ftp_serv;
        public Scorpion.File_operations.Fileopr fleoper;
        public Scorpion.Game_Engine.Scorpion_RS RS;
        public Amatrix_Server_1._1.Form1 serv;

        public string SHA;
        public string[] cmdargs;

        public string Mess;
        public int Index = 0; //Reader Index
        public string Prog_s; //Application String
        public string Orig_path; //Path
        public string func_tmp = ""; public string path_tmp = "";
        public bool real_time = false;
        public int current_thread_count = 0;

        public AutoCompleteStringCollection acsc;
        //public AutocompleteMenuNS.AutocompleteMenu acm_xmd = new AutocompleteMenuNS.AutocompleteMenu();

        public bool is_hardware_accelerated = false;

        public Types types;

        public enum list_type { db_list };

        //Main Collections

        //TERMS/WIKI TERMS
        public ArrayList AL_TERMS_WIKI_REF = new ArrayList() { "license", "windows", "commands" };
        public ArrayList AL_TERMS_WIKI = new ArrayList() { "[LICENSE]", "[WIKI:]", "[COMMANDS:]" };
        public ArrayList AL_WIKI = new ArrayList() {
            "Licensed Under the GNU GPL Version 3\n<One Platform. Noded Command Framework>\nCopyright (C) <2013-2016>  <Oscar Arjun Singh Tark>\n\nThis program is free software: you can redistribute it and/or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or (at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see<http://www.gnu.org/licenses/>.\n\n____________________________________________________________\n\nProgrammers:\n\nOscar Arjun Singh Tark\n\n------------------------------------------------------------------------\n\nIncludes the Following Software:\n\nNONE.\n\n------------------------------------------------------------------------\n\nIncludes the Following Artwork:\n\nLed24.de\n\nhttp://led24.de/iconset/\n\n",
            "If you are trying to start a cmd process and want it to hold. Set the process name as cmd and in the beginning of the first argument put /K as: '/K ping.exe 127.0.0.1'",
            "One Platform internal commands:\nAll commands start with a specific syntax directive.function(*variable,*variable,...)\n\n>System Commands(Start with one.):\none.write(*variable,*variable,...)\none.about()"
        };

        //Ubearables
        public ArrayList AL_UNBEARABLE_CHARS = new ArrayList() { ",", "]"/*, " "*/, ")" };
        public ArrayList AL_WILDCARDS = new ArrayList() { "-", " " };

        //Events
        public ArrayList AL_Ref_EVT = new ArrayList();
        public ArrayList AL_EVT = new ArrayList();

        //WINFORMS GUI

        //holds properties for development use
        public ArrayList AL_GUI_PROPERTIES = new ArrayList();
        
        //public ArrayList AL_GUI_PROPERTIES_CONTAINED = new ArrayList() { null, null, null, null, new ArrayList { AnchorStyles.Bottom, AnchorStyles.Left, AnchorStyles.None, AnchorStyles.Right, AnchorStyles.Top }, new ArrayList() { DockStyle.Bottom, DockStyle.Fill, DockStyle.Left, DockStyle.None, DockStyle.Right, DockStyle.Top }, null };
        public ArrayList AL_GUI_TEMPLATES = new ArrayList() { new Form(), new Button(), new CheckBox(), new CheckedListBox(), new ColorDialog(), new ComboBox(), new ContextMenuStrip(), new DataGridView(), new DateTimePicker(), new DomainUpDown(), new FlowLayoutPanel(), new FolderBrowserDialog(), new FontDialog(), new GroupBox(), new ImageList(), new Label(), new LinkLabel(), new ListBox(), new ListView(), new MaskedTextBox(), new MenuStrip(), new MonthCalendar(), new NotifyIcon(), new NumericUpDown(), new OpenFileDialog(), new PageSetupDialog(), new Panel(), new PictureBox(), new PrintDialog(), new PrintPreviewControl(), new PrintPreviewDialog(), new ProgressBar(), new PropertyGrid(), new RadioButton(), new RichTextBox(), new SaveFileDialog(), new SplitContainer(), new Splitter(), new StatusStrip(), new TabControl(), new TableLayoutPanel(), new TextBox(), new ToolStrip(), new TrackBar(), new TreeView(), new WebBrowser() };
        public ArrayList AL_GUI_TEMPLATES_REF = new ArrayList() { "form", "button", "checkbox", "checkedlistbox", "colordialog", "combobox", "contextmenustrip", "datagridview", "datetimepicker", "domainupdown", "flowlayoutpanel", "folderbrowsingdialog", "fontdialog", "groupbox", "imagelist", "label", "linklabel", "listbox", "listview", "maskedtextbox", "menustrip" ,"monthcalendar", "notifyicon", "numericupdown", "openfiledialog", "pagesetupdialog", "panel", "picturebox", "printdialog", "printpreviewcontrol", "printpreviewdialog", "progressbar", "propertygrid", "radiobutton", "richtextbox", "savefiledialog", "splitcontainer", "splitter", "statusstrip", "tabcontrol", "tablelayoutpanel", "textbox", "toolstrip", "trackbar", "treeview", "webbrowser" };
        
        //OPENTK
        public ArrayList AL_DISP_DEVICES = new ArrayList();
        public ArrayList AL_OBJ_3D = new ArrayList();
        public ArrayList AL_OBJ_3D_REF = new ArrayList();
        public ArrayList AL_UPDATED_OTK_GUI = new ArrayList();

        //Sockets
        public ArrayList AL_SOCK = new ArrayList();
        public ArrayList AL_SOCK_REF = new ArrayList();
        public ArrayList AL_SESSION = new ArrayList();

        //Variables
        public ArrayList AL_CURR_VAR = new ArrayList();
        public ArrayList AL_CURR_VAR_REF = new ArrayList();
        public ArrayList AL_CURR_VAR_TAG = new ArrayList();
        public ArrayList AL_CURR_VAR_EVT = new ArrayList();

        //Tables DATA
        public ArrayList AL_TBLE = new ArrayList();
        public ArrayList AL_TBLE_REF = new ArrayList();
        
        //Variables for Queries
        public ArrayList AL_QUERY_VAR = new ArrayList() { "<cell>", "</cell>", "<operator>", "</operator>", "<create>", "</create>", "<delete>", "</delete>", "<search>", "</search>", "<is>", "</is>", "<isnot>", "</isnot>", "<getall>", "</getall>" };

        //Recursive Callers
        public ArrayList AL_REC_REF = new ArrayList();
        public ArrayList AL_REC = new ArrayList();
        public ArrayList AL_REC_TME = new ArrayList();

        //AuthTable
        public ArrayList AL_AUTH_REF = new ArrayList();
        public ArrayList AL_AUTH = new ArrayList();

        //PIPE Networking
        public ArrayList AL_PIPES = new ArrayList();
        public ArrayList AL_PIPES_REF = new ArrayList();

        //TCPIP Networking
        public ArrayList AL_AMCS = new ArrayList();
        public ArrayList AL_AMCS_REF = new ArrayList();

        //SHS REDEFINED TO EXECUTE SYSTEM COMMAND
        public ArrayList AL_SHS_REF = new ArrayList();
        public ArrayList AL_SHS = new ArrayList();
        public ArrayList AL_SHS_APP = new ArrayList();
        public ArrayList AL_SHS_APP_REF = new ArrayList();
        
        //Controllable Running Processes
        public ArrayList AL_PRC_REF = new ArrayList();
        public ArrayList AL_PRC = new ArrayList();

        //HIB SETTINGS
        public ArrayList AL_HIB_FILES = new ArrayList() { Application.StartupPath + "\\System\\Data\\one.vds" };

        //OneDB Dir                                                                                                                                                                 Alternative OneDB Path without \
        public ArrayList AL_DIRECTORIES = new ArrayList() { Application.StartupPath + "\\System\\OneDB\\", Application.StartupPath + "\\", Application.StartupPath + "\\System\\Data\\", Application.StartupPath + "\\System\\OneBack\\", Application.StartupPath + "\\System\\OneDB", Application.StartupPath + "\\System\\OneSource\\", Application.StartupPath + "\\System\\OneAssemblies\\", Application.StartupPath + "\\System\\SQLite\\" };
        public ArrayList AL_EXTENSNS = new ArrayList() { ".vds", ".vdb", ".vdsqlite" };

        //Accessors Definitions
        public ArrayList AL_ACC = new ArrayList() { ">>", "*", ".", "(", ")", "\\", "@", "#" };
        public ArrayList AL_SECTIONS = new ArrayList() { "data", "gui", "commands", "variables", "assemblies", "scripts" };

        //Operators Definitions
        public ArrayList AL_OPRTRS = new ArrayList() { "+", "-", "/", "*", "%", "&&", "||", "<", ">", "=", "<=", ">=", "!", /*contains*/ "?", /*Union*/"<->", "<-", "->", /*Intersection*/ "<~>" };

        public string[] AL_KEYS = new string[9]
        {
            "fnc",
            "io",
            "net",
            "eng",
            "db",
            "mem",
            "exe",
            "typ",
            "sec"
        };

        /*public string[][] AL_CALLERS = new string[1][]
        {
            //ALL KEYS CORRESPOND TO INDEX OF CALLERS
        };*/

        //D  D     D      D      D       D         D    D             D       D       D       D       D       D       D       D       D                   D       D       D           D       D   D       D               D       D   D       D       D   D   D       D       D   D       D               D       D           D           D           D               D                   D       D       D       D       D   D       D       D       D       D       D       D       D       D       D       D       D               D           D               D           D           D       D       D       D           D           D           D           D                       D               
        public ArrayList AL_FNC_SCRP = new ArrayList() { /*APP:*/
            "write",
            "ca",
            "about",
            "exit",
            "restart"
            /*DATA:*/,
            "query",
            "rel"
            /*FNC:*/,
            "register",
            "unregister",
            "call",
            "recall", "rcsa", "rcsr", "rcsta", "rcsp", "ucll" /*GUI:*/ , "startgame", "endgame", "gmvsnc", "startvee", "winform", "cadr", "radr" /*IO:*/, "newfile", "deletefile", "savefiletext", "savefilebinary", "newfolder", "deletefolder", "clf", "cp", "mp", "cpd", "md" /*MATH:*/, "add", "subtract", "multiply", "divide", "percentagevalue", "percentageratio", "sin", "sinh", "sign", "asin", "cos", "cosh", "acos", "tan", "tanh", "atan", "atan2", "nlog", "log", "log10", "pow", "bigmul", "divrem32", "divrem64", "ieeeremainder", "larger", "smaller", "cieling", "floor", "exp", "absolute", "squareroot", "truncate", "round", "roundtodecimalpoint", "roundtodecimalmidpoint" /*MEM:*/, "new", "reloadsystem", "deletetag", "tag", "delete", "deletesystem", "hibernate", "unhibernate" /*NET:*/, "sendtcp", "st" /*SHS (80):*/, "run" /*TYP:*/, "add", "addat", "remove", "removeat", "index", "length" /*SHS (87):*/, "ab" /*NET 88*/, "start", "stop" /*GUI 90*/, "addpoint", "removepoint" /*NET 92*/, "startpipe", "stoppipe" /*DATA 94*/, "load", "create", "delete", "save" /*NET (HTTP) 99*/, "httpset", "httpget" /*DATA 101*/, "unload", "link" /*GUI*/, "gwen", "stopvee", /*DATA 105*/ "visualize", /*APP*/ "load" /*GUI 106*/ ,"update", "compileproperties", "editproperties", "callmethod" /*ONE 111*/, "windows", "wiki", "analyzer", "cui" /*MEM 115*/, "set" /*DATA*/, "encrypt" /*FNC HOOK*/, "compile", "importedcall", "import" /*MEM*/, "encrypt", "decrypt" /*FTP* 121*/, "authtable", "sendftp", "recieve" /*IO*/, "upload" /*SQLite 125*/, "sqlconnection", "sqlcreate", "sqlopen", "sqlclose", "sqlverify", "sqlset", "sqlget" /*NET AS SERVER 132*/, "startserver", "stopserver"};
                                                      //D       D       D   D       D       D       D       D   D
        public ArrayList AL_ACC_SUP = new ArrayList() { "fnc", "io", "net", "one", "db", "mem", "gui", "cmd", /*Defunct*/"typ", "sec" };

        //TRANSFER TO TABLES
        public ArrayList AL_MESSAGE = new ArrayList() { "[IP REFUSED]", "[PORT REFUSED]", "[FATAL EXIT]", "The specified GUI element could not run a method, make sure it is not disposed, if so delete it and create it again.", "Databases may only be opened from the \\OneDB folder, please make sure that the database you would like to open is copied to that folder or use the import tool in the menu VDB->Import Database.", "[WARNING]", "A System function has raised an error, this might be the cause of a faulty configuration or an unfound bug, Press 'Ignore' to continue on using the system.", "Main database not found. The system will create it on shutdown.\n\nIF you are seeing this message over and over again, your system database might be corrupt. Run the command 'mem.deleteall()', current running system variables will be saved to a fresh database on shutdown, so that you do not loose your work.", "File loaded successfully to variable." };

        //Assemblies
        public ArrayList AL_ASSEMB = new ArrayList();
        public ArrayList AL_ASSEMB_REF = new ArrayList();
        public ArrayList AL_ASSEMB_INST = new ArrayList();
        public ArrayList AL_ASSEMB_PROG = new ArrayList();

        //SQL/NOSQL
        public ArrayList AL_SQL_REF = new ArrayList();
        public ArrayList AL_SQL = new ArrayList();
        

        //TCP
        public long session = 0;
        public Main.Amatrix_Sever_Client_Lite amcl;
        public static string IP = "127.0.0.1";
    }
}